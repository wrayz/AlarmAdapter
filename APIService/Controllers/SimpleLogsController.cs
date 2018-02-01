using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// SystemLogs API
    /// </summary>
    public class SystemLogsController : ApiController
    {
        private SimpleLog_BLL _bll = new SimpleLog_BLL();

        /// <summary>
        /// SystemLogs 紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData()
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var content = Request.Content.ReadAsStringAsync().Result;

                //Log 取得
                var log = GetLog(content);

                if (log == null)
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備"));
                
                //紀錄新增
                _bll.ModifyLog(log);
                //推送至IM
                _bll.PushIM(log);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// LogMaster 紀錄
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData(APILog log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                //對應設備取得
                var device = GetDevice(log.DEVICE_ID);

                var simpleLog = new SimpleLog
                {
                    DEVICE_SN = device.DEVICE_SN,
                    ERROR_TIME = log.LOG_TIME,
                    ERROR_INFO = log.LOG_INFO
                };

                //紀錄新增
                _bll.ModifyLog(simpleLog);
                //推送至IM
                _bll.PushIM(simpleLog);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// Log 取得
        /// </summary>
        /// <param name="plain">原始內容</param>
        /// <returns></returns>
        private SimpleLog GetLog(string plain)
        {
            var pattern = @"\n(.+?)\r\n";
            //最後一筆 Log
            var lastLog = Regex.Match(plain, pattern, RegexOptions.RightToLeft).ToString();

            //來源IP
            var source = GetSourceIP();
            var device = GetDevice(source);

            if (string.IsNullOrEmpty(device.DEVICE_SN))
                return null;

            //異常時間
            var time = Convert.ToDateTime(Regex.Match(lastLog, @"\((.*?)\)").Groups[1].ToString());

            if (lastLog.Contains("tampering dark detected!"))
            {
                return new SimpleLog
                {
                    DEVICE_SN = device.DEVICE_SN,
                    ERROR_TIME = time,
                    ERROR_INFO = "tampering dark detected"
                };
            }
            else if (lastLog.Contains("tampering detected!"))
            {
                return new SimpleLog
                {
                    DEVICE_SN = device.DEVICE_SN,
                    ERROR_TIME = time,
                    ERROR_INFO = "tampering detected"
                };
            }

            return null;
        }

        /// <summary>
        /// 設備資料取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        private Device GetDevice(string id)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return bll.Get(new QueryOption(), new UserLogin(), new Device { DEVICE_ID = id, DEVICE_TYPE = "S", IS_MONITOR = "Y" });
        }

        /// <summary>
        /// 取得來源IP
        /// </summary>
        /// <returns></returns>
        private string GetSourceIP()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
    }
}