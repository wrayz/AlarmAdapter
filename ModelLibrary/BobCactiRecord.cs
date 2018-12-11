using Newtonsoft.Json;
using System;

namespace ModelLibrary
{
    /// <summary>
    /// Bob Cacti 接收格式
    /// </summary>
    public class BobCactiRecord
    {
        /// <summary>
        /// 設備識別碼
        /// </summary>
        [JsonProperty("id")]
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 動作類型
        /// </summary>
        [JsonProperty("action")]
        public string ACTION_TYPE { get; set; }

        /// <summary>
        /// 記錄訊息
        /// </summary>
        [JsonProperty("info")]
        public string LOG_INFO { get; set; }

        /// <summary>
        /// 記錄時間
        /// </summary>
        [JsonProperty("time")]
        public DateTime LOG_TIME { get; set; }

        /// <summary>
        /// 監控項目
        /// </summary>
        [JsonProperty("target")]
        public string TARGET_NAME
        {
            get
            {
                return "Ping";
            }
        }
    }
}