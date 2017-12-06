namespace ModelLibrary
{
    /// <summary>
    /// 群組成員資料
    /// </summary>
    public class GroupMember
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        public string GROUP_SN { get; set; }

        /// <summary>
        /// 群組成員帳號
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 群組成員名稱
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 群組資訊
        /// </summary>
        public Group GROUP_INFO { get; set; }
    }
}