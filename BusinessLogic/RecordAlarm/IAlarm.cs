using ModelLibrary;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// 告警
    /// </summary>
    public interface IAlarm
    {
        /// <summary>
        /// 是否異常
        /// </summary>
        /// <param name="deviceMonitor">設備監控訊息</param>
        /// <returns></returns>
        bool IsException(DeviceMonitor deviceMonitor);
    }
}