namespace ModelLibrary.Generic
{
    /// <summary>
    /// 資料處理參數
    /// </summary>
    public class ModifyOption
    {
        /// <summary>
        /// 處理類型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 擴展資訊
        /// </summary>
        public GenericExtand Extand { get; set; }

        /// <summary>
        /// 實體資料
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 實體集合
        /// </summary>
        public string List { get; set; }

        /// <summary>
        /// 非實體屬性之參數資料
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 是否搬移檔案
        /// </summary>
        public bool File { get; set; }

        /// <summary>
        /// 是否需設定登入使用者
        /// </summary>
        public bool User { get; set; }
    }
}