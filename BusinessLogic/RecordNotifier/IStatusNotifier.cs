using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 狀態通知器
    /// </summary>
    public interface IStatusNotifier
    {
        /// <summary>
        /// 通知檢查
        /// </summary>
        /// <param name="currentMonitor"></param>
        /// <param name="previousMonitor"></param>
        /// <returns></returns>
        bool Check(Monitor currentMonitor, Monitor previousMonitor);
    }
}