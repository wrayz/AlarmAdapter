using Newtonsoft.Json;
using System;

namespace ModelLibrary
{
    /// <summary>
    /// 設備紀錄
    /// </summary>
    public class APILog
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        public string LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        [JsonProperty("sn")]
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備對應ID
        /// </summary>
        [JsonProperty("id")]
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 動作類型
        /// </summary>
        [JsonProperty("action")]
        public string ACTION_TYPE { get; set; }

        /// <summary>
        /// 紀錄資訊
        /// </summary>
        [JsonProperty("info")]
        public string LOG_INFO { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        [JsonProperty("time")]
        public DateTime? LOG_TIME { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [JsonProperty("user")]
        public string USERID { get; set; }
    }
}