using APIService.Model;
using BusinessLogic;
using BusinessLogic.Notification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// 一般網路設備告警 API
    /// </summary>
    public class DeviceLogsController : ApiController
    {
        //一般網路設備商業邏輯
        private DeviceLog_BLL _bll;
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
            var type = (AlarmType)Enum.Parse(typeof(AlarmType), log.ACTION_TYPE);

            try
            {
                CheckContent(log);
                CheckLicense(log.LOG_TIME);

                Process(log, type);

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

        /// <summary>
        /// 告警處理
        /// </summary>
        /// <param name="data">告警資料</param>
        /// <param name="type">告警類型</param>
        private void Process(Log data, AlarmType type)
        {
            _bll = new DeviceLog_BLL();
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
                    throw new Exception("無此告警類型");
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

            if (_notification.IsNotification(alarm, device.NOTIFICATION_SETTING, device.NOTIFICATION_RECORDS))
            {
#if Release
                PushNotification(log);
#endif
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
        /// 內容檢查
        /// </summary>
        /// <param name="log">告警訊息</param>
        private void CheckContent(Log log)
        {
            //設備ID檢查
            if (string.IsNullOrEmpty(log.DEVICE_ID))
                throw new HttpRequestException("資料未包含設備ID，請檢查資料");
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        /// <param name="logtime">告警訊息時間</param>
        private void CheckLicense(DateTime? logtime)
        {
            //來源IP驗證
            //if (!Validate())
            //    return Content(HttpStatusCode.Unauthorized, new APIResponse("來源IP未認證"));

            if (LicenseLogic.Token == null)
                throw new HttpRequestException("License key 無效，請檢查License Key");

            var token = LicenseLogic.Token;

            if (!(logtime >= token.StartDate && logtime <= token.EndDate))
                throw new HttpRequestException("License key 已過期，請檢查License Key");
        }

        /// <summary>
        /// 來源IP驗證
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            return HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.UserHostAddress;
        }
    }
}