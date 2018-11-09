using ModelLibrary;
using System;
using System.Collections.Generic;

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
        /// <param name="deviceMonitor">設備監控訊息</param>
        /// <param name="alarmConditions">告警條件清單</param>
        /// <returns></returns>
        public bool IsException(DeviceMonitor deviceMonitor, List<AlarmCondition> alarmConditions)
        {
            if (alarmConditions.Count == 0)
                return DefaultCheck(deviceMonitor);

            var condition = alarmConditions.Find(x => x.DEVICE_SN == deviceMonitor.DEVICE_SN && x.TARGET_NAME == deviceMonitor.TARGET_NAME);

            return Check(condition.TARGET_VALUE, deviceMonitor.TARGET_VALUE) ? condition.IS_EXCEPTION : !condition.IS_EXCEPTION;
        }

        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="record">監控條件值</param>
        /// <returns></returns>
        protected abstract bool DefaultCheck(DeviceMonitor deviceMonitor);

        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="condition">告警條件</param>
        /// <param name="record">監控條件值</param>
        /// <returns></returns>
        protected abstract bool Check(string condition, string record);
    }
}