using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// SystemLogs API
    /// </summary>
    public class SystemLogsController : ApiController
    {
        private SimpleLog_BLL _bll = new SimpleLog_BLL();

        /// <summary>
        /// SystemLogs 紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData()
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var content = Request.Content.ReadAsStringAsync().Result;

                //Log 取得
                var log = GetLog(content);

                if (log == null)
                    return Ok();

                //紀錄新增
                var insertedLog = _bll.ModifyLog(log, "C");
                log.LOG_SN = insertedLog.LOG_SN;
                //對應設備取得
                var device = GetDevice(new Device { DEVICE_SN = log.DEVICE_SN, DEVICE_TYPE = "S", IS_MONITOR = "Y" });

                //間隔通知
                //PushInterval(log, device);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 間隔通知
        /// </summary>
        /// <param name="simpleLog">異常記錄</param>
        /// <param name="device">設備資訊</param>
        private void PushInterval(SimpleLog simpleLog, Device device)
        {
            //詳細記錄資訊取得
            var detail = _bll.GetSimpleLog(new SimpleLog { LOG_SN = simpleLog.LOG_SN, DEVICE_SN = simpleLog.DEVICE_SN });
            //通知服務
            var payload = new SimplePayload(detail);
            var pushService = new PushService(payload);

            //設定間隔訊息類型
            var messageType = (MessageType)Enum.Parse(typeof(MessageType), device.NOTIFY_SETTING.MESSAGE_TYPE);
            //檢查結果
            var check = false;

            //異常記錄
            var log = new APILog
            {
                DEVICE_SN = simpleLog.DEVICE_SN,
                LOG_INFO = simpleLog.ERROR_INFO,
                LOG_TIME = simpleLog.ERROR_TIME
            };

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
                //通知推送
                pushService.PushNotification();
                //通知記錄儲存
                SaveRecord(simpleLog);
            }
        }

        /// <summary>
        /// 儲存通知記錄
        /// </summary>
        /// <param name="log">設備記錄</param>
        private void SaveRecord(SimpleLog simpleLog)
        {
            var bll = new DeviceNotifyRecord_BLL();
            //通知記錄物件
            var record = new DeviceNotifyRecord
            {
                DEVICE_SN = simpleLog.DEVICE_SN,
                RECORD_ID = simpleLog.LOG_SN,
                NOTIFY_TIME = simpleLog.ERROR_TIME
            };
            //通知記錄更新
            bll.SaveNotifyRecord(record);
        }

        /// <summary>
        /// Log 取得
        /// </summary>
        /// <param name="plain">原始內容</param>
        /// <returns></returns>
        private SimpleLog GetLog(string plain)
        {
            //log訊息
            var info = plain.Substring(plain.IndexOf("=") + 1);

            //來源IP
            var source = GetSourceIP();
            //log時間
            var time = DateTime.Now;
            //記錄檔
            //File.AppendAllText("C:/EyesFree/CameraLog.txt", string.Format("{0}, Source: {1}, Log: {2}\n", time.ToString(), source, plain));

            var device = GetDevice(new Device { DEVICE_ID = source, DEVICE_TYPE = "S", IS_MONITOR = "Y" });

            if (string.IsNullOrEmpty(device.DEVICE_SN))
                return null;

            return new SimpleLog
            {
                DEVICE_SN = device.DEVICE_SN,
                ERROR_TIME = time,
                ERROR_INFO = info
            };
        }

        /// <summary>
        /// 設備資訊取得
        /// </summary>
        /// <param name="device">實體資料</param>
        /// <returns></returns>
        private Device GetDevice(Device device)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return bll.Get(new QueryOption { Relation = true }, new UserLogin(), device);
        }

        /// <summary>
        /// 取得來源IP
        /// </summary>
        /// <returns></returns>
        private string GetSourceIP()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
    }
}