using ModelLibrary.Enumerate;
using System;

namespace APIService.NotificationPusher
{
    /// <summary>
    /// 推播器建立工廠
    /// </summary>
    internal static class PusherFactory
    {
        /// <summary>
        /// 推播器實體建立
        /// </summary>
        /// <param name="destination">通知目的地</param>
        /// <returns></returns>
        public static IPusher CreateInstance(NotificationDestination destination)
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
                    throw new Exception($"尚未實作 { destination } 推播器");
            }

            return pusher;
        }
    }
}