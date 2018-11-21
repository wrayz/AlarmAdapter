using APIService.NotificationDirector;
using APIService.NotificationStrategy;
using APIService.PushStrategy;
using BusinessLogic.Director;
using BusinessLogic.License;
using ModelLibrary.Enumerate;
using NLog;
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
        private const string _detector = "Cacti";

        [HttpPost]
        public IHttpActionResult Post()
        {
            var logger = LogManager.GetLogger(_detector);

            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                logger.Info(record);

                var license = new LicenseBusinessLogic();
                license.Verify(DateTime.Now);

                var workDirector = new WorkDirector(_detector, record, DeviceType.N);
                workDirector.Execute();

                var strategy = new GenericNotifier();
                var notificationDirector = new NotificationStationDirector(strategy);
                notificationDirector.Execute();

                var pusher = new MonitorPushStrategy(notificationDirector.Monitors);
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