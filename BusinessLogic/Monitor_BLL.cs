using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
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

        /// <summary>
        /// 維修監控項目取得
        /// </summary>
        /// <param name="repair">維修資訊</param>
        /// <returns></returns>
        public string GetRepairTarget(Repair repair)
        {
            var monitor = new Monitor
            {
                RECORD_SN = repair.RECORD_SN,
                DEVICE_SN = repair.DEVICE_SN
            };

            return _dao.Get(new QueryOption(), monitor).TARGET_NAME;
        }
    }
}