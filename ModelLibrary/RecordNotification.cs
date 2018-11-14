using System;

namespace ModelLibrary
{
    /// <summary>
    /// 通知記錄資訊
    /// </summary>
    public class RecordNotification
    {
        /// <summary>
        /// 記錄編號
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
        public string TARGET_MESSAGE{ get; set; }

        /// <summary>
        /// 通知時間
        /// </summary>
        public DateTime? NOTIFICATION_TIME { get; set; }
    }
}
