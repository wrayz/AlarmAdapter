using APIService.Director;
using BusinessLogic.NotificationStrategy;
using ModelLibrary.Enumerate;
using System;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 溫濕度計告警接收 API
    /// </summary>
    [RoutePrefix("api/records")]
    public class IfaceController : ApiController
    {
        [Route("")]
        public IHttpActionResult POST()
        {
            var logger = NLog.LogManager.GetLogger("iFace");

            try
            {
                var record = Request.Content.ReadAsStringAsync().Result;
                var sourceIp = GetSourceIP();
                logger.Info(sourceIp + "|" + record);

                var director = new GenericRecordDirector(Detector.iFace, record, DeviceType.D, new GenericNotifierStrategy(), sourceIp);
                director.Execute();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 來源 IP 取得
        /// </summary>
        /// <returns></returns>
        private string GetSourceIP()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
    }
}