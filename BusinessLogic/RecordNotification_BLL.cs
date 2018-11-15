using DataAccess;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic
{
    /// <summary>
    /// 通知記錄商業邏輯
    /// </summary>
    public class RecordNotification_BLL : GenericBusinessLogic<RecordNotification>
    {
        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <param name="notificationCondition">通知條件</param>
        /// <returns></returns>
        internal RecordNotification GetRecord(Monitor monitor, NotificationCondition notificationCondition)
        {
            if (notificationCondition.INTERVAL_TIME == 0)
                return new RecordNotification();

            RecordNotification condition;

            var level = (IntervalLevel)Enum.Parse(typeof(IntervalLevel), notificationCondition.INTERVAL_LEVEL);

            switch (level)
            {
                case IntervalLevel.Device:
                    condition = new RecordNotification { DEVICE_SN = monitor.DEVICE_SN };
                    break;

                case IntervalLevel.MonitorTarget:
                    condition = new RecordNotification
                    {
                        DEVICE_SN = monitor.DEVICE_SN,
                        TARGET_NAME = monitor.TARGET_NAME
                    };
                    break;

                case IntervalLevel.TargetMessage:
                    condition = new RecordNotification
                    {
                        DEVICE_SN = monitor.DEVICE_SN,
                        TARGET_NAME = monitor.TARGET_NAME,
                        TARGET_MESSAGE = monitor.TARGET_MESSAGE
                    };
                    break;

                default:
                    throw new Exception("錯誤的間隔層級");
            }

            return (_dao as RecordNotification_DAO).GetRecord(condition);
        }
    }
}