using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// 告警層
    /// </summary>
    public abstract class Alarm : IAlarm
    {
        /// <summary>
        /// 是否異常
        /// </summary>
        /// <param name="monitor">監控訊息</param>
        /// <param name="target">監控項目資訊</param>
        /// <returns></returns>
        public string IsException(Monitor monitor, Target target)
        {
            List<AlarmCondition> conditions;

            if (target.ALARM_CONDITIONS.Count == 0)
                conditions = GetDefaultCondition(monitor).ALARM_CONDITIONS;
            else
                conditions = target.ALARM_CONDITIONS.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME)
                                                    .ToList();

            if (conditions.Count == 0)
                conditions = GetDefaultCondition(monitor).ALARM_CONDITIONS;

            return Check(monitor.TARGET_VALUE, conditions) ? "Y" : "N";
        }

        /// <summary>
        /// 預設告警條件取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <returns></returns>
        protected abstract Target GetDefaultCondition(Monitor monitor);

        //TODO: DefaultCheck，待前端做出監控項目新增後再拿掉。

        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="record">監控條件值</param>
        /// <param name="conditions">告警條件清單</param>
        /// <returns></returns>
        protected abstract bool Check(string record, List<AlarmCondition> conditions);
    }
}