using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;

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
        /// <param name="deviceType">設備類型</param>
        /// <param name="list">通知資訊清單</param>
        /// <returns></returns>
        public static IContent CreateInstance(string deviceType, List<Notification> list)
        {
            IContent content;

            var type = Enum.Parse(typeof(DeviceType), deviceType);

            switch (type)
            {
                case DeviceType.N:
                    content = new GenericContent(list);
                    break;

                case DeviceType.S:
                    content = new CameraContent(list);
                    break;

                case DeviceType.D:
                    content = new IfaceContent(list);
                    break;

                default:
                    throw new Exception($"尚未實作 { type } 通知內容");
            }

            return content;
        }
    }
}