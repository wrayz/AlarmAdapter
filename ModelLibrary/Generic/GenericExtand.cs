namespace ModelLibrary.Generic
{
    /// <summary>
    /// 擴展資訊
    /// </summary>
    public class GenericExtand
    {
        /// <summary>
        /// 資料庫連線
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 動作前動作
        /// </summary>
        public string Before { get; set; }

        /// <summary>
        /// 擴充方法名稱
        /// </summary>
        public string Method { get; set; }
    }
}