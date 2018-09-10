using APIService.Model;
using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Text.RegularExpressions;
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
                //File.AppendAllText("C:/EyesFree/SimpleLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), JsonConvert.SerializeObject(log)));

                //設備ID檢查
                if (string.IsNullOrEmpty(log.DEVICE_ID))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("資料未包含設備ID，請檢查資料內容"));

                //對應設備取得
                var device = GetDevice(log.DEVICE_ID);
                log.DEVICE_SN = device.DEVICE_SN;

                if (string.IsNullOrEmpty(device.DEVICE_SN))
                    return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，請確認設備為對應的類型[簡易數據設備]"));

                var simpleLog = new SimpleLog
                {
                    DEVICE_SN = device.DEVICE_SN,
                    ERROR_TIME = log.LOG_TIME,
                    ERROR_INFO = log.LOG_INFO
                };

                //紀錄新增
                var insertedLog = _bll.ModifyLog(simpleLog, "L");
                //記錄編號
                log.LOG_SN = insertedLog.LOG_SN;

                //待查黑名單取得
                var blockIP = GetBlockIP(log.LOG_INFO);
                //是否回報黑名單
                if (new AbuseIpDbService(blockIP).IsReported())
                {
                    //間隔通知
                    //PushInterval(log, device);
                }

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
            return bll.Get(new QueryOption { Relation = true }, new UserLogin(), new Device { DEVICE_ID = id, DEVICE_TYPE = "S", IS_MONITOR = "Y" });
        }

        /// <summary>
        /// 待查黑名單取得
        /// </summary>
        /// <param name="info">Logmaster 訊息</param>
        /// <returns></returns>
        private string GetBlockIP(string info)
        {
            var list = Regex.Split(info, "detect block ip ");
            return list[1];
        }

        /// <summary>
        /// 間隔通知
        /// </summary>
        /// <param name="log">異常記錄</param>
        /// <param name="device">設備資訊</param>
        //private void PushInterval(APILog log, Device device)
        //{
        //    //詳細記錄資訊取得
        //    var detail = _bll.GetSimpleLog(new SimpleLog { LOG_SN = log.LOG_SN, DEVICE_SN = log.DEVICE_SN });
        //    //通知服務
        //    var payload = new SimplePayload(detail);
        //    var pushService = new PushService(payload);

        //    //檢查結果
        //    var check = false;
        //    //設定間隔訊息類型
        //    var messageType = (MessageType)Enum.Parse(typeof(MessageType), device.NOTIFY_SETTING.MESSAGE_TYPE);

        //    switch (messageType)
        //    {
        //        case MessageType.A:
        //            check = _bll.CheckAllMessageInterval(log, device.NOTIFY_SETTING);
        //            break;

        //        case MessageType.S:
        //            check = _bll.CheckSameMessageInterval(log, device.NOTIFY_SETTING);
        //            break;
        //    }

        //    if (check)
        //    {
        //        //通知
        //        pushService.PushNotification();
        //        //通知記錄儲存
        //        SaveRecord(log);
        //    }
        //}

        /// <summary>
        /// 儲存通知記錄
        /// </summary>
        /// <param name="log">設備記錄</param>
        //private void SaveRecord(APILog log)
        //{
        //    var bll = new DeviceNotifyRecord_BLL();
        //    //通知記錄物件
        //    var record = new DeviceNotifyRecord
        //    {
        //        DEVICE_SN = log.DEVICE_SN,
        //        RECORD_ID = log.LOG_SN
        //    };
        //    //通知記錄更新
        //    bll.SaveNotifyRecord(record);
        //}
    }
}