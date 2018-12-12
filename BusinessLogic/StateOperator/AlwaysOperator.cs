using System.Collections.Generic;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 持續異常運算子
    /// </summary>
    public class AlwaysOperator : IOperator
    {
        public bool Check(string record, List<string> conditions)
        {
            return true;
        }
    }
}