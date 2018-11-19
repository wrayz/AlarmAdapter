using BusinessLogic.RemoteNotification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIService.NotificationPusher
{
    /// <summary>
    /// 推播器管理站
    /// </summary>
    internal class PusherDirector
    {
        private readonly string _detector;
        private readonly List<NotificationContent> _contents;
        private readonly List<Monitor> _monitors;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="monitors">監控資訊清單</param>
        public PusherDirector(string detector, List<Monitor> monitors)
        {
            _detector = detector;

            _monitors = monitors.Where(x => x.IS_NOTIFICATION == "N").ToList();

            _contents = GetContents();
        }

        /// <summary>
        /// 推播執行
        /// </summary>
        /// <param name="destinations">通知目的地清單</param>
        public void Execute(List<NotificationDestination> destinations)
        {
            _contents.ForEach(content =>
            {
                destinations.ForEach(destination =>
                {
                    var pusher = GetPusher(destination);
                    pusher.Push(content);
                });
            });
        }

        /// <summary>
        /// 通知內容清單取得
        /// </summary>
        /// <returns></returns>
        private List<NotificationContent> GetContents()
        {
            var contents = new List<NotificationContent>();

            _monitors.ForEach(monitor =>
            {
                var content = RemoteNotificationFactory.CreateInstance(_detector, monitor);
                contents.Add(content);
            });

            return contents;
        }

        /// <summary>
        /// 取得推送器
        /// </summary>
        /// <param name="destination">通知目的地</param>
        /// <returns></returns>
        private IPusher GetPusher(NotificationDestination destination)
        {
            IPusher pusher;

            switch (destination)
            {
                case NotificationDestination.Mobile:
                    pusher = new MobilePusher();
                    break;

                case NotificationDestination.Desktop:
                    pusher = new DesktopPusher();
                    break;

                default:
                    throw new Exception($"尚未實作 { destination } 推送內容");
            }

            return pusher;
        }
    }
}