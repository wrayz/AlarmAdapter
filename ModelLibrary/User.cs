using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 使用者資料
    /// </summary>
    public class User
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 郵件帳號
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// Slack id
        /// </summary>
        public string SLACK_ID { get; set; }

        /// <summary>
        /// 帳號類型
        /// </summary>
        public string USER_TYPE { get; set; }
    }
}