using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="list">通知資訊清單</param>
        /// <returns></returns>
        public static GenericContent CreateInstance(string deviceType, List<Notification> list)
        {
            GenericContent content;

            var type = Enum.Parse(typeof(DeviceType), deviceType);
            var notification = list.First();

            switch (type)
            {
                case DeviceType.N:
                    content = new CactiContent(notification);
                    break;

                case DeviceType.S:
                    content = new CameraContent(notification, list);
                    break;

                default:
                    throw new Exception($"尚未實作 { type } 通知內容");
            }

            content.CustomInitialize();

            return content;
        }
    }
}