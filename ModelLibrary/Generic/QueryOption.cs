namespace ModelLibrary.Generic
{
    /// <summary>
    /// 資料查詢參數
    /// </summary>
    public class QueryOption
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public QueryOption()
        {
            Extand = new GenericExtand();
        }

        /// <summary>
        /// 擴展資訊
        /// </summary>
        public GenericExtand Extand { get; set; }

        /// <summary>
        /// 資料來源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 關聯計劃
        /// </summary>
        public QueryPlan Plan { get; set; }

        /// <summary>
        /// 查詢條件
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 虛擬實體名
        /// </summary>
        public string VirtualName { get; set; }

        /// <summary>
        /// 是否直接一對一關聯
        /// </summary>
        public bool Relation { get; set; }

        /// <summary>
        /// 使用者查詢
        /// </summary>
        public UserCustom Custom { get; set; }

        /// <summary>
        /// 每頁筆數
        /// </summary>
        public DataPager Page { get; set; }

        /// <summary>
        /// 是否需設定登入使用者
        /// </summary>
        public bool User { get; set; }

        /// <summary>
        /// 權限設定
        /// </summary>
        public string LimitType { get; set; }
    }
}