using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
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
        public Target GetTarget(Detector detector, string deviceSn, string targetName)
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

            if (target.OPERATOR_TYPE == null)
                return DefaultTarget(detector, condition);

            return target;
        }

        /// <summary>
        /// 預設監控項目取得
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="condition">實體條件</param>
        /// <returns></returns>
        private Target DefaultTarget(Detector detector, Target condition)
        {
            Target target;

            switch (detector)
            {
                case Detector.Cacti:
                    target = GetCactiTarget(condition);
                    break;

                case Detector.Camera:
                    target = GetAlwaysTarget(condition);
                    break;

                case Detector.BobCacti:
                    target = GetBobCactiTarget(condition);
                    break;

                case Detector.Logmaster:
                    target = GetAlwaysTarget(condition);
                    break;

                default:
                    throw new Exception($"無 { detector } 預設監控項目設定");
            }

            return target;
        }

        /// <summary>
        /// Bob Cacti 預設監控項目資訊取得
        /// </summary>
        /// <param name="condition">實體條件</param>
        /// <returns></returns>
        private Target GetBobCactiTarget(Target condition)
        {
            var data = new Target
            {
                DEVICE_SN = condition.DEVICE_SN,
                TARGET_NAME = condition.TARGET_NAME,
                OPERATOR_TYPE = "In",
                TARGET_STATUS = "0",
                IS_EXCEPTION = "Y",
                ALARM_CONDITIONS = new List<AlarmCondition>
                {
                    new AlarmCondition
                    {
                        DEVICE_SN = condition.DEVICE_SN,
                        TARGET_NAME = condition.TARGET_NAME,
                        TARGET_VALUE = "Error"
                    }
                }
            };

            return _dao.Modify("Insert", data);
        }

        /// <summary>
        /// Camera 預設監控項目資訊取得
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Target GetAlwaysTarget(Target condition)
        {
            var data = new Target
            {
                DEVICE_SN = condition.DEVICE_SN,
                TARGET_NAME = condition.TARGET_NAME,
                OPERATOR_TYPE = "Always",
                TARGET_STATUS = "0",
                IS_EXCEPTION = "Y",
                ALARM_CONDITIONS = new List<AlarmCondition>()
            };

            return _dao.Modify("Insert", data);
        }

        /// <summary>
        /// Cacti 預設監控項目資訊取得
        /// </summary>
        /// <param name="condition">實體條件</param>
        /// <returns></returns>
        private Target GetCactiTarget(Target condition)
        {
            var value = condition.TARGET_NAME == "Ping" ? "DOWN" : "ALERT";

            var data = new Target
            {
                DEVICE_SN = condition.DEVICE_SN,
                TARGET_NAME = condition.TARGET_NAME,
                OPERATOR_TYPE = "In",
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