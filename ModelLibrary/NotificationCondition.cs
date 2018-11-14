using ModelLibrary.Enumerate;

namespace ModelLibrary
{
    /// <summary>
    /// 通知條件
    /// </summary>
    public class NotificationCondition
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 通知類型
        /// </summary>
        public string NOTICATION_TYPE { get; set; }

        /// <summary>
        /// 間隔類型
        /// </summary>
        public string INTERVAL_LEVEL { get; set; }

        /// <summary>
        /// 間隔時間
        /// </summary>
        public int? INTERVAL_TIME { get; set; }
    }
}