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
    /// 簡易設備記錄商業邏輯
    /// </summary>
    public class SimpleLog_BLL
    {
        private IDataAccess<SimpleLog> _dao = GenericDataAccessFactory.CreateInstance<SimpleLog>();

        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _url = ConfigurationManager.AppSettings["host"];

        /// <summary>
        /// 系統名稱
        /// </summary>
        private readonly string _system = "EyesFree";

        /// <summary>
        /// 記錄新增
        /// </summary>
        /// <param name="log"></param>
        public void ModifyLog(SimpleLog log)
        {
            _dao.Modify("Insert", log);
        }

        /// <summary>
        /// 推送至IM
        /// </summary>
        /// <param name="origin">原始log資料</param>
        public void PushIM(SimpleLog origin)
        {
            var option = new QueryOption { Plan = new QueryPlan { Join = "Payload" } };
            var condition = new SimpleLog { DEVICE_SN = origin.DEVICE_SN, ERROR_TIME = origin.ERROR_TIME };
            var log = _dao.Get(option, condition);

            var fields = new List<Field>
            {
                new Field("主機名稱", log.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", log.DEVICE_INFO.DEVICE_ID, true),
                new Field("異常資訊", log.ERROR_INFO, false),
                new Field("異常時間", log.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true)
            };

            //訊息內容
            var payload = new Payload
            {
                LOG_SN = log.LOG_SN,
                LOG_TYPE = "S",
                DEVICE_SN = log.DEVICE_SN,
                SYSTEM_NAME = "EyesFree",
                BUTTON_STATUS = "N",
                COLOR = "danger",
                TITLE = "攝像機異常資訊",
                GROUP_LIST = log.GROUP_LIST,
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