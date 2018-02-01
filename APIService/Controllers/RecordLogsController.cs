using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 數據設備記錄 API
    /// </summary>
    public class RecordLogsController : ApiController
    {
        private RecordLog_BLL _bll = new RecordLog_BLL();

        /// <summary>
        /// 數據設備記錄更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData(RecordLog log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var token = LicenseLogic.Token;
                
                //登入資訊
                var user = GenericAPIService.GetUserInfo();

                //紀錄動作處理物件
                var bll = GenericBusinessFactory.CreateInstance<Device>();
                var condition = new Device
                {
                    DEVICE_SN = log.DEVICE_SN,
                    DEVICE_TYPE = "D",
                    IS_MONITOR = "Y",
                    RECORD_STATUS = "E"
                };
                //對應設備
                var device = bll.Get(new QueryOption(), user, condition);

                if (!string.IsNullOrEmpty(device.DEVICE_SN))
                {
                    _bll.ModifyLogs(log, user);

                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，或對應設備狀態不符"));
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }
    }
}