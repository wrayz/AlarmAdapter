using APIService.Model;
using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Net.Http;
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
            var logger = NLog.LogManager.GetLogger("Cacti Repair");

            try
            {
                var login = GenericAPIService.GetUserInfo();
                log.USERID = login.USERID;

                //設備是否異常
                if (!IsErrorDevice(log.DEVICE_SN, login))
                    throw new HttpRequestException($"設備 { log.DEVICE_SN } 狀態並非異常，無需修復");

                var bll = new Log_BLL();

                bll.Modify(log.ACTION_TYPE, login, log);

                //是否通知
                if (CheckNotification(log))
                    PushNotification(log);

                return Ok();
            }
            catch (HttpResponseException ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.Forbidden, new APIResponse(ex.Message));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 設備是否異常
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="login">登入人員</param>
        /// <returns></returns>
        private bool IsErrorDevice(string deviceSn, UserLogin login)
        {
            var condition = new Device { DEVICE_SN = deviceSn, DEVICE_TYPE = "N", IS_MONITOR = "Y", DEVICE_STATUS = "E" };
            return GenericBusinessFactory.CreateInstance<Device>().IsExists(new QueryOption(), login, condition);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void PushNotification(Log log)
        {
            var notification = NotificationFactory.CreateInstance(DeviceType.N);
            var type = (EventType)Enum.Parse(typeof(EventType), log.ACTION_TYPE);
            var payload = notification.GetPayload(type, log.DEVICE_SN, log.LOG_SN);
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
            var bll = GenericBusinessFactory.CreateInstance<NotificationRecord>();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "N",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN
            };

            return bll.IsExists(new QueryOption(), new UserLogin(), condition);
        }
    }
}