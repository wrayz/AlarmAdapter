using APIService.Director;
using ModelLibrary.Enumerate;
using System;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Logmaster 接收 API
    /// </summary>
    public class LogmasterController : ApiController
    {
        public IHttpActionResult Post()
        {
            var logger = NLog.LogManager.GetLogger("Logmaster");
            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                logger.Info(record);

                var director = new GenericRecordDirector(Detector.Logmaster, record, DeviceType.S);
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