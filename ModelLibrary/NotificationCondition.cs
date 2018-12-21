namespace ModelLibrary
{
    /// <summary>
    /// 通知條件
    /// </summary>
    public class NotificationCondition
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 通知類型（0 - 狀態改變, 1 - 持續異常, 2 - 持續正常）
        /// </summary>
        public string NOTIFICATION_TYPE { get; set; }

        /// <summary>
        /// 間隔類型（0 - 設備, 1 - 監控項目, 2 - 監控訊息）
        /// </summary>
        public string INTERVAL_LEVEL { get; set; }

        /// <summary>
        /// 間隔時間
        /// </summary>
        public int? INTERVAL_TIME { get; set; }
    }
}