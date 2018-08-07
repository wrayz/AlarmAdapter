using BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace APIService
{
    /// <summary>
    /// 推播服務
    /// </summary>
    public class PushService
    {
        /// <summary>
        /// 手機通知服務位址
        /// </summary>
        private readonly string _imUrl = ConfigurationManager.AppSettings["im"];

        /// <summary>
        /// 桌機通知服務位址
        /// </summary>
        private readonly string _socketUrl = ConfigurationManager.AppSettings["socket"];

        private Payload _payload;

        /// <summary>
        /// 建構式
        /// </summary>
        public PushService(Payload payload)
        {
            _payload = payload;
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
                return client.PostAsync("im/eyesFreeLog", content).Result;
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

                return client.PostAsync(_socketUrl, content).Result;
            }
        }
    }
}