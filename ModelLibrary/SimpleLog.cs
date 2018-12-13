using System;
using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 簡易設備異常紀錄
    /// </summary>
    public class SimpleLog
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
        /// 異常時間
        /// </summary>
        public DateTime? ERROR_TIME { get; set; }

        /// <summary>
        /// 異常資訊
        /// </summary>
        public string ERROR_INFO { get; set; }

        /// <summary>
        /// 設備資訊
        /// </summary>
        public Device DEVICE_INFO { get; set; }

        /// <summary>
        /// 設備群組清單
        /// </summary>
        public List<GroupDevice> GROUP_LIST { get; set; }
    }
}