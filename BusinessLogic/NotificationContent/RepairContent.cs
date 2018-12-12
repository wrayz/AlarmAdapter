using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 維修登記推送內容
    /// </summary>
    public class RepairContent : GenericContent
    {
        private readonly Repair _repair;
        private Device _device;

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
            _device = GetDevice();

            DEVICE_SN = _repair.DEVICE_SN;

            RECORD_SN = _repair.RECORD_SN;

            BUTTON_STATUS = "R";

            TITLE = "設備修復處理中";

            COLOR = "warning";

            LOG_TYPE = _device.DEVICE_TYPE;

            GROUP_LIST = _device.GROUPS;

            FIELD_LIST = GetFields();
        }

        private List<Field> GetFields()
        {
            return new List<Field>
            {
                new Field("設備名稱", _device.DEVICE_NAME, true),
                new Field("設備識別碼", _device.DEVICE_ID, true),
                new Field("登記時間", _repair.REGISTER_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("處理人員", _repair.USERID, true)
            };
        }

        private Device GetDevice()
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return (bll as Device_BLL).GetDevice(_repair.DEVICE_SN);
        }
    }
}