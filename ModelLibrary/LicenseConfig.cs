namespace ModelLibrary
{
    /// <summary>
    /// License參數資料
    /// </summary>
    public class LicenseConfig
    {
        /// <summary>
        /// License金鑰
        /// </summary>
        public string LICENSE_KEY { get; set; }

        /// <summary>
        /// 啟動日期
        /// </summary>
        public string START_DATE { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public string END_DATE { get; set; }

        /// <summary>
        /// 監控設備數
        /// </summary>
        public int? PERMIT_COUNT { get; set; }
    }
}