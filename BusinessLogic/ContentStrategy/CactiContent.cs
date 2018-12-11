using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.ContentStrategy
{
    /// <summary>
    /// Cacti 推送內容
    /// </summary>
    public class CactiContent : GenericContentStrategy
    {
        private readonly Notification _notification;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="notification">通知資訊</param>
        public CactiContent(Notification notification)
        {
            _notification = notification;

            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
            DEVICE_SN = _notification.DEVICE_SN;

            TARGET_NAME = _notification.TARGET_NAME;

            RECORD_SN = _notification.RECORD_SN;

            BUTTON_STATUS = GetButtonStatus();

            TITLE = GetTitle();

            COLOR = GetColor();

            LOG_TYPE = _notification.DEVICE.DEVICE_TYPE;

            GROUP_LIST = GetGroups();

            FIELD_LIST = GetFields();
        }

        private string GetButtonStatus()
        {
            return _notification.MONITOR.IS_EXCEPTION == "Y" ? "E" : "N";
        }

        private string GetTitle()
        {
            return _notification.MONITOR.IS_EXCEPTION == "Y" ? "設備異常資訊" : "異常設備恢復資訊";
        }

        private string GetColor()
        {
            return _notification.MONITOR.IS_EXCEPTION == "Y" ? "danger" : "good";
        }

        private List<DeviceGroup> GetGroups()
        {
            var bll = GenericBusinessFactory.CreateInstance<DeviceGroup>();
            return (bll as DeviceGroup_BLL).GetGroups(_notification.DEVICE_SN);
        }

        private List<Field> GetFields()
        {
            return new List<Field>
            {
                new Field("設備名稱", _notification.DEVICE.DEVICE_NAME, true),
                new Field("設備位址", _notification.DEVICE.DEVICE_ID, true),
                new Field("監控項目", _notification.TARGET_NAME, true),
                new Field("發生時間", _notification.MONITOR.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", _notification.TARGET_MESSAGE, true)
            };
        }
    }
}