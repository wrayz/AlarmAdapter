﻿using APIService.Director;
using ModelLibrary.Enumerate;
using NLog;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Cacti 接收 API
    /// </summary>
    public class CactiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            var detector = "Cacti";
            var logger = LogManager.GetLogger(detector);

            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                logger.Info(record);

                var director = new GenericRecordDirector(detector, record, DeviceType.N);
                director.Execute();

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