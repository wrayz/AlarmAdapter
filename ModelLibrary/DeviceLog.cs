namespace ModelLibrary
{
    /// <summary>
    /// 設備紀錄對應資料
    /// </summary>
    public class DeviceLog
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 紀錄編號
        /// </summary>
        public int? LOG_SN { get; set; }

        /// <summary>
        /// 設備紀錄詳細資料
        /// </summary>
        public LogDetail LOG_INFO { get; set; }
    }
}