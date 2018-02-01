using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 推送訊息物件
    /// </summary>
    public class IMPayload
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="name">推送名稱</param>
        /// <param name="type">推送動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public IMPayload(string name, EventType type, LogDetail log)
        {
            //紀錄編號
            LOG_SN = log.LOG_SN;
            //紀錄類型
            LOG_TYPE = "N";
            //設備編號
            DEVICE_SN = log.DEVICE_SN;
            //設備狀態
            BUTTON_STATUS = log.DEVICE_STATUS;
            //通送名稱
            SYSTEM_NAME = name;
            //群組清單內容
            GROUP_LIST = log.GROUP_LIST;
            //卡片資訊設置
            SetField(log);
            //動作類型資料設置
            SetContent(type, log);
        }

        /// <summary>
        /// Log 編號
        /// </summary>
        public int? LOG_SN { get; set; }

        /// <summary>
        /// 紀錄類型
        /// </summary>
        public string LOG_TYPE { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 推送名稱
        /// </summary>
        public string SYSTEM_NAME { get; set; }

        /// <summary>
        /// 設備狀態 N - 正常, E - 異常, R-修復中
        /// </summary>
        public string BUTTON_STATUS { get; set; }

        /// <summary>
        /// 顏色
        /// </summary>
        public string COLOR { get; set; }

        /// <summary>
        /// 推送狀態資訊
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 群組清單
        /// </summary>
        public List<DeviceGroup> GROUP_LIST { get; set; }

        /// <summary>
        /// 附加欄位清單
        /// </summary>
        public List<Field> FIELD_LIST { get; set; }

        /// <summary>
        /// 卡片資訊設置
        /// </summary>
        /// <param name="log">設備紀錄詳細資料</param>
        private void SetField(LogDetail log)
        {
            FIELD_LIST = new List<Field>
            {
                new Field("主機名稱", log.DEVICE_NAME, true),
                new Field("設備位址", log.DEVICE_ID, true),
                new Field("異常資訊", log.ERROR_INFO, true)
            };
        }

        /// <summary>
        /// 動作類型資料設置
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        private void SetContent(EventType type, LogDetail log)
        {
            switch (type)
            {
                //恢復
                case EventType.Recover:
                    TITLE = "異常設備恢復資訊";
                    COLOR = "good";
                    FIELD_LIST.Add(new Field("恢復時間", log.UP_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", log.USER_NAME, true));
                    break;
                //異常
                case EventType.Error:
                    TITLE = "設備異常資訊";
                    COLOR = "danger";
                    FIELD_LIST.Add(new Field("異常時間", log.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    break;
                //修復
                case EventType.Repair:
                    TITLE = "異常設備處理資訊";
                    COLOR = "warning";
                    FIELD_LIST.Add(new Field("處理時間", log.REPAIR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", log.USER_NAME, true));
                    break;
            }
        }
    }
}