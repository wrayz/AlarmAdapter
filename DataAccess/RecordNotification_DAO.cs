using ModelLibrary;
using SourceHelper.Core;
using SourceHelper.Enumerate;
using System.Collections.Generic;

namespace DataAccess
{
    /// <summary>
    /// 通知記錄資料存取
    /// </summary>
    public class RecordNotification_DAO : GenericDataAccess<RecordNotification>
    {
        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <returns></returns>
        public RecordNotification GetRecord(RecordNotification condition)
        {
            var context = QueryContextFactory.CreateInstance<RecordNotification>();
            var orders = new List<UserOrder>
            {
                new UserOrder
                {
                    PropertyName = "NOTIFICATION_TIME",
                    Type = OrderType.DESC
                }
            };

            context.Main.Query(condition).OrderBy(orders);

            return context.GetEntity();
        }
    }
}