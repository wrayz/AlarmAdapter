using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.StateOperator
{
    public static class OperatorFactory
    {
        public static IOperator CreateInstance(AlarmOperatorType type)
        {
            IOperator alarmOperator;

            switch (type)
            {
                case AlarmOperatorType.Equal:
                    alarmOperator = new EqualOperator();
                    break;

                case AlarmOperatorType.In:
                    alarmOperator = new InOperator();
                    break;

                default:
                    throw new Exception($"尚未實作 { type } 運算子判斷");
            }

            return alarmOperator;
        }
    }
}