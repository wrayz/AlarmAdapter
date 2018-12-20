using APIService.Director;
using BusinessLogic.NotificationStrategy;
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
            var logger = LogManager.GetLogger("Cacti");

            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                logger.Info(record);

                var director = new GenericRecordDirector(Detector.Cacti, record, DeviceType.N, new CactiNotifierStrategy());
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