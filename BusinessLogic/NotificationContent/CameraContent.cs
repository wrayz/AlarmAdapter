using ModelLibrary;
using System.Collections.Generic;
using System.Configuration;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 攝像機通知內容
    /// </summary>
    public class CameraContent : CactiContent
    {
        private List<Notification> _notifications;

        public CameraContent(Notification notification, List<Notification> notifications) : base(notification)
        {
            _notifications = notifications;
        }

        internal override void CustomInitialize()
        {
            BUTTON_STATUS = "N";

            TITLE = "設備異常資訊";

            COLOR = "danger";

            //攝像機影片
            if (_notifications.Count > 1)
            {
                var camera = _notifications[1];
                var host = ConfigurationManager.AppSettings["host"];
                var fileUrl = $"{ host }{ camera.TARGET.FILE_DIR }/{camera.MONITOR.TARGET_VALUE}.{ camera.TARGET.FILE_TYPE }";

                FIELD_LIST.Add(new Field(camera.TARGET_NAME, fileUrl, true));
            }
        }
    }
}