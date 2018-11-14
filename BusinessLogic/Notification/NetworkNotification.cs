using DataAccess;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Notification
{
    /// <summary>
    /// 一般網路設備通知
    /// </summary>
    public class NetworkNotification : INotification
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="alarm">告警物件</param>
        /// <param name="condition">通知條件</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        public bool IsNotification(Alarm alarm, NotificationCondition condition, List<NotificationRecord> records)
        {
            NotificationRecord record;

            var level = (IntervalLevel)Enum.Parse(typeof(IntervalLevel), condition.INTERVAL_LEVEL);

            switch (level)
            {
                case IntervalLevel.Device:
                    record = records.OrderByDescending(x => x.LOG_SN).FirstOrDefault();
                    break;

                case IntervalLevel.MonitorTarget:
                    record = records.Find(x => x.RECORD_CONTENT == alarm.Content);
                    break;

                default:
                    throw new Exception();
            }

            if (record == null) return true;

            var nextTime = record.NOTIFY_TIME.Value.AddMinutes(condition.INTERVAL_TIME.Value);

            return alarm.Time >= nextTime;
        }

        /// <summary>
        /// 通知物件取得
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="logSn">記錄編號</param>
        /// <returns></returns>
        public Payload GetPayload(EventType type, string deviceSn, int? logSn)
        {
            //紀錄詳細資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<LogDetail>();
            //查詢條件
            var condition = new LogDetail { DEVICE_SN = deviceSn, LOG_SN = logSn };
            //查詢參數
            var option = new QueryOption { Plan = new QueryPlan { Join = "Detail" } };

            var logDetail = dao.Get(option, condition);

            return new IMPayload(type, logDetail);
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="data">通知記錄資料</param>
        public void Save(NotificationRecord data)
        {
            var dao = GenericDataAccessFactory.CreateInstance<NotificationRecord>();

            dao.Modify("Save", data);
        }
    }
}