using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.StateOperator
{
    /// <summary>
    /// 介於運算子
    /// </summary>
    public class BetweenOperator : IOperator
    {
        public bool Check(string record, List<string> conditions)
        {
            var source = Convert.ToDecimal(record);
            var min = Convert.ToDecimal(conditions.Min());
            var max = Convert.ToDecimal(conditions.Max());
            return source >= min && source <= max;
        }
    }
}