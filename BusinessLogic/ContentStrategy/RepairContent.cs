using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.ContentStrategy
{
    /// <summary>
    /// 維修登記推送內容
    /// </summary>
    public class RepairContent : GenericContentStrategy
    {
        private Repair _repair;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="repair">維修資訊</param>
        public RepairContent(Repair repair)
        {
            _repair = repair;

            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
            DEVICE_SN = _repair.DEVICE_SN;

            RECORD_SN = _repair.RECORD_SN;

            BUTTON_STATUS = "R";

            TITLE = "設備修復處理中";

            COLOR = "warning";

            LOG_TYPE = "R";

            GROUP_LIST = GetGroups();

            FIELD_LIST = GetFields();
        }

        private List<DeviceGroup> GetGroups()
        {
            var bll = GenericBusinessFactory.CreateInstance<DeviceGroup>();
            return (bll as DeviceGroup_BLL).GetGroups(_repair.DEVICE_SN);
        }

        private List<Field> GetFields()
        {
            var device = GetDevice();

            return new List<Field>
            {
                new Field("設備名稱", device.DEVICE_NAME, true),
                new Field("設備識別碼", device.DEVICE_ID, true),
                new Field("登記時間", DateTime.Now.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("處理人員", _repair.USERID, true)
            };
        }

        private Device GetDevice()
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return (bll as Device_BLL).GetNameAndId(_repair.DEVICE_SN);
        }
    }
}