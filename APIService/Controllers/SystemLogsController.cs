using APIService.Model;
using BusinessLogic;
using BusinessLogic.License;
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
    /// SystemLogs API
    /// </summary>
    public class SystemLogsController : ApiController
    {
        private Device _device;

        /// <summary>
        /// SystemLogs 紀錄
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostData()
        {
            var logger = NLog.LogManager.GetLogger("Camera");

            try
            {
                var content = Request.Content.ReadAsStringAsync().Result;
                var sourceIp = GetSourceIP();
                logger.Info(string.Format("{0} | {1}", sourceIp, content));

                CheckLicense();

                //資料解析
                var data = ParseData(content, sourceIp);
                //商業邏輯
                var bll = new SimpleLog_BLL();
                //儲存
                var simpleLog = bll.ModifyLog(data, "C");

                //通知
                PushNotification(simpleLog);

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
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 告警通知
        /// </summary>
        /// <param name="simpleLog"></param>
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
        /// 資料解析
        /// </summary>
        /// <param name="content">原始內容</param>
        /// <returns></returns>
        private SimpleLog ParseData(string content, string ip)
        {
            //原始訊息
            var info = content.Substring(content.IndexOf("=") + 1);
            //設備資訊設置
            SetDevice(ip);

            return new SimpleLog
            {
                DEVICE_SN = _device.DEVICE_SN,
                ERROR_TIME = DateTime.Now,
                ERROR_INFO = info
            };
        }

        /// <summary>
        /// 設備資訊設置
        /// </summary>
        /// <param name="ip">設備 IP</param>
        /// <returns></returns>
        private void SetDevice(string ip)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            var option = new QueryOption { Relation = true, Plan = new QueryPlan { Join = "Records" } };
            var condition = new Device { DEVICE_TYPE = "S", DEVICE_ID = ip, IS_MONITOR = "Y" };

            _device = bll.Get(option, new UserLogin(), condition);
        }

        /// <summary>
        /// 取得來源IP
        /// </summary>
        /// <returns></returns>
        private string GetSourceIP()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        private static void CheckLicense()
        {
            var license = new LicenseBusinessLogic();
            license.Verify(DateTime.Now);
        }
    }
}