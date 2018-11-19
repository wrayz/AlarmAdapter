using BusinessLogic.RemoteNotification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace APIService.NotificationPusher
{
    /// <summary>
    /// 手機推播器
    /// </summary>
    internal class MobilePusher : IPusher
    {
        private readonly string _url;

        public MobilePusher()
        {
            _url = ConfigurationManager.AppSettings["im"];
        }

        public HttpResponseMessage Push(NotificationContent content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);

                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(content))
                });

                var response = client.PostAsync("im/eyesFreeLog", formContent).Result;

                return response.EnsureSuccessStatusCode();
            }
        }
    }
}