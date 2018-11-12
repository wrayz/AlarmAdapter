using ModelLibrary.Enumerate;

namespace ModelLibrary
{
    /// <summary>
    /// 告警通知設定
    /// </summary>
    public class NotificationSetting
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 通知類型
        /// </summary>
        public NotificationType NOTICATION_TYPE { get; set; }

        /// <summary>
        /// 間隔類型
        /// </summary>
        public IntervalType INTERVAL_TYPE { get; set; }

        /// <summary>
        /// 關閉間隔時間
        /// </summary>
        public int? MUTE_INTERVAL { get; set; }
    }
}