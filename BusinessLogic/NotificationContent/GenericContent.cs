using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// Cacti 推送內容
    /// </summary>
    public class GenericContent : IContent
    {
        protected Notification Notification { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="notifications">通知資訊</param>
        public GenericContent(List<Notification> notifications)
        {
            Notification = notifications.First();
        }

        /// <summary>
        /// 執行
        /// </summary>
        public virtual List<PushContent> Execute()
        {
            return new List<PushContent>
            {
                new PushContent
                {
                    DEVICE_SN = Notification.DEVICE_SN,
                    RECORD_SN = Notification.RECORD_SN,
                    LOG_TYPE = Notification.DEVICE.DEVICE_TYPE,
                    GROUP_LIST = GetGroups(),

                    TARGET_NAME = GetTargetName(),
                    BUTTON_STATUS = GetButtonStatus(),
                    TITLE = GetTitle(),
                    COLOR = GetColor(),
                    FIELD_LIST = GetFields()
                }
            };
        }

        protected virtual string GetTargetName()
        {
            return Notification.TARGET_NAME;
        }

        protected virtual string GetButtonStatus()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "E" : "N";
        }

        protected virtual string GetTitle()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "設備異常資訊" : "異常設備恢復資訊";
        }

        protected virtual string GetColor()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "danger" : "good";
        }

        protected virtual List<Field> GetFields()
        {
            return new List<Field>
            {
                new Field("設備名稱", Notification.DEVICE.DEVICE_NAME, true),
                new Field("設備位址", Notification.DEVICE.DEVICE_ID, true),
                new Field("監控項目", Notification.TARGET_NAME, true),
                new Field("發生時間", Notification.MONITOR.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", Notification.TARGET_MESSAGE, true)
            };
        }

        protected List<GroupDevice> GetGroups()
        {
            var bll = GenericBusinessFactory.CreateInstance<GroupDevice>();
            return (bll as GroupDevice_BLL).GetGroups(Notification.DEVICE_SN);
        }
    }
}