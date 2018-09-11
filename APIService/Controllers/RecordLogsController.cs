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
    /// 數據設備記錄修復 API
    /// </summary>
    public class RecordLogsController : ApiController
    {
        //商業邏輯
        private RecordLog_BLL _bll = new RecordLog_BLL();

        /// <summary>
        /// 數據設備記錄更新
        /// </summary>
        /// <param name="log">數據記錄資料</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData(RecordLog log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                    throw new HttpRequestException("License key 無效，請檢查License Key");

                var user = GenericAPIService.GetUserInfo();

                if (IsErrorDevice(log.DEVICE_SN, user))
                {
                    log.USERID = user.USERID;
                    _bll.ModifyRecordLog("Repair", log);

                    if (CheckNotification(log))
                    {
                        PushNotification(EventType.Repair, log);
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
        private bool CheckNotification(RecordLog log)
        {
            var bll = new NotificationRecord_BLL();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "D",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN
            };

            return bll.CheckNotification(condition);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="log">記錄資料</param>
        private void PushNotification(EventType type, RecordLog log)
        {
            var notification = NotificationFactory.CreateInstance(DeviceType.Digital);
            var payload = notification.GetPayload(type, log.DEVICE_SN, log.LOG_SN);
            var push = new PushService(payload);

            push.PushNotification();
        }
    }
}