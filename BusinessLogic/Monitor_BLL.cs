using DataAccess;
using ModelLibrary;

namespace BusinessLogic
{
    /// <summary>
    /// 監控資訊商業邏輯
    /// </summary>
    public class Monitor_BLL : GenericBusinessLogic<Monitor>
    {
        /// <summary>
        /// 前次監控資訊取得
        /// </summary>
        /// <param name="monitor">目前監控資訊</param>
        /// <returns></returns>
        public Monitor GetPreviousMonitor(Monitor monitor)
        {
            var condition = new Monitor
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME
            };

            return (_dao as Monitor_DAO).GetPreviousMonitor(condition);
        }
    }
}