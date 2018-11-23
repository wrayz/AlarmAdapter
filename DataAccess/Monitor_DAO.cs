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
        /// <param name="monitor">目前監控資訊</param>
        /// <returns></returns>
        public Monitor GetPreviousMonitor(Monitor monitor)
        {
            var context = QueryContextFactory.CreateInstance<Monitor>();

            var condition = new Monitor
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME
            };
            var query = new List<QueryCondition>
            {
                new QueryCondition
                {
                    PropertyName = "RECORD_SN",
                    Type = OperatorType.LessThan,
                    Values = new List<object> { monitor.RECORD_SN }
                }
            };
            var orders = new List<UserOrder>
            {
                new UserOrder
                {
                    PropertyName = "RECORD_SN",
                    Type = OrderType.DESC
                }
            };

            context.Main.Query(condition)
                        .UserQuery(query)
                        .OrderBy(orders)
                        .Pager(1, 1);

            return context.GetEntity();
        }

        /// <summary>
        /// 待通知檢查監控資訊清單取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Monitor> GetNotificationMonitors()
        {
            var context = QueryContextFactory.CreateInstance<Monitor>();
            var condition = new QueryCondition
            {
                PropertyName = "IS_NOTIFICATION",
                Type = OperatorType.ISNULL
            };

            context.Main.Query("IS_NOTIFICATION", OperatorType.ISNULL, new List<object>());

            return context.GetEntities();
        }
    }
}