namespace ModelLibrary.Generic
{
    /// <summary>
    /// 分頁
    /// </summary>
    public class DataPager
    {
        /// <summary>
        /// 每頁筆數
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 目前頁碼
        /// </summary>
        public int Index { get; set; }
    }
}