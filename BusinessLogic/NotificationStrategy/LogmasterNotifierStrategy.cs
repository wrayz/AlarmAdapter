using ModelLibrary;

namespace BusinessLogic.NotificationStrategy
{
    /// <summary>
    /// Logmaster 通知站策略
    /// </summary>
    public class LogmasterNotifierStrategy : NotifierStrategy
    {
        public override string IsNotification(NotificationCondition condition, Monitor monitor, Monitor previousMonitor, Notification notification)
        {
            var check = CheckStatusNotification(condition, monitor, previousMonitor) &&
                         CheckNotificationInterval(condition, monitor, notification);

            var abuseIpDb = new AbuseIpDbBusinessLogic(monitor);

            var result = check && monitor.TARGET_NAME == "detect block ip" ? abuseIpDb.CheckScore() : check;

            return result ? "Y" : "N";
        }
    }
}