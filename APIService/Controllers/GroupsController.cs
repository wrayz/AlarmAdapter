using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 群組 API
    /// </summary>
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        /// <summary>
        /// 取得使用者所有群組清單
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult GetGroups()
        {
            try
            {
                //User Info
                var login = GetUserInfo();

                //查詢參數
                var opt = new QueryOption { Relation = true, User = true };

                var bll = GenericBusinessFactory.CreateInstance<GroupMember>();
                var output = bll.GetList(opt, login, null);

                return Ok(output);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 取得群組成員清單
        /// </summary>
        /// <param name="gorupId">群組編號</param>
        /// <returns></returns>
        [Route("{gorupId}/members")]
        public IHttpActionResult GetGroupMembers(string gorupId)
        {
            try
            {
                //User Info
                var login = GetUserInfo();

                //查詢參數
                var opt = new QueryOption();
                var condition = new GroupMember { GROUP_SN = gorupId };

                var bll = GenericBusinessFactory.CreateInstance<GroupMember>();
                var output = bll.GetList(opt, login, condition);

                return Ok(output);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 取得群組設備清單
        /// </summary>
        /// <param name="gorupId">群組編號</param>
        /// <returns></returns>
        [Route("{gorupId}/devices")]
        public IHttpActionResult GetGroupDevices(string gorupId)
        {
            try
            {
                //User Info
                var login = GetUserInfo();

                //查詢參數
                var opt = new QueryOption { Relation = true, Plan = new QueryPlan() { Join = "Detail" } };

                var condition = new GroupService { GROUP_SN = gorupId };

                var bll = GenericBusinessFactory.CreateInstance<GroupService>();
                var output = bll.GetList(opt, login, condition);

                return Ok(output);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 使用者資料取得 (By session)
        /// </summary>
        /// <returns></returns>
        public UserLogin GetUserInfo()
        {
            var user = HttpContext.Current.Session["User"].ToString();
            return JsonConvert.DeserializeObject<UserLogin>(user);
        }
    }
}