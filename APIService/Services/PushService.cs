﻿using BusinessLogic.RemoteNotification;
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
        // 手機通知服務位址
        private readonly string _imUrl = ConfigurationManager.AppSettings["im"];

        // 桌機通知服務位址
        private readonly string _socketUrl = ConfigurationManager.AppSettings["socket"];

        private NotificationContent _notificationContent;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="notificationContent">推送內容</param>
        public PushService(NotificationContent notificationContent)
        {
            _notificationContent = notificationContent;
        }

        /// <summary>
        /// 推播
        /// </summary>
        public void PushNotification()
        {
            PushIM().EnsureSuccessStatusCode();
            //PushDesktop().EnsureSuccessStatusCode();
        }

        /// <summary>
        /// 訊息推送IM
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage PushIM()
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //伺服器位址
                client.BaseAddress = new Uri(_imUrl);

                //內容
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(_notificationContent))
                });

                //post
                var response = client.PostAsync("im/eyesFreeLog", content).Result;

                return response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// 訊息推送桌機
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage PushDesktop()
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //內容
                var content = new StringContent(JsonConvert.SerializeObject(_notificationContent), Encoding.UTF8, "application/json");

                var response = client.PostAsync(_socketUrl, content).Result;

                return response.EnsureSuccessStatusCode();
            }
        }
    }
}