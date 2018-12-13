using DataExpansion;
using System;
using System.Collections.Generic;

namespace ModelLibrary
{
    public class RecordLog
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        public int? LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 異常紀錄時間
        /// </summary>
        public DateTime? RECORD_TIME { get; set; }

        /// <summary>
        /// 記錄溫度
        /// </summary>
        public decimal? RECORD_TEMPERATURE { get; set; }

        /// <summary>
        /// 紀錄濕度
        /// </summary>
        public decimal? RECORD_HUMIDITY { get; set; }

        /// <summary>
        /// 修復人員帳號
        /// </summary>
        [User]
        public string USERID { get; set; }

        /// <summary>
        /// 修復時間
        /// </summary>
        public DateTime? REPAIR_TIME { get; set; }

        /// <summary>
        /// 修復資訊
        /// </summary>
        public string REPAIR_INFO { get; set; }

        /// <summary>
        /// 恢復時間
        /// </summary>
        public DateTime? RECOVER_TIME { get; set; }

        /// <summary>
        /// 設備資訊
        /// </summary>
        public Device DEVICE_INFO { get; set; }

        /// <summary>
        /// 處理人員資訊
        /// </summary>
        public User USER_INFO { get; set; }

        /// <summary>
        /// 設備群組清單
        /// </summary>
        public List<GroupDevice> GROUP_LIST { get; set; }
    }
}