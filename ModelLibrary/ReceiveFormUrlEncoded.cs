using System;

namespace ModelLibrary
{
    /// <summary>
    /// FormUrlEncoded 接收格式 (0.5版)
    /// </summary>
    public class ReceiveFormUrlEncoded
    {
        /// <summary>
        /// 設備識別碼
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 動作類型
        /// </summary>
        public string ACTION_TYPE { get; set; }

        /// <summary>
        /// 記錄訊息
        /// </summary>
        public string LOG_INFO { get; set; }

        /// <summary>
        /// 記錄時間
        /// </summary>
        public DateTime LOG_TIME { get; set; }

        /// <summary>
        /// 監控項目
        /// </summary>
        public string TARGET_NAME { get; set; }
    }
}