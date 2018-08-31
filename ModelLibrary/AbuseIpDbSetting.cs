namespace ModelLibrary
{
    /// <summary>
    /// 黑名單資料庫設定
    /// </summary>
    public class AbuseIpDbSetting
    {
        /// <summary>
        /// API KEY
        /// </summary>
        public string API_KEY { get; set; }

        /// <summary>
        /// 信任分數
        /// </summary>
        public int CONFIDENCE_SCORE { get; set; }
    }
}
