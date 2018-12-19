using DataExpansion;
using System;

namespace ModelLibrary
{
    /// <summary>
    /// 登記維修資訊
    /// </summary>
    public class Repair
    {
        /// <summary>
        /// 維修編號
        /// </summary>
        public string REPAIR_SN { get; set; }

        /// <summary>
        /// 記錄編號
        /// </summary>
        public string RECORD_SN { get; set; }

        /// <summary>
        /// 記錄編號（ TODO: 等 IM 改完拿掉）
        /// </summary>
        public string LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目名稱
        /// </summary>
        public string TARGET_NAME { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [User]
        public string USERID { get; set; }

        /// <summary>
        /// 登記時間
        /// </summary>
        public DateTime? REGISTER_TIME { get; set; }

        /// <summary>
        /// 設備資訊
        /// </summary>
        public Device DEVICE { get; set; }
    }
}