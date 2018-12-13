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
    [RoutePrefix("api/devicelogs")]
    public class BobCactiController : ApiController
    {
        /// <summary>
        /// 告警 API
        /// </summary>
        /// <param name="raw">原始記錄</param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(ReceiveFormUrlEncoded raw)
        {
            var logger = NLog.LogManager.GetLogger("Cacti");

            try
            {
                var record = JsonConvert.SerializeObject(raw);
                logger.Info(record);

                var director = new GenericRecordDirector(Detector.BobCacti, record, DeviceType.N);
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