using APIService.Director;
using ModelLibrary;
using ModelLibrary.Enumerate;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Bob Cacti 接收 API
    /// </summary>
    public class BobCactiController : ApiController
    {
        /// <summary>
        /// 告警 API
        /// </summary>
        /// <param name="raw">原始記錄</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(BobCactiRecord raw)
        {
            var detector = "Cacti";
            var logger = NLog.LogManager.GetLogger(detector);

            try
            {
                var record = JsonConvert.SerializeObject(raw);
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