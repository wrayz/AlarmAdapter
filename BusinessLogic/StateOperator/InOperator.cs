using System.Collections.Generic;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 包含運算子
    /// </summary>
    internal class InOperator : IOperator
    {
        /// <summary>
        /// 檢查
        /// </summary>
        /// <param name="record">記錄值</param>
        /// <param name="conditions">條件清單</param>
        /// <returns></returns>
        public bool Check(string record, List<string> conditions)
        {
            return conditions.Contains(record);
        }
    }
}