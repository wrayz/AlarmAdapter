using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 通知器
    /// </summary>
    internal class Notifier
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="currentMonitor"></param>
        /// <param name="previousMonitor"></param>
        /// <returns></returns>
        public bool IsNotification(NotificationCondition condition, DeviceMonitor currentMonitor, DeviceMonitor previousMonitor)
        {
            var statusNotifier = StatusNotifierFactory.CreateInstance(condition.NOTICATION_TYPE);
            var result = statusNotifier.Check(currentMonitor, previousMonitor);

            return result;
        }
    }
}