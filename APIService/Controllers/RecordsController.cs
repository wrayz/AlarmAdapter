using APIService.Model;
using BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 定時紀錄資料API
    /// </summary>
    public class RecordsController : ApiController
    {
        private Record_BLL _bll = new Record_BLL();

        /// <summary>
        /// 資料紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData()
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var content = Request.Content.ReadAsStringAsync().Result;
                var body = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                //紀錄資料
                _bll.ModifyRecords(body);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }
    }
}