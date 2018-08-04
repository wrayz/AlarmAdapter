using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 數據設備紀錄資料API
    /// </summary>
    public class RecordsController : ApiController
    {
        //商業邏輯
        private RecordLog_BLL _bll = new RecordLog_BLL();

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

                //轉換資料
                var content = Request.Content.ReadAsStringAsync().Result;
                var body = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                var list = _bll.DataConvert(body);

                //記錄檔
                FileDataLog(content);

                //監控參數取得
                var limit = _bll.GetLimit();

                //回應
                var response = "";
                //事件
                var type = EventType.Error;

                //資料儲存
                foreach (var record in list)
                {
                    if (string.IsNullOrEmpty(record.DEVICE_ID)) break;

                    //設備資料取得
                    var device = _bll.GetDeviceById(record);
                    //沒有設備編號
                    if (string.IsNullOrEmpty(device.DEVICE_SN)) break;

                    //設備編號
                    record.DEVICE_SN = device.DEVICE_SN;
                    //數據記錄新增
                    _bll.AddRecord(record);

                    //異常
                    if (((record.RECORD_TEMPERATURE > limit.MAX_TEMPERATURE_VAL || record.RECORD_TEMPERATURE < limit.MIN_TEMPERATURE_VAL) ||
                    (record.RECORD_HUMIDITY > limit.MAX_HUMIDITY_VAL || record.RECORD_HUMIDITY < limit.MIN_HUMIDITY_VAL)) &&
                     device.RECORD_STATUS == "N")
                    {
                        //紀錄資料
                        var data = new RecordLog
                        {
                            DEVICE_SN = record.DEVICE_SN,
                            RECORD_TIME = record.RECORD_TIME,
                            RECORD_TEMPERATURE = record.RECORD_TEMPERATURE,
                            RECORD_HUMIDITY = record.RECORD_HUMIDITY
                        };

                        _bll.ModifyRecordLog("Abnormal", data);

                        //事件
                        type = EventType.Error;
                    }
                    //恢復
                    else if ((record.RECORD_TEMPERATURE <= limit.MAX_TEMPERATURE_VAL && record.RECORD_TEMPERATURE >= limit.MIN_TEMPERATURE_VAL) &&
                             (record.RECORD_HUMIDITY <= limit.MAX_HUMIDITY_VAL && record.RECORD_HUMIDITY >= limit.MIN_HUMIDITY_VAL) &&
                             (device.RECORD_STATUS == "E" || device.RECORD_STATUS == "R"))
                    {
                        //紀錄資料
                        var data = new RecordLog
                        {
                            DEVICE_SN = record.DEVICE_SN,
                            RECOVER_TIME = record.RECORD_TIME
                        };

                        _bll.ModifyRecordLog("Recover", data);

                        //事件
                        type = EventType.Recover;
                    }

                    //設備數據記錄取得
                    var deviceRecord = _bll.GetDeviceRecord(device.DEVICE_SN);
                    var recordLog = _bll.GetRecordLog(deviceRecord.LOG_SN);
                    //推送通知
                    response += PushNotification(type, recordLog);
                }

                return Content(HttpStatusCode.OK, new APIResponse(response));
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

        /// <summary>
        /// 記錄檔
        /// </summary>
        /// <param name="content">原始資料</param>
        private void FileDataLog(string content)
        {
            //log時間
            var time = DateTime.Now;
            //記錄檔
            File.AppendAllText("C:/EyesFree/DataLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), content));
        }
    }
}