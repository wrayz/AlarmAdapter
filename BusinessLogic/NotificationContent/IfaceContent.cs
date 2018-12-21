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

        public IfaceContent(List<Notification> notifications) : base(notifications)
        {
        }

        public override List<PushContent> Execute()
        {
            var contents = new List<PushContent>();

            foreach (var item in Notifications)
            {
                Notification = item;

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
                    FIELD_LIST = GetFields()
                });
            }

            return contents;
        }

        protected override List<Field> GetFields()
        {
            var value = Convert.ToDecimal(Notification.MONITOR.TARGET_MESSAGE) / 100;
            var message = $"{ Notification.TARGET_NAME }：{ value }";

            return new List<Field>
            {
                new Field("設備名稱", Notification.DEVICE.DEVICE_NAME, true),
                new Field("設備位址", Notification.DEVICE.DEVICE_ID, true),
                new Field("監控項目", Notification.TARGET_NAME, true),
                new Field("發生時間", Notification.MONITOR.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", message, true)
            };
        }
    }
}