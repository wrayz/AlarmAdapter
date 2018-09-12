using System;

namespace ModelLibrary
{
    /// <summary>
    /// 告警
    /// </summary>
    public class Alarm
    {
        /// <summary>
        /// 時間
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Content { get; set; }
    }
}