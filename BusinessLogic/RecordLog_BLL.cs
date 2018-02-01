using BusinessLogic.Event;
using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// 數據設備異常資訊商業邏輯
    /// </summary>
    public class RecordLog_BLL
    {
        private IDataAccess<RecordLog> _dao = GenericDataAccessFactory.CreateInstance<RecordLog>();

        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _url = ConfigurationManager.AppSettings["im"];

        /// <summary>
        /// 系統名稱
        /// </summary>
        private readonly string _system = "EyesFree";

        /// <summary>
        /// 紀錄資料
        /// </summary>
        /// <param name="log"></param>
        public void ModifyLogs(RecordLog log, UserLogin user)
        {
            //修復紀錄
            _dao.Modify("Repair", new RecordLog { LOG_SN = log.LOG_SN, DEVICE_SN = log.DEVICE_SN, USERID = user.USERID });

            var recordLog = _dao.Get(new QueryOption { Plan = new QueryPlan { Join = "Payload" } }, new RecordLog { LOG_SN = log.LOG_SN });

            var fields = new List<Field>
            {
                new Field("主機名稱", recordLog.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", recordLog.DEVICE_INFO.DEVICE_ID, true),
                new Field("處理時間", recordLog.REPAIR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("處理人員", recordLog.USER_INFO.USER_NAME, true)
            };

            var payload = new Payload
            {
                LOG_SN = recordLog.LOG_SN,
                LOG_TYPE = "D",
                DEVICE_SN = recordLog.DEVICE_SN,
                SYSTEM_NAME = _system,
                BUTTON_STATUS = "R",
                COLOR = "warning",
                TITLE = "溫溼度設備處理資訊",
                GROUP_LIST = recordLog.GROUP_LIST,
                FIELD_LIST = fields
            };

            //推送訊息
            PushMessage(payload);
        }

        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        private async Task<HttpStatusCode> PushMessage(Payload payload)
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //伺服器位址
                client.BaseAddress = new Uri(_url);

                //內容
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(payload))
                });

                //post
                var result = await client.PostAsync("im/eyesFreeLog", content);

                return result.StatusCode;
            }
        }
    }
}