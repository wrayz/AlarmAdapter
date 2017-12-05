using Newtonsoft.Json;

namespace APIService.Model
{
    /// <summary>
    /// Slack 按鈕資訊API物件
    /// </summary>
    public class SlackButton
    {
        /// <summary>
        /// 按鈕名稱
        /// </summary>
        [JsonProperty("name")]
        public string BUTTON_NAME { get; set; }

        /// <summary>
        /// 按鈕類型
        /// </summary>
        [JsonProperty("type")]
        public string BUTTON_TYPE { get; set; }

        /// <summary>
        /// 按鈕值
        /// </summary>
        [JsonProperty("value")]
        public string BUTTON_VALUE { get; set; }
    }
}