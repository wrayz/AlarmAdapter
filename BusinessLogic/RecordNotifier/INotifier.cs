using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 通知器介面
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="notificationCondition">通知條件</param>
        /// <param name="currentMonitor">目前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <param name="notificationRecord">通知記錄</param>
        /// <returns></returns>
        string IsNotification(NotificationCondition notificationCondition, Monitor currentMonitor, Monitor previousMonitor, RecordNotification notificationRecord);
    }
}