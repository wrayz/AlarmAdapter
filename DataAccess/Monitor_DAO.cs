using ModelLibrary;
using SourceHelper.Enumerate;

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
            context.Main.Query(condition).OrderBy("RECORD_SN", OrderType.DESC);

            return context.GetEntity();
        }
    }
}