using ModelLibrary;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 持續異常通知器
    /// </summary>
    internal class ContinuityAbnormalityNotifier : IStatusNotifier
    {
        public bool Check(Monitor currentMonitor, Monitor previousMonitor)
        {
            return true;
        }
    }
}