using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Notification
{
    /// <summary>
    /// 簡易設備通知
    /// </summary>
    public class SimpleNotification : INotification
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="time">記錄時間</param>
        /// <param name="setting">通知設定</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        public Payload GetPayload(EventType type, string deviceSn, int? logSn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<SimpleLog>();

            var option = new QueryOption { Plan = new QueryPlan { Join = "Payload" } };
            var condition = new SimpleLog { DEVICE_SN = deviceSn, LOG_SN = logSn };

            var simpleLog = dao.Get(option, condition);

            return new SimplePayload(simpleLog);
        }

        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="alarm">告警物件</param>
        /// <param name="setting">通知設定</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        public bool IsNotification(Alarm alarm, NotificationSetting setting, List<NotificationRecord> records)
        {
            NotificationRecord record;

            switch (setting.MESSAGE_TYPE)
            {
                case "A":
                    record = records.OrderByDescending(x => x.LOG_SN).FirstOrDefault();
                    break;
                case "S":
                    record = records.Find(x => x.RECORD_CONTENT == alarm.Content );
                    break;
                default:
                    throw new Exception();
            }

            if (record == null) return true;

            var nextTime = record.NOTIFY_TIME.Value.AddMinutes(setting.MUTE_INTERVAL.Value);

            return alarm.Time > nextTime;
        }
    }
}