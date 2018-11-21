using APIService.PushStrategy;
using BusinessLogic.Director;
using BusinessLogic.License;
using ModelLibrary.Enumerate;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 接收 Cacti API
    /// </summary>
    public class CactiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            var detector = "Cacti";
            var logger = NLog.LogManager.GetLogger(detector);

            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                logger.Info(record);

                var license = new LicenseBusinessLogic();
                license.Verify(DateTime.Now);

                var director = new WorkDirector(detector, record, DeviceType.N);
                director.Execute();

                GenericPushStrategy pusher = new MonitorPushStrategy(director.Monitors);
                pusher.Execute();

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