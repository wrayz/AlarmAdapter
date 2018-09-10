using APIService.Model;
using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 一般網路設備修復 API
    /// </summary>
    public class DevicesController : ApiController
    {
        /// <summary>
        /// 一般網路設備維修 API
        /// </summary>
        /// <param name="log">設備記錄</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Repair(Log log)
        {
            try
            {
                var login = GenericAPIService.GetUserInfo();
                log.USERID = login.USERID;

                var isError = IsErrorDevice(log, login);

                //設備是否異常
                if (isError)
                {
                    var bll = new DeviceLog_BLL();

                    bll.Modify(log.ACTION_TYPE, login, log);
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，或對應設備狀態不符"));
                }

                //是否通知
                if (CheckNotification(log))
                    PushNotification(log);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 設備是否異常
        /// </summary>
        /// <param name="log">設備記錄</param>
        /// <param name="login">登入人員</param>
        /// <returns></returns>
        private bool IsErrorDevice(Log log, UserLogin login)
        {
            var condition = new Device { DEVICE_SN = log.DEVICE_SN, IS_MONITOR = "Y", DEVICE_STATUS = "E" };
            var isError = GenericBusinessFactory.CreateInstance<Device>().IsExists(new QueryOption(), login, condition);
            return isError;
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void PushNotification(Log log)
        {
            var notification = NotificationFactory.CreateInstance(DeviceType.Network);
            var payload = notification.GetPayload(log.ACTION_TYPE, log.DEVICE_SN, log.LOG_SN);
            var push = new PushService(payload);

            push.PushNotification();
        }

        /// <summary>
        /// 告警通知是否存在
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        private bool CheckNotification(Log log)
        {
            var bll = new NotificationRecord_BLL();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "N",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN
            };

            return bll.CheckNotification(condition);
        }
    }
}