using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 通知器
    /// </summary>
    internal class Notifier : INotifier
    {
        private NotificationCondition _notificationCondition;
        private DeviceMonitor _currentMonitor;

        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="notificationCondition">通知條件</param>
        /// <param name="currentMonitor">目前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <param name="notificationRecord">通知記錄</param>
        /// <returns></returns>
        public bool IsNotification(NotificationCondition notificationCondition, DeviceMonitor currentMonitor, DeviceMonitor previousMonitor, NotificationRecord notificationRecord)
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
        private bool CheckStatusNotification(DeviceMonitor previousMonitor)
        {
            var statusNotifier = StatusNotifierFactory.CreateInstance(_notificationCondition.NOTICATION_TYPE);

            return statusNotifier.Check(_currentMonitor, previousMonitor);
        }

        /// <summary>
        /// 間隔通知檢查
        /// </summary>
        /// <returns></returns>
        private bool CheckNotificationInterval(NotificationRecord record)
        {
            if (record.NOTIFY_TIME == null)
                return true;

            var nextTime = record.NOTIFY_TIME.Value.AddMinutes(_notificationCondition.INTERVAL_TIME.Value);

            return _currentMonitor.RECEIVE_TIME >= nextTime;
        }
    }
}