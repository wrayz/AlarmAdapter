namespace ModelLibrary.Generic
{
    /// <summary>
    /// 使用者登入
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 員工帳號
        /// </summary>
        public string EMP_NO { get; set; }

        /// <summary>
        /// 組織編號
        /// </summary>
        public string ORG_SN { get; set; }

        /// <summary>
        /// 使用者角色
        /// </summary>
        public string USER_ROLE { get; set; }
    }
}