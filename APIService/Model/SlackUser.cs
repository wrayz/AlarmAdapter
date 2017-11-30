using Newtonsoft.Json;

namespace APIService.Model
{
    /// <summary>
    /// Slack 使用者資訊
    /// </summary>
    public class SlackUser
    {
        /// <summary>
        /// 使用者ID
        /// </summary>
        [JsonProperty("id")]
        public string SLACK_ID { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        [JsonProperty("name")]
        public string SLACK_NAME { get; set; }
    }
}