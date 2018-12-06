using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 狀態運算子判斷介面
    /// </summary>
    public interface IOperator
    {
        bool Check(string record, List<AlarmCondition> conditions);
    }
}