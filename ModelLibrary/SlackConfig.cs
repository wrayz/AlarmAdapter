namespace ModelLibrary
{
    /// <summary>
    /// 參數設定資料
    /// </summary>
    public class SlackConfig
    {
        /// <summary>
        /// Slack uri
        /// </summary>
        public string SLACK_URI { get; set; }

        /// <summary>
        /// Slack Token
        /// </summary>
        public string SLACK_TOKEN { get; set; }

        /// <summary>
        /// OutGoing Token
        /// </summary>
        public string OUTGOING_TOKEN { get; set; }
    }
}