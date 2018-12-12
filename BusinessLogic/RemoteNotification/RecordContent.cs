using ModelLibrary;
using ModelLibrary.Enumerate;
using System.Collections.Generic;

namespace BusinessLogic.RemoteNotification
{
    /// <summary>
    /// 數據類型通知資料
    /// </summary>
    public class RecordContent : GenericRemoteContent
    {
        private RecordLog _recordLog;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="system">系統名稱</param>
        /// <param name="type">動作類型</param>
        /// <param name="recordLog">數據記錄</param>
        public RecordContent(EventType type, RecordLog recordLog)
        {
            //紀錄編號
            LOG_SN = recordLog.LOG_SN;
            //紀錄類型
            LOG_TYPE = "D";
            //設備編號
            DEVICE_SN = recordLog.DEVICE_SN;
            //群組清單內容
            GROUP_LIST = recordLog.GROUP_LIST;

            _recordLog = recordLog;
            //資料設置
            Initialize(type);
        }

        /// <summary>
        /// 資料設置
        /// </summary>
        /// <param name="type">資料動作</param>
        public override void Initialize(EventType type)
        {
            FIELD_LIST = new List<Field>
            {
                new Field("設備名稱", _recordLog.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", _recordLog.DEVICE_INFO.DEVICE_ID, true)
            };

            switch (type)
            {
                case EventType.Error:
                    TITLE = "設備異常資訊";
                    BUTTON_STATUS = "E";
                    COLOR = "danger";

                    FIELD_LIST.Add(new Field("異常數據", string.Format("溫度：{0} 度\n濕度：{1} %", _recordLog.RECORD_TEMPERATURE, _recordLog.RECORD_HUMIDITY), false));
                    FIELD_LIST.Add(new Field("異常時間", _recordLog.RECORD_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    break;

                case EventType.Repair:
                    TITLE = "異常設備處理資訊";
                    BUTTON_STATUS = "R";
                    COLOR = "warning";

                    FIELD_LIST.Add(new Field("處理時間", _recordLog.REPAIR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", _recordLog.USER_INFO.USER_NAME, true));
                    break;

                case EventType.Recover:
                    TITLE = "異常設備恢復資訊";
                    BUTTON_STATUS = "N";
                    COLOR = "good";

                    FIELD_LIST.Add(new Field("現在數據", string.Format("溫度：{0} 度\n濕度：{1} %", _recordLog.RECORD_TEMPERATURE, _recordLog.RECORD_HUMIDITY), false));
                    FIELD_LIST.Add(new Field("恢復時間", _recordLog.RECOVER_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", _recordLog.USER_INFO.USER_NAME, true));
                    break;

                default:
                    break;
            }
        }
    }
}