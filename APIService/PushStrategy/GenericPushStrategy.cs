using APIService.NotificationPusher;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System.Collections.Generic;

namespace APIService.PushStrategy
{
    /// <summary>
    /// 推播策略
    /// </summary>
    internal abstract class GenericPushStrategy
    {
        private readonly List<NotificationDestination> _destinations;

        /// <summary>
        /// 建構式
        /// </summary>
        public GenericPushStrategy()
        {
            _destinations = GetDestinations();
        }

        /// <summary>
        /// 推播執行
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 推播通知目的地
        /// </summary>
        /// <param name="contents">通知內容清單</param>
        protected void PushDestination(List<PushContent> contents)
        {
            _destinations.ForEach(destination =>
            {
                var pusher = PusherFactory.CreateInstance(destination);
                contents.ForEach(content => pusher.Push(content));
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