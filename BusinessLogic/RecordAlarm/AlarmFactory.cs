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
        /// <param name="type">偵測器類型</param>
        /// <returns></returns>
        public static Alarm CreateInstance(string type)
        {
            Alarm alarm;

            switch (type)
            {
                case "Cacti":
                    alarm = new CactiAlarm();
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { type } 告警判斷");
            }

            return alarm;
        }
    }
}