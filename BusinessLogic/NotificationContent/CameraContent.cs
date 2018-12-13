using ModelLibrary;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 攝像機通知內容
    /// </summary>
    public class CameraContent : CactiContent
    {
        public CameraContent(Notification notification) : base(notification)
        {
        }

        protected override void Initialize()
        {
            BUTTON_STATUS = "N";

            TITLE = "設備異常資訊";

            COLOR = "danger";

            FIELD_LIST.Add(new Field (Notification.TARGET_NAME, Notification.TARGET_MESSAGE, true));
        }
    }
}