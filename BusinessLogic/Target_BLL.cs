using ModelLibrary;
using ModelLibrary.Generic;

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
        /// <param name="deviceSn">設備編號</param>
        /// <param name="targetName">監控項目名稱</param>
        /// <returns></returns>
        public Target GetTarget(string deviceSn, string targetName)
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

            return _dao.Get(option, condition);
        }
    }
}