using ModelLibrary;
using SourceHelper.Core;
using SourceHelper.Enumerate;
using System.Collections.Generic;

namespace DataAccess
{
    /// <summary>
    /// 通知記錄資料存取
    /// </summary>
    public class Notification_DAO : GenericDataAccess<Notification>
    {
        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public Notification GetRecord(Notification condition)
        {
            var context = QueryContextFactory.CreateInstance<Notification>();
            var orders = new List<UserOrder>
            {
                new UserOrder
                {
                    PropertyName = "NOTIFICATION_SN",
                    Type = OrderType.DESC
                }
            };

            context.Main.Query(condition)
                        .OrderBy(orders)
                        .Pager(1, 1);

            return context.GetEntity();
        }
    }
}