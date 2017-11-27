using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    public class DeviceLogsController : ApiController
    {
        /// <summary>
        /// 設備紀錄更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ModifyLog(APILog log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }

                var token = LicenseLogic.Token;

                if (!(log.LOG_TIME >= token.StartDate && log.LOG_TIME <= token.EndDate))
                    return Unauthorized();

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();
                //對應設備
                var device = bll.GetDevice(log);

                if (device.DEVICE_SN != null)
                {
                    //對應設備編號擴充
                    log.DEVICE_SN = device.DEVICE_SN;

                    //log編號取得
                    if (log.ACTION_TYPE == "Recover")
                        log.LOG_SN = bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;

                    //紀錄處理
                    var deviceLog = bll.LogModify(log);
                    //詳細記錄資訊取得
                    var detail = bll.GetLogDetail(deviceLog.LOG_SN);
                    //訊息推送結果
                    var responses = await bll.PushEvent(log.ACTION_TYPE, detail);

                    if(Array.IndexOf(responses, HttpStatusCode.Unauthorized) == -1)
                        return Content(HttpStatusCode.Unauthorized, "Log紀錄成功，但推送至IM或Slack未獲得授權");

                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "無對應設備，或對應設備狀態不符");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}