using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 溫濕度計通知內容
    /// </summary>
    public class IfaceContent : GenericContent
    {
        private readonly List<Notification> _notifications;

        public IfaceContent(List<Notification> notifications) : base(notifications)
        {
            _notifications = notifications;
        }

        public override List<PushContent> Execute()
        {
            var contents = new List<PushContent>();

            foreach (var item in _notifications)
            {
                contents.Add(new PushContent
                {
                    DEVICE_SN = Notification.DEVICE_SN,
                    RECORD_SN = Notification.RECORD_SN,
                    LOG_TYPE = Notification.DEVICE.DEVICE_TYPE,
                    GROUP_LIST = GetGroups(),

                    TARGET_NAME = item.TARGET_NAME,
                    BUTTON_STATUS = GetButtonStatus(),
                    TITLE = GetTitle(),
                    COLOR = GetColor(),
                    FIELD_LIST = GetFields(item)
                });
            }

            return contents;
        }

        private List<Field> GetFields(Notification notification)
        {
            var value = Convert.ToDecimal(notification.MONITOR.TARGET_MESSAGE) / 100;
            var message = $"{ notification.TARGET_NAME }：{ value }";

            return new List<Field>
            {
                new Field("設備名稱", notification.DEVICE.DEVICE_NAME, true),
                new Field("設備位址", notification.DEVICE.DEVICE_ID, true),
                new Field("監控項目", notification.TARGET_NAME, true),
                new Field("發生時間", notification.MONITOR.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", message, true)
            };
        }
    }
}