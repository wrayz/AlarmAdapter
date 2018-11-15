using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 狀態改變通知器
    /// </summary>
    internal class StatusChangeNotifier : IStatusNotifier
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="currentMonitor">當前監控訊息</param>
        /// <param name="previousMonitor">前次監控訊息</param>
        /// <returns></returns>
        public bool Check(Monitor currentMonitor, Monitor previousMonitor)
        {
            if (previousMonitor.IS_EXCEPTION == null)
                return currentMonitor.IS_EXCEPTION.Value;

            return currentMonitor.IS_EXCEPTION != previousMonitor.IS_EXCEPTION;
        }
    }
}