using APIService.Model;
using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 數據設備紀錄資料API
    /// </summary>
    public class RecordsController : ApiController
    {
        //商業邏輯
        private RecordLog_BLL _bll;

        //通知商業邏輯
        private INotification _notification;

        /// <summary>
        /// 資料紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                CheckLicense();

                var content = Request.Content.ReadAsStringAsync().Result;

#if Release
                RecordRawData(content);
#endif

                _bll = new RecordLog_BLL();
                _notification = NotificationFactory.CreateInstance(DeviceType.Digital);

                //轉換資料
                var data = _bll.ParseData(content);
                //監控參數取得
                var limit = _bll.GetLimit();

                foreach (var record in data)
                {
                    if (string.IsNullOrEmpty(record.DEVICE_ID)) break;

                    var device = GetDevice(record.DEVICE_ID);

                    if (string.IsNullOrEmpty(device.DEVICE_SN)) break;

                    record.DEVICE_SN = device.DEVICE_SN;

                    _bll.InsertRecord(record);

                    Process(record, device, limit);
                }

                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return Content(HttpStatusCode.Forbidden, new APIResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 告警處理
        /// </summary>
        /// <param name="record">告警記錄</param>
        /// <param name="device">設備資訊</param>
        /// <param name="limit">告警限制</param>
        private void Process(Record record, Device device, RecordLimit limit)
        {
            if (_bll.IsError(record, limit, device.RECORD_STATUS))
            {
                SaveErrorRecordLog(record);
                var alarm = new Alarm { Time = record.RECORD_TIME };

                if (_notification.IsNotification(alarm, device.NOTIFICATION_SETTING, device.NOTIFICATION_RECORDS))
                {
                    var deviceRecord = GetDeviceRecord(record.DEVICE_SN);

#if Release
                    PushNotification(EventType.Error, deviceRecord);
#endif

                    SaveNotifyRecord(deviceRecord);
                }
            }

            if (_bll.IsRecover(record, limit, device.RECORD_STATUS))
            {
                SaveRecoverRecordLog(record);

                var deviceRecord = GetDeviceRecord(record.DEVICE_SN);

                if (CheckNotification(deviceRecord))
                {
                    PushNotification(EventType.Recover, deviceRecord);
                }
            }
        }

        /// <summary>
        /// 異常記錄儲存
        /// </summary>
        /// <param name="record">告警記錄</param>
        private void SaveErrorRecordLog(Record record)
        {
            var data = new RecordLog
            {
                DEVICE_SN = record.DEVICE_SN,
                RECORD_TIME = record.RECORD_TIME,
                RECORD_TEMPERATURE = record.RECORD_TEMPERATURE,
                RECORD_HUMIDITY = record.RECORD_HUMIDITY
            };

            _bll.ModifyRecordLog("Abnormal", data);
        }

        /// <summary>
        /// 正常記錄儲存
        /// </summary>
        /// <param name="record">告警記錄</param>
        private void SaveRecoverRecordLog(Record record)
        {
            var data = new RecordLog
            {
                DEVICE_SN = record.DEVICE_SN,
                RECOVER_TIME = record.RECORD_TIME
            };

            _bll.ModifyRecordLog("Recover", data);
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="deviceRecord">設備對應告警記錄</param>
        private void SaveNotifyRecord(DeviceRecord deviceRecord)
        {
            var bll = new NotificationRecord_BLL();

            var data = new NotificationRecord
            {
                DEVICE_TYPE = "D",
                DEVICE_SN = deviceRecord.DEVICE_SN,
                LOG_SN = deviceRecord.LOG_SN
            };

            bll.SaveNotification(data);
        }

        /// <summary>
        /// 告警通知是否存在
        /// </summary>
        /// <param name="deviceRecord">設備對應告警記錄</param>
        /// <returns></returns>
        private bool CheckNotification(DeviceRecord deviceRecord)
        {
            var bll = new NotificationRecord_BLL();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "D",
                DEVICE_SN = deviceRecord.DEVICE_SN,
                LOG_SN = deviceRecord.LOG_SN
            };

            return bll.CheckNotification(condition);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="deviceRecord">設備對應告警記錄</param>
        private void PushNotification(EventType type, DeviceRecord deviceRecord)
        {
            var payload = _notification.GetPayload(type, deviceRecord.DEVICE_SN, deviceRecord.LOG_SN);
            var push = new PushService(payload);

            push.PushNotification();
        }

        /// <summary>
        /// 設備記錄對應取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        private DeviceRecord GetDeviceRecord(string deviceSn)
        {
            var bll = GenericBusinessFactory.CreateInstance<DeviceRecord>();
            var condition = new DeviceRecord { DEVICE_SN = deviceSn };
            return bll.Get(new QueryOption(), new UserLogin(), condition);
        }

        /// <summary>
        /// 設備資訊取得
        /// </summary>
        /// <param name="deviceId">設備 ID</param>
        /// <returns></returns>
        private Device GetDevice(string deviceId)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            var option = new QueryOption { Relation = true, Plan = new QueryPlan { Join = "Records" } };
            var condition = new Device
            {
                DEVICE_ID = deviceId,
                DEVICE_TYPE = "D",
                IS_MONITOR = "Y"
            };

            return bll.Get(option, new UserLogin(), condition);
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        private void CheckLicense()
        {
            if (LicenseLogic.Token == null)
                throw new HttpRequestException("License key 無效，請檢查License Key");

            var token = LicenseLogic.Token;
            var time = DateTime.Now;

            if (!(time >= token.StartDate && time <= token.EndDate))
                throw new HttpRequestException("License key 已過期，請檢查License Key");
        }

        /// <summary>
        /// 記錄檔
        /// </summary>
        /// <param name="content">原始資料</param>
        private void RecordRawData(string content)
        {
            //log時間
            var time = DateTime.Now;
            //記錄檔
            File.AppendAllText("C:/EyesFree/DataLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), content));
        }
    }
}