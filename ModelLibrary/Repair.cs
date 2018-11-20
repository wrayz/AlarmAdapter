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
        /// 記錄編號
        /// </summary>
        public string RECORD_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [User]
        public string USERID { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime? CREATE_TIME { get; set; }
    }
}