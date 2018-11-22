using DataAccess;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// 通知記錄商業邏輯
    /// </summary>
    public class Notification_BLL : GenericBusinessLogic<Notification>
    {
        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <param name="notificationCondition">通知條件</param>
        /// <returns></returns>
        public Notification GetRecord(Monitor monitor, NotificationCondition notificationCondition)
        {
            if (notificationCondition.INTERVAL_TIME == 0)
                return new Notification();

            Notification condition;

            var level = (IntervalLevel)Enum.Parse(typeof(IntervalLevel), notificationCondition.INTERVAL_LEVEL);

            switch (level)
            {
                case IntervalLevel.Device:
                    condition = new Notification { DEVICE_SN = monitor.DEVICE_SN };
                    break;

                case IntervalLevel.MonitorTarget:
                    condition = new Notification
                    {
                        DEVICE_SN = monitor.DEVICE_SN,
                        TARGET_NAME = monitor.TARGET_NAME
                    };
                    break;

                case IntervalLevel.TargetMessage:
                    condition = new Notification
                    {
                        DEVICE_SN = monitor.DEVICE_SN,
                        TARGET_NAME = monitor.TARGET_NAME,
                        TARGET_MESSAGE = monitor.TARGET_MESSAGE
                    };
                    break;

                default:
                    throw new Exception("錯誤的間隔層級");
            }

            return (_dao as Notification_DAO).GetRecord(condition);
        }

        /// <summary>
        /// 待通知清單取得
        /// </summary>
        /// <returns></returns>
        public List<Notification> GetPendingNotifications()
        {
            var query = new QueryOption
            {
                Relation = true
            };

            var condition = new Notification
            {
                IS_PENDING = "Y"
            };

            return _dao.GetList(query, condition).ToList();
        }
    }
}