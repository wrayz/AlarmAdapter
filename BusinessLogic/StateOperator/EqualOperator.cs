using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 相等運算子
    /// </summary>
    public class EqualOperator : IOperator
    {
        /// <summary>
        /// 檢查
        /// </summary>
        /// <param name="record">監控項目的值</param>
        /// <param name="conditions">告警條件</param>
        /// <returns></returns>
        public bool Check(string record, List<AlarmCondition> conditions)
        {
            return record == conditions.First().TARGET_VALUE;
        }
    }
}