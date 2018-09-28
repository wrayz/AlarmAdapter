using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    public class CactiController : ApiController
    {
        public IHttpActionResult Post()
        {
            try
            {
                //原始資料
                var content = Request.Content.ReadAsStringAsync().Result;

                RecordRawData(content);

                var log = JsonConvert.DeserializeObject<Log>(content);

                var api = new DeviceLogsController();

                api.Post(log);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 原始資料儲存
        /// </summary>
        /// <param name="content">記錄資料</param>
        private void RecordRawData(string content)
        {
            var time = DateTime.Now;
            File.AppendAllText("C:/EyesFree/CactiRawData.txt", string.Format("{0}, {1}\n", time.ToString(), content));
        }
    }
}