using APIService.Director;
using BusinessLogic.NotificationStrategy;
using ModelLibrary;
using ModelLibrary.Enumerate;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Logmaster 接收 API
    /// </summary>
    public class LogmasterController : ApiController
    {
        public IHttpActionResult Post(ReceiveFormUrlEncoded raw)
        {
            var logger = NLog.LogManager.GetLogger("Logmaster");
            try
            {
                var record = JsonConvert.SerializeObject(raw);
                logger.Info(record);

                var director = new GenericRecordDirector(Detector.Logmaster, record, DeviceType.S, new LogmasterNotifierStrategy());
                director.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}