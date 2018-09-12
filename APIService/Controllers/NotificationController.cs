using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 通知 API
    /// </summary>
    public class NotificationController : ApiController
    {
        /// <summary>
        /// 通知 API
        /// </summary>
        /// <param name="record">通知物件</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult POST([FromBody]NotificationRecord record)
        {
            //設備類型
            var type = (DeviceType)Enum.Parse(typeof(DeviceType), record.DEVICE_TYPE);
            //通知邏輯
            var notification = NotificationFactory.CreateInstance(type);
            //通知服務
            var pushservice = new PushService(notification.GetPayload(EventType.Error, record.DEVICE_SN, record.LOG_SN));

            try
            {
                //通知
                pushservice.PushNotification();
                //儲存
                SaveNotification(record);

                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return Content(HttpStatusCode.Forbidden, ex);
            }
        }

        /// <summary>
        /// 通知儲存
        /// </summary>
        /// <param name="record">通知物件</param>
        private void SaveNotification(NotificationRecord record)
        {
            var bll = new NotificationRecord_BLL();
            bll.SaveNotification(record);
        }
    }
}