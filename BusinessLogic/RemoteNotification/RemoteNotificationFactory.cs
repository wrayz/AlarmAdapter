using ModelLibrary;
using System;

namespace BusinessLogic.RemoteNotification
{
    /// <summary>
    /// 遠端通知內容產生器
    /// </summary>
    public static class RemoteNotificationFactory
    {
        /// <summary>
        /// 通知內容建立工廠
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="monitor">監控資訊</param>
        /// <returns></returns>
        public static NotificationContent CreateInstance(string detector, Monitor monitor)
        {
            NotificationContent content;

            switch (detector)
            {
                case "Cacti":
                    content = new CactiContent(monitor);
                    break;

                default:
                    throw new Exception($"尚未實作 { detector } 通知推送內容");
            }

            return content;
        }
    }
}