using BusinessLogic.StateOperator;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Linq;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// 告警層
    /// </summary>
    public class Alarmer : IAlarm
    {
        /// <summary>
        /// 是否異常
        /// </summary>
        /// <param name="monitor">監控訊息</param>
        /// <param name="target">監控項目資訊</param>
        /// <returns></returns>
        public string IsException(Monitor monitor, Target target)
        {
            var type = (AlarmOperatorType)Enum.Parse(typeof(AlarmOperatorType), target.OPERATOR_TYPE);
            var alarmOperator = OperatorFactory.CreateInstance(type);
            var conditions = target.ALARM_CONDITIONS.Select(x => x.TARGET_VALUE).ToList();

            return alarmOperator.Check(monitor.TARGET_VALUE, conditions) ? "Y" : "N";
        }
    }
}