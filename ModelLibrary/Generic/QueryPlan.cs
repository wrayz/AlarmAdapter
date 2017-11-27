namespace ModelLibrary.Generic
{
    /// <summary>
    /// 資料查詢計劃
    /// </summary>
    public class QueryPlan
    {
        /// <summary>
        /// 欄位計劃
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// 關聯計劃
        /// </summary>
        public string Join { get; set; }

        /// <summary>
        /// 排序計劃
        /// </summary>
        public string Order { get; set; }
    }
}