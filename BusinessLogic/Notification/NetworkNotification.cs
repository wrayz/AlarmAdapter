using DataAccess;
using ModelLibrary;
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
                    record = records.Find(x => x.RECORD_CONTENT == alarm.Content);
                    break;
                default:
                    throw new Exception();
            }

            if (record == null) return true;

            var nextTime = record.NOTIFY_TIME.Value.AddMinutes(setting.MUTE_INTERVAL.Value);

            return alarm.Time > nextTime;
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
    }
}