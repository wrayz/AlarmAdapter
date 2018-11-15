using ModelLibrary;
using SourceHelper.Core;
using SourceHelper.Enumerate;
using System.Collections.Generic;

namespace DataAccess
{
    /// <summary>
    /// 監控資料存取
    /// </summary>
    public class Monitor_DAO : GenericDataAccess<Monitor>
    {
        /// <summary>
        /// 前次監控資訊取得
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public Monitor GetPreviousMonitor(Monitor condition)
        {
            var context = QueryContextFactory.CreateInstance<Monitor>();
            var order = new List<UserOrder>
            {
                new UserOrder
                {
                    PropertyName = "RECORD_SN",
                    Type = OrderType.DESC
                }
            };
            context.Main.Query(condition).OrderBy(order);

            return context.GetEntity();
        }
    }
}