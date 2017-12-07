using DataExpansion;
using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 設備資料
    /// </summary>
    public class MemberDevice
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

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
        /// 修復人員帳號
        /// </summary>
        public string REPAIRMAN_ID { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [User]
        public string USERID { get; set; }

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