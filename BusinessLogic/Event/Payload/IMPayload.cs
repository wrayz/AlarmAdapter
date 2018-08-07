using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 推送訊息物件
    /// </summary>
    public class IMPayload : Payload
    {
        private LogDetail _logDetail;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="name">系統名稱</param>
        /// <param name="type">推送動作類型</param>
        /// <param name="logDetail">設備紀錄詳細資料</param>
        public IMPayload(EventType type, LogDetail logDetail)
        {
            //紀錄編號
            LOG_SN = logDetail.LOG_SN;
            //紀錄類型
            LOG_TYPE = "N";
            //設備編號
            DEVICE_SN = logDetail.DEVICE_SN;
            //設備狀態
            BUTTON_STATUS = logDetail.DEVICE_STATUS;
            //群組清單內容
            GROUP_LIST = logDetail.GROUP_LIST;

            //動作類型資料設置
            _logDetail = logDetail;
            SetData(type);
        }

        /// <summary>
        /// 資料設置
        /// </summary>
        /// <param name="type">動作類型</param>
        public override void SetData(EventType type)
        {
            FIELD_LIST = new List<Field>
            {
                new Field("設備名稱", _logDetail.DEVICE_NAME, true),
                new Field("設備位址", _logDetail.DEVICE_ID, true)
            };

            switch (type)
            {
                //恢復
                case EventType.Recover:
                    TITLE = "異常設備恢復資訊";
                    COLOR = "good";
                    FIELD_LIST.Add(new Field("恢復時間", _logDetail.UP_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", _logDetail.USER_NAME, true));
                    break;
                //異常
                case EventType.Error:
                    TITLE = "設備異常資訊";
                    COLOR = "danger";
                    FIELD_LIST.Add(new Field("異常時間", _logDetail.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("異常資訊", _logDetail.ERROR_INFO, true));
                    break;
                //修復
                case EventType.Repair:
                    TITLE = "異常設備處理資訊";
                    COLOR = "warning";
                    FIELD_LIST.Add(new Field("處理時間", _logDetail.REPAIR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", _logDetail.USER_NAME, true));
                    break;
            }
        }
    }
}