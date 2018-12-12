using ModelLibrary;
using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 通知內容實體建立工廠
    /// </summary>
    public static class NotificationContentFactory
    {
        /// <summary>
        /// 通知內容實體建立
        /// </summary>
        /// <param name="notification">通知資訊</param>
        /// <returns></returns>
        public static GenericContent CreateInstance(Notification notification)
        {
            var type = Enum.Parse(typeof(DeviceType), notification.DEVICE.DEVICE_TYPE);
            GenericContent content;

            switch (type)
            {
                case DeviceType.N:
                    content = new CactiContent(notification);
                    break;

                default:
                    throw new Exception($"尚未實作 { type } 通知內容");
            }

            return content;
        }
    }
}