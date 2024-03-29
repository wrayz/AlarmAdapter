﻿using System;

namespace ModelLibrary
{
    /// <summary>
    /// 通知資訊
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 通知編號 (YYYYMMDD + 5S)
        /// </summary>
        public string NOTIFICATION_SN { get; set; }

        /// <summary>
        /// 記錄編號 (YYYYMMDD + 5S)
        /// </summary>
        public string RECORD_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目名稱
        /// </summary>
        public string TARGET_NAME { get; set; }

        /// <summary>
        /// 監控項目訊息
        /// </summary>
        public string TARGET_MESSAGE { get; set; }

        /// <summary>
        /// 是否等待中（Y - 是, N - 否）
        /// </summary>
        public string IS_PENDING { get; set; }

        /// <summary>
        /// 通知時間
        /// </summary>
        public DateTime? NOTIFICATION_TIME { get; set; }

        /// <summary>
        /// 監控資訊
        /// </summary>
        public Monitor MONITOR { get; set; }

        /// <summary>
        /// 設備資訊
        /// </summary>
        public Device DEVICE { get; set; }

        /// <summary>
        /// 監控項目資訊
        /// </summary>
        public Target TARGET { get; set; }
    }
}