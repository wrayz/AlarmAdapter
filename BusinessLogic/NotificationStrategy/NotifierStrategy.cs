using BusinessLogic.RecordNotifier;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.NotificationStrategy
{
    /// <summary>
    /// 通知器策略
    /// </summary>
    public abstract class NotifierStrategy
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="condition">通知條件</param>
        /// <param name="monitor">目前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <param name="notification">通知記錄</param>
        /// <returns></returns>
        public abstract string IsNotification(NotificationCondition condition, Monitor monitor, Monitor previousMonitor, Notification notification);

        /// <summary>
        /// 狀態通知檢查
        /// </summary>
        /// <param name="condition">通知條件</param>
        /// <param name="monitor">目前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <returns></returns>
        protected bool CheckStatusNotification(NotificationCondition condition, Monitor monitor, Monitor previousMonitor)
        {
            var type = (NotificationType)Enum.Parse(typeof(NotificationType), condition.NOTIFICATION_TYPE);
            var statusNotifier = StatusNotifierFactory.CreateInstance(type);

            return statusNotifier.Check(monitor, previousMonitor);
        }

        /// <summary>
        /// 間隔通知檢查
        /// </summary>
        /// <param name="condition">通知條件</param>
        /// <param name="monitor">目前監控訊息</param>
        /// <param name="notification">通知記錄</param>
        /// <returns></returns>
        protected bool CheckNotificationInterval(NotificationCondition condition, Monitor monitor, Notification notification)
        {
            if (notification.NOTIFICATION_TIME == null)
                return true;

            var nextTime = notification.NOTIFICATION_TIME.Value.AddMinutes(condition.INTERVAL_TIME.Value);

            return monitor.RECEIVE_TIME >= nextTime;
        }
    }
}