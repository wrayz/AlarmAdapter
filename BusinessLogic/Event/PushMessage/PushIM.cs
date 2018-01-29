using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 訊息推送處理物件(IM)
    /// </summary>
    public class PushIM : IPushAction
    {
        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _url = ConfigurationManager.AppSettings["host"];

        /// <summary>
        /// 系統名稱
        /// </summary>
        private readonly string _system = "EyesFree";

        /// <summary>
        /// 推送訊息物件
        /// </summary>
        private IMPayload _payload;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public PushIM(EventType type, LogDetail log)
        {
            //推送訊息物件
            _payload = new IMPayload(_system, type, log);
        }

        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> PushMessage()
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //伺服器位址
                client.BaseAddress = new Uri(_url);

                //內容
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(_payload))
                });

                //post
                var result = await client.PostAsync("im/eyesFreeLog", content);

                return result.StatusCode;
            }
        }
    }
}