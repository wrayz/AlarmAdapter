namespace ModelLibrary
{
    /// <summary>
    /// 黑名單 API 設定
    /// </summary>
    public class AbuseIpDbSetting
    {
        /// <summary>
        /// API KEY
        /// </summary>
        public string API_KEY { get; set; }

        /// <summary>
        /// 黑名單分數
        /// </summary>
        public int ABUSE_SCORE { get; set; }

        /// <summary>
        /// 無類別域間路由
        /// </summary>
        public int CIDR { get; set; }

        /// <summary>
        /// 查詢天數
        /// </summary>
        public int SEARCHE_DAYS { get; set; }
    }
}
