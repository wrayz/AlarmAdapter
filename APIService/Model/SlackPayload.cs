using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APIService.Model
{
    /// <summary>
    /// 訊息API物件(Slack interactive message 內容)
    /// </summary>
    public class SlackPayload
    {
        /// <summary>
        /// 訊息類型
        /// </summary>
        [JsonProperty("type")]
        public string MESSAGE_TYPE { get; set; }

        /// <summary>
        /// 按鈕清單
        /// </summary>
        [JsonProperty("actions")]
        public List<SlackButton> ACTION_LIST { get; set; }

        /// <summary>
        /// 訊息辨識ID(紀錄編號)
        /// </summary>
        [JsonProperty("callback_id")]
        public string CALLBACK_ID { get; set; }

        ///// <summary>
        ///// 團隊資訊
        ///// </summary>
        //[JsonProperty("team")]
        //public Team TEAM_INFO { get; set; }

        /// <summary>
        /// 頻道資訊
        /// </summary>
        [JsonProperty("channel")]
        public SlackChannel CHANNEL_INFO { get; set; }

        /// <summary>
        /// 使用者資訊
        /// </summary>
        [JsonProperty("user")]
        public SlackUser USER_INFO { get; set; }

        /// <summary>
        /// 動作時間標籤
        /// </summary>
        [JsonProperty("action_ts")]
        public string ACTION_TS { get; set; }

        /// <summary>
        /// 訊息時間標籤
        /// </summary>
        [JsonProperty("message_ts")]
        public string MESSAGE_TS { get; set; }

        /// <summary>
        /// 卡片編號
        /// </summary>
        [JsonProperty("attachment_id")]
        public string ATTACHMENT_ID { get; set; }

        /// <summary>
        /// 訊息token
        /// </summary>
        [JsonProperty("token")]
        public string SLACK_TOKEN { get; set; }

        ///// <summary>
        ///// 原始內容
        ///// </summary>
        //[JsonProperty("original_message")]
        //public string original_message { get; set; }

        /// <summary>
        /// 回應網址
        /// </summary>
        [JsonProperty("response_url")]
        public string RESPONSE_URL { get; set; }

        /// <summary>
        /// Request trigger id
        /// </summary>
        [JsonProperty("trigger_id")]
        public string TRIGGER_ID { get; set; }
    }
}