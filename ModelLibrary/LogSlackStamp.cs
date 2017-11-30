namespace ModelLibrary
{
    /// <summary>
    /// 設備紀錄Slack ts對應資料
    /// </summary>
    public class LogSlackStamp
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        public string LOG_SN { get; set; }

        /// <summary>
        /// 頻道ID
        /// </summary>
        public string CHANNEL_ID { get; set; }

        /// <summary>
        /// 訊息ts
        /// </summary>
        public string TIME_STAMP { get; set; }
    }
}