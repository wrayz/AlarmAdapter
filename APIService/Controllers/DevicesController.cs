using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 設備API
    /// </summary>
    public class DevicesController : ApiController
    {
        /// <summary>
        /// 設備維修
        /// </summary>
        /// <param name="log">設備記錄</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Repair(Log log)
        {
            try
            {
                //User Info
                var login = GenericAPIService.GetUserInfo();
                log.USERID = login.USERID;

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();
                //確認該設備狀態為異常
                var condition = new Device { DEVICE_SN = log.DEVICE_SN, IS_MONITOR = "Y", DEVICE_STATUS = "E" };
                var isError = GenericBusinessFactory.CreateInstance<Device>().IsExists(new QueryOption(), login, condition);

                if (isError)
                {
                    //紀錄處理
                    bll.LogModify(log);
                    //詳細記錄資訊取得
                    var detail = bll.GetLogDetail(log.LOG_SN.Value);
                    log.LOG_TIME = detail.REPAIR_TIME;

                    //推送通知
                    if (bll.hasNotify(log))
                    {
                        var response = PushNotification(log, detail);
                        return Content(HttpStatusCode.OK, new APIResponse(response));
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
        private string PushNotification(Log log, LogDetail detail)
        {
            var response = "";
            //訊息推送
            var pushService = new PushService(log.ACTION_TYPE, detail);
            response += pushService.PushIM() + "\n";
            response += pushService.PushDesktop();

            return response;
        }
    }
}