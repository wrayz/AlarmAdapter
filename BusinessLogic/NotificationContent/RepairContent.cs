using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 維修登記推送內容
    /// </summary>
    public class RepairContent : IContent
    {
        private readonly Repair _repair;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="repair">維修資訊</param>
        public RepairContent(Repair repair)
        {
            _repair = repair;
        }

        /// <summary>
        /// 執行
        /// </summary>
        /// <returns></returns>
        public List<PushContent> Execute()
        {
            var device = GetDevice();

            return new List<PushContent>
            {
                new PushContent
                {
                    DEVICE_SN = _repair.DEVICE_SN,
                    RECORD_SN = _repair.RECORD_SN,
                    BUTTON_STATUS = "R",
                    TITLE = "設備修復處理中",
                    COLOR = "warning",
                    LOG_TYPE = device.DEVICE_TYPE,
                    GROUP_LIST = device.GROUPS,
                    FIELD_LIST = GetFields(device)
                }
            };
        }

        private List<Field> GetFields(Device device)
        {
            return new List<Field>
            {
                new Field("設備名稱", device.DEVICE_NAME, true),
                new Field("設備識別碼", device.DEVICE_ID, true),
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