﻿using BusinessLogic;
using BusinessLogic.License;
using ModelLibrary;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 維修登記 API
    /// </summary>
    public class RepairController : ApiController
    {
        public IHttpActionResult Post(Repair repair)
        {
            var name = "Repair";
            var logger = NLog.LogManager.GetLogger(name);

            try
            {
                var license = new LicenseBusinessLogic();
                license.Verify(DateTime.Now);

                var login = GenericAPIService.GetUserInfo();
                var bll = GenericBusinessFactory.CreateInstance<Repair>();
                (bll as Repair_BLL).Save(repair, login);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}