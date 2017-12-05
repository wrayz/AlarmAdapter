namespace APIService.Model
{
    /// <summary>
    /// Slack cmd API物件
    /// </summary>
    public class SlackCmdMessage
    {
        /// <summary>
        /// Verification Token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 團隊ID
        /// </summary>
        public string team_id { get; set; }

        /// <summary>
        /// 團隊名稱
        /// </summary>
        public string team_domain { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string enterprise_id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string enterprise_name { get; set; }

        /// <summary>
        /// 發送頻道ID
        /// </summary>
        public string channel_id { get; set; }

        /// <summary>
        /// 發送頻道名稱
        /// </summary>
        public string channel_name { get; set; }

        /// <summary>
        /// 使用者 slack id
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 使用者 slack 名稱
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 指令字串
        /// </summary>
        public string command { get; set; }

        /// <summary>
        /// 參數字串內容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 回應url
        /// </summary>
        public string response_url { get; set; }

        /// <summary>
        /// 觸發ID
        /// </summary>
        public string trigger_id { get; set; }
    }
}