using System;

namespace ModelLibrary
{
    /// <summary>
    /// 告警通知記錄
    /// </summary>
    public class NotificationRecord
    {
        /// <summary>
        /// 設備類型
        /// </summary>
        public string DEVICE_TYPE { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 記錄編號 
        /// </summary>
        public int? LOG_SN { get; set; }

        /// <summary>
        /// 通知時間
        /// </summary>
        public DateTime? NOTIFY_TIME { get; set; }

        /// <summary>
        /// 一般網路設備異常記錄
        /// </summary>
        public Log NETWORK_LOG { get; set; }

        /// <summary>
        /// 簡易設備異常記錄
        /// </summary>
        public SimpleLog SIMPLE_LOG { get; set; }
    }
}
