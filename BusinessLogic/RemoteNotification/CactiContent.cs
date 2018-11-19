using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;

namespace BusinessLogic.RemoteNotification
{
    /// <summary>
    /// Cacti 推送內容
    /// </summary>
    public class CactiContent : NotificationContent
    {
        private readonly Monitor _monitor;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        public CactiContent(Monitor monitor)
        {
            _monitor = monitor;

            Initialize(EventType.Error);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">事件類型</param>
        public override void Initialize(EventType type)
        {
            DEVICE_SN = _monitor.DEVICE_SN;

            LOG_SN = GetLogSn();

            BUTTON_STATUS = GetButtonStatus();

            TITLE = GetTitle();

            COLOR = GetColor();

            LOG_TYPE = GetLogType();

            GROUP_LIST = GetGroups();

            FIELD_LIST = GetFields();
        }

        private int? GetLogSn()
        {
            //TODO: 未來資料庫有存原始資料，就可撤掉此方法

            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            var recordSn = (bll as Monitor_BLL).GetRecordSn(_monitor);

            return Convert.ToInt32(recordSn);
        }

        private string GetButtonStatus()
        {
            return _monitor.IS_EXCEPTION == "Y" ? "E" : "N";
        }

        private string GetTitle()
        {
            return _monitor.IS_EXCEPTION == "Y" ? "設備異常資訊" : "異常設備恢復資訊";
        }

        private string GetColor()
        {
            return _monitor.IS_EXCEPTION == "Y" ? "danger" : "good";
        }

        private string GetLogType()
        {
            return _monitor.IS_EXCEPTION == "N" || _monitor.TARGET_NAME == "Ping" ? "N" : "S";
        }

        private List<DeviceGroup> GetGroups()
        {
            var bll = GenericBusinessFactory.CreateInstance<DeviceGroup>();
            return (bll as DeviceGroup_BLL).GetGroups(_monitor.DEVICE_SN);
        }

        private List<Field> GetFields()
        {
            return new List<Field>
            {
                new Field("設備名稱", _monitor.DeviceName, true),
                new Field("設備位址", _monitor.DEVICE_ID, true),
                new Field("監控項目", _monitor.TARGET_NAME, true),
                new Field("發生時間", _monitor.RECEIVE_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("監控資訊", _monitor.TARGET_MESSAGE, true)
            };
        }
    }
}