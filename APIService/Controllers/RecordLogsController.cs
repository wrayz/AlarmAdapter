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
        //商業邏輯
        private RecordLog_BLL _bll = new RecordLog_BLL();

        /// <summary>
        /// 數據設備記錄更新
        /// </summary>
        /// <param name="log">數據記錄資料</param>
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

                //對應設備取得
                var device = _bll.GetDeviceBySn(log.DEVICE_SN);

                if (!string.IsNullOrEmpty(device.DEVICE_SN))
                {
                    //紀錄資料
                    var data = new RecordLog
                    {
                        LOG_SN = log.LOG_SN,
                        DEVICE_SN = log.DEVICE_SN,
                        USERID = user.USERID
                    };
                    _bll.ModifyRecordLog("Repair", data);

                    //數據記錄取得
                    var recordLog = _bll.GetRecordLog(log.LOG_SN);
                    //推送通知
                    var response = PushNotification(EventType.Repair, recordLog);

                    return Content(HttpStatusCode.OK, new APIResponse(response));
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


        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="type">資料動作</param>
        /// <param name="data">數據記錄資料</param>
        /// <returns></returns>
        private string PushNotification(EventType type, RecordLog data)
        {
            var result = "";
            var payload = new RecordPayload("EyesFree", type, data);
            var pushService = new PushService(payload);

            result += pushService.PushIM() + "\n";
            result += pushService.PushDesktop();

            return result;
        }
    }
}