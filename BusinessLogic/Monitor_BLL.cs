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
            var condition = new Monitor
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME
            };

            return (_dao as Monitor_DAO).GetPreviousMonitor(condition);
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
        /// 記錄編號取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <returns></returns>
        internal string GetRecordSn(Monitor monitor)
        {
            //TODO: 未來資料庫有存原始資料，就可撤掉此方法

            var condition = new Monitor
            {
                DEVICE_SN = monitor.DEVICE_SN,
                TARGET_NAME = monitor.TARGET_NAME,
                RECEIVE_TIME = monitor.RECEIVE_TIME
            };

            var option = new QueryOption
            {
                Plan = new QueryPlan
                {
                    Select = "RecordSn"
                }
            };

            return _dao.Get(option, condition).RECORD_SN;
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