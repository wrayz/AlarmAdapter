using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// 告警層
    /// </summary>
    public abstract class Alarm : IAlarm
    {
        private List<AlarmCondition> _alarmConditions;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="alarmConditions">告警條件</param>
        public Alarm(List<AlarmCondition> alarmConditions)
        {
            _alarmConditions = alarmConditions;
        }

        /// <summary>
        /// 是否異常
        /// </summary>
        /// <param name="deviceMonitor">設備監控訊息</param>
        /// <returns></returns>
        public bool IsException(DeviceMonitor deviceMonitor)
        {
            var condition = _alarmConditions.Find(x => x.DEVICE_SN == deviceMonitor.DEVICE_SN && x.TARGET_NAME == deviceMonitor.TARGET_NAME);

            return Check(condition.TARGET_VALUE, deviceMonitor.TARGET_VALUE) ? condition.IS_EXCEPTION : !condition.IS_EXCEPTION;
        }

        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="condition">告警條件</param>
        /// <param name="record">監控條件值</param>
        /// <returns></returns>
        protected abstract bool Check(string condition, string record);
    }
}