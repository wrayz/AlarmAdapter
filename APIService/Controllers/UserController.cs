using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 使用者 API
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// 使用者取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUser()
        {
            try
            {
                //User Info
                var login = GenericAPIService.GetUserInfo();

                //查詢參數
                var opt = new QueryOption { Relation = true, User = true };

                var bll = GenericBusinessFactory.CreateInstance<User>();
                var output = bll.Get(opt, login, null);

                return Ok(output);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }
    }
}