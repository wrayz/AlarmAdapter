using BusinessLogic.Event;
using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace APIService
{
    public class PushService
    {
        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _imUrl = ConfigurationManager.AppSettings["im"];

        private readonly string _socketUrl = ConfigurationManager.AppSettings["socket"];

        /// <summary>
        /// 系統名稱
        /// </summary>
        private readonly string _system = "EyesFree";

        private IMPayload _payload;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public PushService(string type, LogDetail log)
        {
            var enumType = (EventType)Enum.Parse(typeof(EventType), type);

            //推送訊息物件
            _payload = new IMPayload(_system, enumType, log);
        }

        /// <summary>
        /// 訊息推送IM
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage PushIM()
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //伺服器位址
                client.BaseAddress = new Uri(_imUrl);

                //內容
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(_payload))
                });

                //post
                var result = client.PostAsync("im/eyesFreeLog", content).Result;

                return result;
            }
        }

        /// <summary>
        /// 訊息推送桌機
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage PushDesktop()
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //內容
                var content = new StringContent(JsonConvert.SerializeObject(_payload), Encoding.UTF8, "application/json");

                //post
                var result = client.PostAsync(_socketUrl, content).Result;

                return result;
            }
        }
    }
}