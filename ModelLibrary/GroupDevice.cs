using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 群組設備資料
    /// </summary>
    public class GroupDevice
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        public string GROUP_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GROUP_NAME { get; set; }

        /// <summary>
        /// 設備對應ID
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DEVICE_NAME { get; set; }

        /// <summary>
        /// 設備狀態 N - 正常, E - 異常, R-修復中
        /// </summary>
        public string DEVICE_STATUS { get; set; }

        /// <summary>
        /// 是否監控
        /// </summary>
        public string IS_MONITOR { get; set; }

        /// <summary>
        /// 紀錄編號
        /// </summary>
        public string LOG_SN { get; set; }

        /// <summary>
        /// 修復人員帳號
        /// </summary>
        public string REPAIRMAN_ID { get; set; }

        /// <summary>
        /// 修復人員資訊
        /// </summary>
        public User REPAIRMAN_INFO { get; set; }

        /// <summary>
        /// 管理人清單
        /// </summary>
        public List<DeviceMaintainer> MAINTAINER_LIST { get; set; }
    }
}