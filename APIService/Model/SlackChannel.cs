using Newtonsoft.Json;

namespace APIService.Model
{
    /// <summary>
    /// Slack 頻道資訊API物件
    /// </summary>
    public class SlackChannel
    {
        /// <summary>
        /// 頻道ID
        /// </summary>
        [JsonProperty("channel")]
        public string CHANNEL_ID { get; set; }

        /// <summary>
        /// 頻道名稱
        /// </summary>
        [JsonProperty("name")]
        public string CHANNEL_NAME { get; set; }
    }
}