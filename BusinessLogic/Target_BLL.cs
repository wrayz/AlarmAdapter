using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 監控項目資訊商業邏輯
    /// </summary>
    public class Target_BLL : GenericBusinessLogic<Target>
    {
        /// <summary>
        /// 監控項目資訊取得
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="targetName">監控項目名稱</param>
        /// <returns></returns>
        public Target GetTarget(string detector, string deviceSn, string targetName)
        {
            var option = new QueryOption
            {
                Relation = true,
                Plan = new QueryPlan { Join = "AlarmConditions" }
            };

            var condition = new Target
            {
                DEVICE_SN = deviceSn,
                TARGET_NAME = targetName
            };

            var target = _dao.Get(option, condition);

            if (detector == "Cacti" && target.OPERATOR_TYPE == null)
                return GetDefaultCactiTarget(condition);

            return target;
        }

        /// <summary>
        /// Cacti 預設監控項目資訊取得
        /// </summary>
        /// <param name="condition">實體條件</param>
        /// <returns></returns>
        private Target GetDefaultCactiTarget(Target condition)
        {
            var value = condition.TARGET_NAME == "Ping" ? "DOWN" : "ALERT";

            var data = new Target
            {
                DEVICE_SN = condition.DEVICE_SN,
                TARGET_NAME = condition.TARGET_NAME,
                OPERATOR_TYPE = "Equal",
                TARGET_STATUS = "0",
                IS_EXCEPTION = "Y",
                ALARM_CONDITIONS = new List<AlarmCondition>
                {
                    new AlarmCondition
                    {
                        DEVICE_SN = condition.DEVICE_SN,
                        TARGET_NAME = condition.TARGET_NAME,
                        TARGET_VALUE = value
                    }
                }
            };

            return _dao.Modify("Insert", data);
        }
    }
}