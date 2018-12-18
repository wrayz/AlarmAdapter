﻿using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// Cacti 推送內容
    /// </summary>
    public class CactiContent : GenericContent
    {
        protected Notification Notification { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="notification">通知資訊</param>
        public CactiContent(Notification notification)
        {
            Notification = notification;

            ExecuteInitialize();
        }

        /// <summary>
        /// 初始化執行
        /// </summary>
        private void ExecuteInitialize()
        {
            DEVICE_SN = Notification.DEVICE_SN;

            TARGET_NAME = Notification.TARGET_NAME;

            RECORD_SN = Notification.RECORD_SN;

            LOG_TYPE = Notification.DEVICE.DEVICE_TYPE;

            GROUP_LIST = GetGroups();

            FIELD_LIST = GetFields();
        }

        /// <summary>
        /// 客製初始化
        /// </summary>
        internal override void CustomInitialize()
        {
            BUTTON_STATUS = GetButtonStatus();

            TITLE = GetTitle();

            COLOR = GetColor();
        }

        private string GetButtonStatus()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "E" : "N";
        }

        private string GetTitle()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "設備異常資訊" : "異常設備恢復資訊";
        }

        private string GetColor()
        {
            return Notification.MONITOR.IS_EXCEPTION == "Y" ? "danger" : "good";
        }

        private List<GroupDevice> GetGroups()
        {
            var bll = GenericBusinessFactory.CreateInstance<GroupDevice>();
            return (bll as GroupDevice_BLL).GetGroups(Notification.DEVICE_SN);
        }

        private List<Field> GetFields()
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
    }
}