using APIService.Model;
using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace APIService.Controllers
{
    public class SimpleLogsController : ApiController
    {
        private Device _device;

        /// <summary>
        /// LogMaster 紀錄
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(APILog log)
        {
            try
            {
                CheckLicense(log.LOG_TIME);

                if (string.IsNullOrEmpty(log.DEVICE_ID))
                    throw new HttpRequestException("資料未包含設備ID，請檢查資料內容");

                SetDevice(log.DEVICE_ID);
                RecordRawData(log);
                var simpleLog = SaveLog(log);

                //待查黑名單取得
                var blockIP = GetBlockIP(log.LOG_INFO);
                //是否回報黑名單
                if (new AbuseIpDbService(blockIP).IsReported())
                {
                    PushNotification(simpleLog);
                }

                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return Content(HttpStatusCode.Forbidden, new APIResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        private void PushNotification(SimpleLog simpleLog)
        {
            var notification = NotificationFactory.CreateInstance(DeviceType.S);
            var alarm = new Alarm { Time = simpleLog.ERROR_TIME, Content = simpleLog.ERROR_INFO };

            if (notification.IsNotification(alarm, _device.NOTIFICATION_SETTING, _device.NOTIFICATION_RECORDS))
            {
                var payload = notification.GetPayload(EventType.Error, _device.DEVICE_SN, simpleLog.LOG_SN);

                var service = new PushService(payload);
                service.PushNotification();

                var data = new NotificationRecord
                {
                    DEVICE_TYPE = "S",
                    DEVICE_SN = _device.DEVICE_SN,
                    LOG_SN = simpleLog.LOG_SN,
                    RECORD_CONTENT = simpleLog.ERROR_INFO
                };
                notification.Save(data);
            }
        }

        /// <summary>
        /// 告警記錄儲存
        /// </summary>
        /// <param name="log">告警記錄</param>
        /// <returns></returns>
        private SimpleLog SaveLog(APILog log)
        {
            var bll = new SimpleLog_BLL();

            var data = new SimpleLog
            {
                DEVICE_SN = _device.DEVICE_SN,
                ERROR_TIME = log.LOG_TIME,
                ERROR_INFO = log.LOG_INFO
            };

            //紀錄新增
            return bll.ModifyLog(data, "L");
        }

        /// <summary>
        /// 設備資料設置
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        private void SetDevice(string id)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            var option = new QueryOption { Relation = true, Plan = new QueryPlan { Join = "Records" } };
            var condition = new Device { DEVICE_ID = id, DEVICE_TYPE = "S", IS_MONITOR = "Y" };

            _device = bll.Get(option, new UserLogin(), condition);

            if (string.IsNullOrEmpty(_device.DEVICE_SN))
                throw new HttpRequestException("無對應設備，請確認設備為對應的類型[簡易數據設備]");
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
        /// 原始資料儲存
        /// </summary>
        /// <param name="log">記錄資料</param>
        private void RecordRawData(APILog log)
        {
            //log時間
            var time = DateTime.Now;
            //記錄檔
            File.AppendAllText("C:/EyesFree/SimpleLog.txt", string.Format("{0}, Log: {1}\n", time.ToString(), JsonConvert.SerializeObject(log)));
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        /// <param name="logtime">告警時間</param>
        private static void CheckLicense(DateTime? logtime)
        {
            if (LicenseLogic.Token == null)
                throw new HttpRequestException("License key 無效，請檢查License Key");

            var token = LicenseLogic.Token;

            if (!(logtime >= token.StartDate && logtime <= token.EndDate))
                throw new HttpRequestException("License key 已過期，請檢查License Key");
        }
    }
}