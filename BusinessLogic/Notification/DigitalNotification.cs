using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Notification
{
    public class DigitalNotification : INotification
    {
        public Payload GetPayload(string type, string deviceSn, int? logSn)
        {
            //紀錄詳細資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<RecordLog>();
            //查詢條件
            var condition = new RecordLog { DEVICE_SN = deviceSn, LOG_SN = logSn };
            //查詢參數
            var option = new QueryOption { Plan = new QueryPlan { Join = "Payload" } };

            var recordLog = dao.Get(option, condition);

            var eventType = (EventType)Enum.Parse(typeof(EventType), type);

            return new RecordPayload(eventType, recordLog);
        }

        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="time">記錄時間</param>
        /// <param name="setting">通知設定</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        public bool IsNotification(DateTime? time, NotificationSetting setting, List<NotificationRecord> records)
        {
            var record = records.FirstOrDefault();

            if (record == null) return true;

            var nextTime = record.NOTIFY_TIME.Value.AddMinutes(setting.MUTE_INTERVAL.Value);

            return time > nextTime;
        }
    }
}