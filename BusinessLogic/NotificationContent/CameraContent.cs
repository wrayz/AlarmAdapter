using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Configuration;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 攝像機通知內容
    /// </summary>
    public class CameraContent : GenericContent
    {
        public CameraContent(List<Notification> notifications) : base(notifications)
        {
        }

        protected override string GetButtonStatus()
        {
            return "N";
        }

        protected override string GetTitle()
        {
            return "設備異常資訊";
        }

        protected override string GetColor()
        {
            return "danger";
        }

        protected override List<Field> GetFields()
        {
            var fields = new List<Field>
            {
                new Field("設備名稱", Notification.DEVICE.DEVICE_NAME, true),
                new Field("設備位址", Notification.DEVICE.DEVICE_ID, true),
                new Field("監控項目", Notification.TARGET_NAME, true),
                new Field("發生時間", Notification.MONITOR.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", Notification.TARGET_MESSAGE, true)
            };

            //攝像機影片
            if (Notifications.Count > 1)
            {
                var camera = Notifications[1];
                var setting = GetCameraSetting();
                var fileUrl = $"{ setting.HOST_URL }/{ setting.FILE_DIR }/{camera.MONITOR.TARGET_VALUE}.{ setting.FILE_TYPE }";

                fields.Add(new Field(camera.TARGET_NAME, fileUrl, true));
            }

            return fields;
        }

        private CameraSetting GetCameraSetting()
        {
            var bll = GenericBusinessFactory.CreateInstance<CameraSetting>();
            return bll.Get(new QueryOption(), new UserLogin());
        }
    }
}