using System;

namespace ModelLibrary
{
    /// <summary>
    /// 黑名單資訊
    /// </summary>
    public class BlockHole
    {
        /// <summary>
        /// 黑名單 IP
        /// </summary>
        public string IP_ADDRESS { get; set; }

        /// <summary>
        /// 黑名單分數
        /// </summary>
        public int? ABUSE_SCORE { get; set; }

        /// <summary>
        /// 請求時間
        /// </summary>
        public DateTime? REQUEST_TIME { get; set; }
    }
}