using System;

namespace ModelLibrary
{
    /// <summary>
    /// 設備監控資訊
    /// </summary>
    public class Monitor
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
        /// 設備識別碼
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 監控項目條件值
        /// </summary>
        public string TARGET_VALUE { get; set; }

        /// <summary>
        /// 監控項目訊息
        /// </summary>
        public string TARGET_MESSAGE { get; set; }

        /// <summary>
        /// 接收時間
        /// </summary>
        public DateTime? RECEIVE_TIME { get; set; }

        /// <summary>
        /// 是否異常
        /// </summary>
        public bool? IS_EXCEPTION { get; set; }

        /// <summary>
        /// 是否通知
        /// </summary>
        public bool? IS_NOTIFICATION { get; set; }

        /// <summary>
        /// 等於
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Monitor monitor))
                return false;
            else
            {
                return monitor.DEVICE_ID == DEVICE_ID &&
                       monitor.TARGET_NAME == TARGET_NAME &&
                       monitor.TARGET_VALUE == TARGET_VALUE &&
                       monitor.TARGET_MESSAGE == TARGET_MESSAGE &&
                       monitor.RECEIVE_TIME == RECEIVE_TIME;
            }
        }
    }
}