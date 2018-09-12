using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 通知 API
    /// </summary>
    public class NotificationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST([FromBody]NotificationRecord notificationRecord)
        {
            return Ok();
        }
    }
}
