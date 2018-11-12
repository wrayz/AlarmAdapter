using APIService.Model;
using BusinessLogic;
using BusinessLogic.License;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Post(APILog log)
        {
            var logger = NLog.LogManager.GetLogger("Logmaster");

            try
            {
                logger.Info(JsonConvert.SerializeObject(log));

                CheckLicense(log.LOG_TIME);

                if (string.IsNullOrEmpty(log.DEVICE_ID))
                    throw new HttpRequestException("資料未包含設備ID，請檢查資料內容");

                _bll.InitLogmasterData(log);

                if (_bll.IsNotification())
                {
                    if (_bll.AbuseIpDbSetting.ABUSE_SCORE == 0)
                        Push();
                    else
                    {
                        var blockHole = GetBlockHole();

                        if (_bll.IsBlockHole(blockHole))
                            Push();
                    }
                }
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.Forbidden, new APIResponse(ex.Message));
            }
            catch (NotSupportedException)
            {
                Push();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Content(HttpStatusCode.InternalServerError, new APIResponse(ex.Message));
            }
        }

        /// <summary>
        /// 黑名單資訊取得
        /// </summary>
        /// <param name="abuseSetting"></param>
        /// <returns></returns>
        private BlockHole GetBlockHole()
        {
            var bll = GenericBusinessFactory.CreateInstance<BlockHole>();
            var blockHole = bll.Get(new QueryOption(), new UserLogin(), new BlockHole { IP_ADDRESS = _bll.IpAddress });

            //黑名單是否需要重新查詢分數
            if (_bll.CheckBlockCycle(blockHole))
            {
                var abuseService = new AbuseIpDbService(_bll.IpAddress);
                var reportedIp = abuseService.GetReportedIP(_bll.AbuseIpDbSetting);

                blockHole.IP_ADDRESS = _bll.IpAddress;
                blockHole.ABUSE_SCORE = reportedIp.abuseConfidenceScore;

                bll.Modify("Save", new UserLogin(), blockHole);
            }

            return blockHole;
        }

        /// <summary>
        /// 推播
        /// </summary>
        private void Push()
        {
            var payload = _bll.Notification.GetPayload(EventType.Error, _bll.Device.DEVICE_SN, _bll.SimpleLog.LOG_SN);

            var service = new PushService(payload);
            service.PushNotification();

            SaveNotification();
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        private void SaveNotification()
        {
            var data = new NotificationRecord
            {
                DEVICE_TYPE = "S",
                DEVICE_SN = _bll.Device.DEVICE_SN,
                LOG_SN = _bll.SimpleLog.LOG_SN,
                RECORD_CONTENT = _bll.SimpleLog.ERROR_INFO
            };

            _bll.Notification.Save(data);
        }

        /// <summary>
        /// License 檢查
        /// </summary>
        /// <param name="logtime">告警時間</param>
        private void CheckLicense(DateTime? logtime)
        {
            var license = new LicenseBusinessLogic();
            license.Verify(logtime);
        }
    }
}