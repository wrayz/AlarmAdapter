using ModelLibrary;
using ModelLibrary.Generic;

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
        /// <param name="condition">告警條件</param>
        /// <param name="record">監控訊息條件值</param>
        /// <returns></returns>
        protected override bool Check(string condition, string record)
        {
            return condition == record;
        }

        /// <summary>
        /// 預設告警條件取得
        /// </summary>
        /// <param name="monitor">監控訊息</param>
        /// <returns></returns>
        protected override AlarmCondition GetDefaultCondition(Monitor monitor)
        {
            var value = monitor.TARGET_NAME == "Ping" ? "DOWN" : "ALERT";
            var condition = new AlarmCondition
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME,
                TARGET_VALUE = value,
                IS_EXCEPTION = "Y"
            };

            Save(condition);

            return condition;
        }

        /// <summary>
        /// 告警條件儲存
        /// </summary>
        /// <param name="data">實體資料</param>
        private void Save(AlarmCondition data)
        {
            var bll = GenericBusinessFactory.CreateInstance<AlarmCondition>();

            bll.Modify("Insert", new UserLogin(), data);
        }
    }
}