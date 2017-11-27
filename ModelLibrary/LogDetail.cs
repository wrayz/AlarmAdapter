using System;
using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 紀錄詳細資料
    /// </summary>
    public class LogDetail
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        public string LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備IP
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DEVICE_NAME { get; set; }

        /// <summary>
        /// 設備狀態
        /// </summary>
        public string DEVICE_STATUS { get; set; }

        /// <summary>
        /// 異常時間
        /// </summary>
        public DateTime? ERROR_TIME { get; set; }

        /// <summary>
        /// 異常資訊
        /// </summary>
        public string ERROR_INFO { get; set; }

        /// <summary>
        /// 修復人員帳號
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 修復人員名稱
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 修復起始時間
        /// </summary>
        public DateTime? REPAIR_TIME { get; set; }

        /// <summary>
        /// 修復說明
        /// </summary>
        public string REPAIR_INFO { get; set; }

        /// <summary>
        /// 恢復時間
        /// </summary>
        public DateTime? UP_TIME { get; set; }

        /// <summary>
        /// 設備對應群組清單
        /// </summary>
        public List<DeviceGroup> GROUP_LIST { get; set; }

        /// <summary>
        /// 設備保管人清單
        /// </summary>
        public List<DeviceMaintainer> MAINTAINER_LIST { get; set; }
    }
}