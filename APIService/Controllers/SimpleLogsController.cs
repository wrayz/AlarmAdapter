using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    public class SimpleLogsController : ApiController
    {
        private SimpleLog_BLL _bll = new SimpleLog_BLL();

        /// <summary>
        /// LogMaster 紀錄
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData(APILog log)
        {
            try
            {
                if (LicenseLogic.Token == null)
                {
                    return Content(HttpStatusCode.Forbidden, new APIResponse("License key 無效，請檢查License Key"));
                }

                //log時間
                var time = DateTime.Now;
                //記錄檔
                File.AppendAllText("C:/EyesFree/SimpleLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), JsonConvert.SerializeObject(log)));

                //設備ID檢查
                if (string.IsNullOrEmpty(log.DEVICE_ID))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("資料未包含設備ID，請檢查資料內容"));

                //對應設備取得
                var device = GetDevice(log.DEVICE_ID);

                if (string.IsNullOrEmpty(device.DEVICE_SN))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，請確認設備為對應的類型[簡易數據設備]"));

                var simpleLog = new SimpleLog
                {
                    DEVICE_SN = device.DEVICE_SN,
                    ERROR_TIME = log.LOG_TIME,
                    ERROR_INFO = log.LOG_INFO
                };

                //紀錄新增
                var insertedLog = _bll.ModifyLog(simpleLog);
                //推送至IM
                _bll.PushIM(insertedLog);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 設備資料取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        private Device GetDevice(string id)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return bll.Get(new QueryOption(), new UserLogin(), new Device { DEVICE_ID = id, DEVICE_TYPE = "S", IS_MONITOR = "Y" });
        }
    }
}