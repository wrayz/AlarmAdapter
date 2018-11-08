using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// 告警器工廠
    /// </summary>
    public static class AlarmFactory
    {
        /// <summary>
        /// 告警器實體產生
        /// </summary>
        /// <param name="type">設備類型</param>
        /// <param name="alarmConditions">告警條件</param>
        /// <returns></returns>
        public static Alarm CreateInstance(string type, List<AlarmCondition> alarmConditions)
        {
            Alarm alarm;

            switch (type)
            {
                case "Cacti":
                    alarm = new CactiAlarm(alarmConditions);
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { type } 告警判斷");
            }

            return alarm;
        }
    }
}