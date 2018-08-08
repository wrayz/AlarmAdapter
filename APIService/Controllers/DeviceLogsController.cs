using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 設備訊息 API
    /// </summary>
    public class DeviceLogsController : ApiController
    {
        //商業邏輯
        private EventBusinessLogic _bll = new EventBusinessLogic();

        /// <summary>
        /// 設備紀錄更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ModifyLog(Log log)
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

                //對應設備編號
                string deviceSn = _bll.GetDeviceByID(log);

                if (!string.IsNullOrEmpty(deviceSn))
                {
                    log.DEVICE_SN = deviceSn;

                    switch (Enum.Parse(typeof(EventType), log.ACTION_TYPE))
                    {
                        case EventType.Error:
                            //紀錄處理
                            _bll.LogModify(log);
                            //log編號取得
                            log.LOG_SN = _bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                            break;

                        case EventType.Recover:
                            //log編號取得
                            log.LOG_SN = _bll.GetDeviceLog(log.DEVICE_SN).LOG_SN;
                            //紀錄處理
                            _bll.LogModify(log);
                            break;
                    }

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
            //事件類型
            var type = (EventType)Enum.Parse(typeof(EventType), log.ACTION_TYPE);
            //詳細記錄資訊取得
            var detail = _bll.GetLogDetail(new LogDetail { LOG_SN = log.LOG_SN });
            //通知服務
            var payload = new IMPayload(type, detail);
            var pushService = new PushService(payload);

            //設備資訊取得
            var device = _bll.GetDevice(log.DEVICE_SN);
            //設定間隔訊息類型
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