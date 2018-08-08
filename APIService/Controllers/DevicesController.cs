using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 設備API
    /// </summary>
    public class DevicesController : ApiController
    {
        //商業邏輯
        private EventBusinessLogic _bll = new EventBusinessLogic();

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

                //確認該設備狀態為異常
                var condition = new Device { DEVICE_SN = log.DEVICE_SN, IS_MONITOR = "Y", DEVICE_STATUS = "E" };
                var isError = GenericBusinessFactory.CreateInstance<Device>().IsExists(new QueryOption(), login, condition);

                if (isError)
                {
                    //紀錄處理
                    _bll.LogModify(log);

                    //間隔通知
                    PushInterval(log);

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
        /// 間隔通知
        /// </summary>
        /// <param name="log">設備記錄</param>
        private void PushInterval(Log log)
        {
            //詳細記錄資訊取得
            var detail = _bll.GetLogDetail(new LogDetail { LOG_SN = log.LOG_SN });
            //通知服務
            var payload = new IMPayload(EventType.Repair, detail);
            var pushService = new PushService(payload);
            
            //設備資訊取得
            var device = _bll.GetDevice(log.DEVICE_SN);
            //訊息類型
            var messageType = (MessageType)Enum.Parse(typeof(MessageType), device.NOTIFY_SETTING.MESSAGE_TYPE);
            //檢查結果
            var check = false;

            switch (messageType)
            {
                case MessageType.A:
                    check = _bll.CheckAllMessageInterval(log, device.NOTIFY_SETTING);
                    break;
                case MessageType.S:
                    check = _bll.CheckSameMessageInterval(log, device.NOTIFY_SETTING);
                    break;
            }

            if (check)
            {
                //推送通知
                pushService.PushNotification();
                //通知記錄儲存
                log.LOG_INFO = detail.ERROR_INFO + "Repair";
                log.LOG_TIME = detail.REPAIR_TIME;
                SaveRecord(log);
            }
            else
            {
                //IM 訊息儲存
                pushService.SaveIMMessage();
            }
        }

        /// <summary>
        /// 儲存通知記錄
        /// </summary>
        /// <param name="log">設備記錄</param>
        private void SaveRecord(Log log)
        {
            var bll = new DeviceNotifyRecord_BLL();
            //通知記錄物件
            var record = new DeviceNotifyRecord
            {
                DEVICE_SN = log.DEVICE_SN,
                ERROR_INFO = log.LOG_INFO,
                NOTIFY_TIME = log.LOG_TIME
            };
            //通知記錄更新
            bll.SaveNotifyRecord(record);
        }
    }
}