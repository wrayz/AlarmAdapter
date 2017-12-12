﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.Event
{
    /// <summary>
    /// Slack cmd response object
    /// </summary>
    public class CmdResponse
    {
        /// <summary>
        /// 回應類型 (in_channel - 公共, ephemeral - 私人)
        /// </summary>
        [JsonProperty("response_type", NullValueHandling = NullValueHandling.Ignore)]
        public string RESPONSE_TYPE { get; set; }

        /// <summary>
        /// 訊息內容
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string TEXT_CONTENT { get; set; }

        /// <summary>
        /// 推送訊息卡片
        /// </summary>
        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public List<Attachment> ATTACHMENT_LIST { get; set; }
    }
}