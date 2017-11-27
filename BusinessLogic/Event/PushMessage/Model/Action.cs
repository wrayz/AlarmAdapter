using Newtonsoft.Json;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 動作按鈕 (Slack)
    /// </summary>
    public class Action
    {
        /// <summary>
        /// 按鈕名稱
        /// </summary>
        [JsonProperty("name")]
        public string BUTTON_NAME { get; set; }

        /// <summary>
        /// 按鈕顯示文字
        /// </summary>
        [JsonProperty("text")]
        public string BUTTON_TEXT { get; set; }

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

        /// <summary>
        /// 按鈕外觀
        /// </summary>
        [JsonProperty("style")]
        public string BUTTON_STYLE { get; set; }
    }
}