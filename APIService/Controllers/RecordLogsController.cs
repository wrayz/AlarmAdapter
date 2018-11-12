using APIService.Model;
using BusinessLogic;
using BusinessLogic.License;
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
    /// 數據設備記錄修復 API
    /// </summary>
    public class RecordLogsController : ApiController
    {
        /// <summary>
        /// 數據設備記錄更新
        /// </summary>
        /// <param name="log">數據記錄資料</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData(RecordLog log)
        {
            var logger = NLog.LogManager.GetLogger("iFace Repair");

            try
            {
                CheckLicense();

                var user = GenericAPIService.GetUserInfo();

                if (!IsErrorDevice(log.DEVICE_SN, user))
                    throw new HttpRequestException($"設備 { log.DEVICE_SN } 狀態並非異常，無需修復");

                log.USERID = user.USERID;

                var bll = new RecordLog_BLL();
                bll.ModifyRecordLog("Repair", log);

                if (IsNotification(log))
                {
                    PushNotification(EventType.Repair, log);
                }

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
            var condition = new Device { DEVICE_SN = deviceSn, DEVICE_TYPE = "D", IS_MONITOR = "Y", RECORD_STATUS = "E" };
            return GenericBusinessFactory.CreateInstance<Device>().IsExists(new QueryOption(), login, condition);
        }

        /// <summary>
        /// 告警通知是否存在
        /// </summary>
        /// <param name="log">記錄資訊</param>
        /// <returns></returns>
        private bool IsNotification(RecordLog log)
        {
            var bll = GenericBusinessFactory.CreateInstance<NotificationRecord>();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "D",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN
            };

            return bll.IsExists(new QueryOption(), new UserLogin(), condition);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="log">記錄資料</param>
        private void PushNotification(EventType type, RecordLog log)
        {
            var notification = NotificationFactory.CreateInstance(DeviceType.D);
            var payload = notification.GetPayload(type, log.DEVICE_SN, log.LOG_SN);
            var push = new PushService(payload);

            push.PushNotification();
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        private void CheckLicense()
        {
            var license = new LicenseBusinessLogic();
            license.Verify(DateTime.Now);
        }
    }
}