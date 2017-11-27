using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 設備群組資料
    /// </summary>
    public class DeviceGroup
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 群組編號
        /// </summary>
        public string GROUP_SN { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GROUP_NAME { get; set; }

        /// <summary>
        /// 頻道ID
        /// </summary>
        public string CHANNEL_ID { get; set; }
    }
}