using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.RecordAlarm
{
    /// <summary>
    /// Cacti 告警器
    /// </summary>
    internal class CactiAlarm : Alarm
    {
        /// <summary>
        /// 告警條件檢查
        /// </summary>
        /// <param name="record">監控訊息條件值</param>
        /// <param name="conditions">告警條件清單</param>
        /// <returns></returns>
        protected override bool Check(string record, List<AlarmCondition> conditions)
        {
            return conditions.First().TARGET_VALUE == record;
        }

        /// <summary>
        /// 預設告警條件取得
        /// </summary>
        /// <param name="monitor">監控訊息</param>
        /// <returns></returns>
        protected override Target GetDefaultCondition(Monitor monitor)
        {
            var value = monitor.TARGET_NAME == "Ping" ? "DOWN" : "ALERT";
            var condition = new Target
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME,
                TARGET_STATUS = "0",
                ALARM_OPERATOR = "=",
                IS_EXCEPTION = "Y",
                ALARM_CONDITIONS = new List<AlarmCondition>
                {
                    new AlarmCondition
                    {
                        DEVICE_SN = monitor.DEVICE_SN,
                        TARGET_NAME = monitor.TARGET_NAME,
                        TARGET_VALUE = monitor.TARGET_VALUE
                    }
                }
            };

            Save(condition);

            return condition;
        }

        /// <summary>
        /// 告警條件儲存
        /// </summary>
        /// <param name="data">實體資料</param>
        private void Save(Target data)
        {
            var bll = GenericBusinessFactory.CreateInstance<Target>();

            bll.Modify("Insert", new UserLogin(), data);
        }
    }
}