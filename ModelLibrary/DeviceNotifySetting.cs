namespace ModelLibrary
{
    /// <summary>
    /// 設備通知設定
    /// </summary>
    public class DeviceNotifySetting
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 訊息種類
        /// </summary>
        public string MESSAGE_TYPE { get; set; }

        /// <summary>
        /// 關閉間隔時間
        /// </summary>
        public int? MUTE_INTERVAL { get; set; }
    }
}
