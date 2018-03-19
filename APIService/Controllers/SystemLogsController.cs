using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.IO;
using System.Net;
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
                    return Ok();

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
        /// Log 取得
        /// </summary>
        /// <param name="plain">原始內容</param>
        /// <returns></returns>
        private SimpleLog GetLog(string plain)
        {
            //log訊息
            var info = plain.Substring(plain.IndexOf("=") + 1);

            //來源IP
            var source = GetSourceIP();
            //log時間
            var time = DateTime.Now;
            //記錄檔
            File.AppendAllText("C:/EyesFree/CameraLog.txt", string.Format("{0}, Source: {1}, Log: {2}\n", time.ToString(), source, plain));

            var device = GetDevice(source);

            if (string.IsNullOrEmpty(device.DEVICE_SN))
                return null;

            return new SimpleLog
            {
                DEVICE_SN = device.DEVICE_SN,
                ERROR_TIME = time,
                ERROR_INFO = info
            };
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