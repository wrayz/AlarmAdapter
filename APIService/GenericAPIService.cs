using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System.Web;

namespace APIService
{
    /// <summary>
    /// API 共用服務
    /// </summary>
    public static class GenericAPIService
    {
        /// <summary>
        /// 使用者資料取得 (By session)
        /// </summary>
        /// <returns></returns>
        public static UserLogin GetUserInfo()
        {
            var user = HttpContext.Current.Session["User"].ToString();
            return JsonConvert.DeserializeObject<UserLogin>(user);
        }

        /// <summary>
        /// 使用者資料取得 (By slack id)
        /// </summary>
        /// <param name="id">Slack id</param>
        /// <returns></returns>
        public static User GetUserInfo(string id)
        {
            var bll = GenericBusinessFactory.CreateInstance<User>();
            var user = bll.Get(new QueryOption(), new UserLogin(), new User { SLACK_ID = id });
            return user;
        }

        /// <summary>
        /// Slack Verification Token 驗證
        /// </summary>
        /// <param name="token">Slack Verification Token</param>
        /// <returns></returns>
        public static bool TokenValidate(string token)
        {
            var bll = GenericBusinessFactory.CreateInstance<SlackConfig>();
            var config = bll.Get(new QueryOption(), new UserLogin());
            return config.VERIFICATION_TOKEN == token;
        }
    }
}