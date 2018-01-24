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
    }
}