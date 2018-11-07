﻿namespace ModelLibrary
{
    /// <summary>
    /// 設備監控資訊 
    /// </summary>
    public class DeviceMonitor
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目名稱
        /// </summary>
        public string TARGET_NAME { get; set; }

        /// <summary>
        /// 監控項目訊息內容
        /// </summary>
        public string TARGET_CONTENT { get; set; }

        /// <summary>
        /// 是否異常
        /// </summary>
        public bool IS_EXCEPTION { get; set; }

        /// <summary>
        /// 是否通知
        /// </summary>
        public bool IS_NOTIFICATION { get; set; }
    }
}