using ModelLibrary;
using System;

namespace BusinessLogic.RecordAlarm
{
    internal class CactiAlarm : Alarm
    {
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

        /// <summary>
        /// 告警條件預設檢查
        /// </summary>
        /// <param name="deviceMonitor">設備監控訊息</param>
        /// <returns></returns>
        protected override bool DefaultCheck(DeviceMonitor deviceMonitor)
        {
            var condition = new AlarmCondition
            {
                DEVICE_SN = deviceMonitor.DEVICE_SN,
                TARGET_NAME = deviceMonitor.TARGET_NAME,
                TARGET_VALUE = "ALERT",
                IS_EXCEPTION = true
            };

            //AddAlarmCondition(condition);

            return condition.TARGET_VALUE == deviceMonitor.TARGET_VALUE;
        }

        /// <summary>
        /// 告警條件新增
        /// </summary>
        /// <param name="condition">告警條件</param>
        private void AddAlarmCondition(AlarmCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}