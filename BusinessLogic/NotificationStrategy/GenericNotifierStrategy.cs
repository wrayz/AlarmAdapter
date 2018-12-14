using ModelLibrary;

namespace BusinessLogic.NotificationStrategy
{
    /// <summary>
    /// 一般通知站策略
    /// </summary>
    public class GenericNotifierStrategy : NotifierStrategy
    {
        public override string IsNotification(NotificationCondition condition, Monitor monitor, Monitor previousMonitor, Notification notification)
        {
            var result = CheckStatusNotification(condition, monitor, previousMonitor) &&
                         CheckNotificationInterval(condition, monitor, notification);

            return result ? "Y" : "N";
        }
    }
}