using APIService.PushStrategy;
using BusinessLogic;
using BusinessLogic.License;
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
    /// 維修登記 API
    /// </summary>
    [RoutePrefix("")]
    public class RepairController : ApiController
    {
        [Route("api/devices")]
        public IHttpActionResult Post(Repair repair)
        {
            var name = "Repair";
            var logger = NLog.LogManager.GetLogger(name);

            try
            {
                var license = new LicenseBusinessLogic();
                license.Verify(DateTime.Now);

                var output = Save(repair);

                GenericPushStrategy pusher = new RepairPushStrategy(output);
                pusher.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 舊版溫濕度計維修登記
        /// </summary>
        /// <param name="repair"></param>
        /// <returns></returns>
        [Route("api/recordlogs")]
        public IHttpActionResult PostOld(Repair repair)
        {
            var name = "Repair";
            var logger = NLog.LogManager.GetLogger(name);

            try
            {
                var license = new LicenseBusinessLogic();
                license.Verify(DateTime.Now);

                var output = Save(repair);

                GenericPushStrategy pusher = new RepairPushStrategy(output);
                pusher.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="repair">維修登記資訊</param>
        private Repair Save(Repair repair)
        {
            var login = GetUserInfo();
            var bll = GenericBusinessFactory.CreateInstance<Repair>();
            return (bll as Repair_BLL).Save(repair, login);
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