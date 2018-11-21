using APIService.NotificationPusher;
using BusinessLogic.NotificationStrategy;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;

namespace APIService.Strategy
{
    /// <summary>
    /// 推播策略
    /// </summary>
    internal abstract class PushStrategy
    {
        private readonly List<NotificationDestination> _destinations;

        /// <summary>
        /// 建構式
        /// </summary>
        public PushStrategy()
        { 
            _destinations = GetDestinations();
        }

        /// <summary>
        /// 推播執行
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 通知內容初始化
        /// </summary>
        protected abstract void InitContent();

        /// <summary>
        /// 推播通知目的地
        /// </summary>
        /// <param name="content">通知內容</param>
        protected void PushDestination(ContentStrategy content)
        {
            _destinations.ForEach(destination =>
            {
                var pusher = PusherFactory.CreateInstance(destination);
                pusher.Push(content);
            });
        }

        /// <summary>
        /// 通知目的地清單
        /// </summary>
        /// <returns></returns>
        private List<NotificationDestination> GetDestinations()
        {
            return new List<NotificationDestination>
            {
                NotificationDestination.Mobile
            };
        }
    }
}