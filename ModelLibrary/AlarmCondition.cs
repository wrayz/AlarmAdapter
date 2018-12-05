namespace ModelLibrary
{
    /// <summary>
    /// 告警條件
    /// </summary>
    public class AlarmCondition
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目名稱
        /// </summary>
        public string TARGET_NAME { get; set; }

        /// <summary>
        /// 監控項目值
        /// </summary>
        public string TARGET_VALUE { get; set; }
    }
}