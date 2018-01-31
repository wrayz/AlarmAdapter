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
        /// <summary>
        /// 數據設備記錄更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public IHttpActionResult PostData(Log log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                var token = LicenseLogic.Token;

                if (!(log.LOG_TIME >= token.StartDate && log.LOG_TIME <= token.EndDate))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 已過期，請檢查License Key"));

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
                    var recordBll = GenericBusinessFactory.CreateInstance<RecordLog>();
                    //設備修復
                    recordBll.Modify("Repair", user, new RecordLog { LOG_SN = log.LOG_SN, DEVICE_SN = log.DEVICE_SN }, null, false, true, new GenericExtand());

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