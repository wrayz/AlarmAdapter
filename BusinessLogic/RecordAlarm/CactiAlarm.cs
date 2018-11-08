using System.Collections.Generic;
using ModelLibrary;

namespace BusinessLogic.RecordAlarm
{
    internal class CactiAlarm : Alarm
    {
        public CactiAlarm(List<AlarmCondition> alarmConditions) : base(alarmConditions)
        {
        }

        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="condition">告警條件</param>
        /// <param name="record">監控訊息條件值</param>
        /// <returns></returns>
        protected override bool Check(string condition, string record)
        {
            return condition == record;
        }
    }
}