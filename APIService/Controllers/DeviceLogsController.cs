using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using System;
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

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();
                //對應設備編號
                string deviceSn = bll.GetDeviceByID(log);

                if (!string.IsNullOrEmpty(deviceSn))
                {
                    log.DEVICE_SN = deviceSn;

                    switch (Enum.Parse(typeof(EventType), log.ACTION_TYPE))
                    {
                        case EventType.Error:
                            //紀錄處理
                            bll.LogModify(log);
                            //log編號取得
                            log.LOG_SN = bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                            break;
                        case EventType.Recover:
                            //log編號取得
                            log.LOG_SN = bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                            //紀錄處理
                            bll.LogModify(log);
                            break;
                    }

                    //推送通知
                    if (bll.hasNotify(log))
                    {
                        //詳細記錄資訊取得
                        var detail = bll.GetLogDetail(log.LOG_SN.Value);

                        PushNotification(log, detail);
                    }

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
        /// 推送通知
        /// </summary>
        /// <param name="log">設備記錄</param>
        /// <param name="detail">記錄詳細資訊</param>
        private void PushNotification(Log log, LogDetail detail)
        {
            //訊息推送
            var pushService = new PushService(log.ACTION_TYPE, detail);
            pushService.PushIM();
            pushService.PushDesktop();
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