using DataAccess;
using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

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
            return (_dao as Monitor_DAO).GetPreviousMonitor(monitor);
        }

        /// <summary>
        /// 待檢查通知之監控清單
        /// </summary>
        /// <returns></returns>
        public List<Monitor> GetNotificationMonitors()
        {
            return (_dao as Monitor_DAO).GetNotificationMonitors().ToList();
        }

        /// <summary>
        /// 監控資訊清單儲存
        /// </summary>
        /// <param name="data">清單資料</param>
        internal void SaveList(List<Monitor> data)
        {
            _dao.ModifyList("InsertList", data);
        }

        /// <summary>
        /// 監控資訊清單更新
        /// </summary>
        /// <param name="data">清單資料</param>
        internal void UpdateList(List<Monitor> data)
        {
            _dao.ModifyList("UpdateList", data);
        }
    }
}