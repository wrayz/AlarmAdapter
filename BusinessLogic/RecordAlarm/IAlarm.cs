using ModelLibrary;
using System.Collections.Generic;

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
        /// <param name="monitor">監控訊息</param>
        /// <param name="target">監控項目資訊</param>
        /// <returns></returns>
        string IsException(Monitor monitor, Target target);
    }
}