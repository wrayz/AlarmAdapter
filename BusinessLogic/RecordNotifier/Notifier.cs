using ModelLibrary;
using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 通知器
    /// </summary>
    internal class Notifier : INotifier
    {
        private NotificationCondition _notificationCondition;
        private Monitor _currentMonitor;

        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="notificationCondition">通知條件</param>
        /// <param name="currentMonitor">目前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <param name="notificationRecord">通知記錄</param>
        /// <returns></returns>
        public bool IsNotification(NotificationCondition notificationCondition, Monitor currentMonitor, Monitor previousMonitor, RecordNotification notificationRecord)
        {
            _notificationCondition = notificationCondition;
            _currentMonitor = currentMonitor;

            return CheckStatusNotification(previousMonitor) && CheckNotificationInterval(notificationRecord);
        }

        /// <summary>
        /// 狀態通知檢查
        /// </summary>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <returns></returns>
        private bool CheckStatusNotification(Monitor previousMonitor)
        {
            var type = (NotificationType)Enum.Parse(typeof(NotificationType), _notificationCondition.NOTICATION_TYPE);
            var statusNotifier = StatusNotifierFactory.CreateInstance(type);

            return statusNotifier.Check(_currentMonitor, previousMonitor);
        }

        /// <summary>
        /// 間隔通知檢查
        /// </summary>
        /// <param name="notificationRecord">通知記錄</param>
        /// <returns></returns>
        private bool CheckNotificationInterval(RecordNotification record)
        {
            if (record.NOTIFICATION_TIME == null)
                return true;

            var nextTime = record.NOTIFICATION_TIME.Value.AddMinutes(_notificationCondition.INTERVAL_TIME.Value);

            return _currentMonitor.RECEIVE_TIME >= nextTime;
        }
    }
}