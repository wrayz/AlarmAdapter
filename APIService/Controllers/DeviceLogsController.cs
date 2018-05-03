using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 設備訊息 API
    /// </summary>
    public class DeviceLogsController : ApiController
    {
        /// <summary>
        /// 設備紀錄更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ModifyLog(Log log)
        {
            try
            {
                //來源IP驗證
                //if (!Validate())
                //    return Content(HttpStatusCode.Unauthorized, new APIResponse("來源IP未認證"));

                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var token = LicenseLogic.Token;

                if (!(log.LOG_TIME >= token.StartDate && log.LOG_TIME <= token.EndDate))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 已過期，請檢查License Key"));

                //設備ID檢查
                if (string.IsNullOrEmpty(log.DEVICE_ID))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("資料未包含設備ID，請檢查資料內容"));

                //log時間
                var time = DateTime.Now;
                //記錄檔
                File.AppendAllText("C:/EyesFree/DeviceLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), JsonConvert.SerializeObject(log)));

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();
                //對應設備編號
                string device = bll.GetDeviceByID(log);

                if (!string.IsNullOrEmpty(device))
                {
                    //對應設備編號擴充
                    log.DEVICE_SN = device;

                    //log編號取得
                    if (log.ACTION_TYPE == "Recover")
                    {
                        //log編號取得
                        log.LOG_SN = bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                        //紀錄處理
                        bll.LogModify(log);
                    }
                    else
                    {
                        //紀錄處理
                        bll.LogModify(log);
                        //紀錄編號取得
                        log.LOG_SN = bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                    }
                    
                    //詳細記錄資訊取得
                    var detail = bll.GetLogDetail(log.LOG_SN.Value);

                    //訊息推送
                    var result = await bll.PushEvent(log.ACTION_TYPE, detail);

                    if (!result)
                        return Content(HttpStatusCode.Forbidden, new APIResponse("Log 紀錄成功，但推送至 IM 時失敗"));

                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，或對應設備狀態不符"));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 來源IP驗證
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.UserHostAddress;
        }
    }
}