using System.Collections.Generic;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 狀態運算子判斷介面
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// 檢查
        /// </summary>
        /// <param name="record">記錄值</param>
        /// <param name="conditions">條件清單</param>
        /// <returns></returns>
        bool Check(string record, List<string> conditions);
    }
}