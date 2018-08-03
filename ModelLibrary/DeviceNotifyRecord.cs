using System;

namespace ModelLibrary
{
    /// <summary>
    /// 設備通知記錄
    /// </summary>
    public class DeviceNotifyRecord
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 通知時間
        /// </summary>
        public DateTime? NOTIFY_TIME { get; set; }
    }
}
