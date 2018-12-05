using APIService.Model;
using BusinessLogic;
using BusinessLogic.AlarmNotification;
using BusinessLogic.License;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 一般網路設備告警 API
    /// </summary>
    public class DeviceLogsController : ApiController
    {
        //一般網路設備商業邏輯
        private Log_BLL _bll;

        //通知商業邏輯
        private INotification _notification;

        /// <summary>
        /// 告警 API
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(Log log)
        {
            var logger = NLog.LogManager.GetLogger("Cacti");

            try
            {
                logger.Info(JsonConvert.SerializeObject(log));

                CheckValid(log);

                Process(log);

                return Ok();
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.Forbidden, new APIResponse(ex.Message));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// 告警處理
        /// </summary>
        /// <param name="data">告警資料</param>
        /// <param name="type">告警類型</param>
        private void Process(Log data)
        {
            var type = (AlarmType)Enum.Parse(typeof(AlarmType), data.ACTION_TYPE);

            _bll = new Log_BLL();
            _notification = NotificationFactory.CreateInstance(DeviceType.N);

            switch (type)
            {
                case AlarmType.Error:
                    ErrorProcess(data);
                    break;

                case AlarmType.Recover:
                    RecoverProcess(data);
                    break;

                default:
                    throw new Exception($"無 { data.ACTION_TYPE } 告警類型");
            }
        }

        /// <summary>
        /// 異常處理
        /// </summary>
        /// <param name="data">記錄資料</param>
        private void ErrorProcess(Log data)
        {
            var log = _bll.SaveErrorLog(data);
            var alarm = new Alarm { Time = data.LOG_TIME, Content = log.LOG_INFO };
            var device = GetDevice(log.DEVICE_SN);

            if (_notification.IsNotification(alarm, device.NOTIFICATION_CONDITION, device.NOTIFICATION_RECORDS))
            {
                PushNotification(log);
                SaveNotifyRecord(log);
            }
        }

        /// <summary>
        /// 恢復處理
        /// </summary>
        /// <param name="data">記錄資料</param>
        private void RecoverProcess(Log data)
        {
            var log = _bll.SaveRecoveryLog(data);
            if (CheckNotification(log))
                PushNotification(log);
        }

        /// <summary>
        /// 推送通知
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void PushNotification(Log log)
        {
            var type = (EventType)Enum.Parse(typeof(EventType), log.ACTION_TYPE);
            var payload = _notification.GetPayload(type, log.DEVICE_SN, log.LOG_SN);
            var push = new PushService(payload);

            push.PushNotification();
        }

        /// <summary>
        /// 設備資訊取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        private Device GetDevice(string sn)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            var option = new QueryOption { Relation = true, Plan = new QueryPlan { Join = "Records" } };
            var condition = new Device { DEVICE_SN = sn, DEVICE_TYPE = "N" };

            return bll.Get(option, new UserLogin(), condition);
        }

        /// <summary>
        /// 告警通知是否存在
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        private bool CheckNotification(Log log)
        {
            var bll = GenericBusinessFactory.CreateInstance<NotificationRecord>();
            var condition = new NotificationRecord
            {
                DEVICE_TYPE = "N",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN
            };

            return bll.IsExists(new QueryOption(), new UserLogin(), condition);
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void SaveNotifyRecord(Log log)
        {
            var data = new NotificationRecord
            {
                DEVICE_TYPE = "N",
                DEVICE_SN = log.DEVICE_SN,
                LOG_SN = log.LOG_SN,
                RECORD_CONTENT = log.LOG_INFO
            };

            _notification.Save(data);
        }

        /// <summary>
        /// 合法性檢查
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void CheckValid(Log log)
        {
            if (string.IsNullOrEmpty(log.DEVICE_ID))
                throw new HttpRequestException($"EyesFree 尚未設置設備ID { log.DEVICE_ID }，請檢查設定");

            var license = new LicenseBusinessLogic();
            license.Verify(log.LOG_TIME);
        }
    }
}