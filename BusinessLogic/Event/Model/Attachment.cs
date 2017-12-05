using ModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 推送訊息卡片 (Slack)
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// 建構式
        /// </summary>
        public Attachment()
        {
        }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public Attachment(EventType type, LogDetail log)
        {
            //訊息辨識ID(紀錄編號)
            CALLBACK_ID = log.LOG_SN;
            //卡片區塊設置
            SetField(log);
            //動作類型資料設置
            SetContent(type, log);
        }

        /// <summary>
        /// 主要標題
        /// </summary>
        //[JsonProperty("pretext")]
        public string TEXT_CONTENT { get; set; }

        /// <summary>
        /// 訊息辨識ID
        /// </summary>
        [JsonProperty("callback_id")]
        public string CALLBACK_ID { get; set; }

        /// <summary>
        /// 狀態顏色
        /// </summary>
        [JsonProperty("color")]
        public string COLOR_TYPE { get; set; }

        /// <summary>
        /// 圖片路徑
        /// </summary>
        [JsonProperty("image_url")]
        public string IMAGE_URL { get; set; }

        /// <summary>
        /// 附加內容清單
        /// </summary>
        [JsonProperty("fields")]
        public List<Field> FIELD_LIST { get; set; }

        /// <summary>
        /// 動作按鈕清單
        /// </summary>
        [JsonProperty("actions")]
        public List<Action> BUTTON_LIST { get; set; }

        /// <summary>
        /// 卡片區塊設置
        /// </summary>
        /// <param name="log">設備紀錄詳細資訊</param>
        private void SetField(LogDetail log)
        {
            FIELD_LIST = new List<Field>
            {
                new Field("紀錄編號", log.LOG_SN, false),
                new Field("設備名稱", log.DEVICE_NAME, true),
                new Field("設備位址", log.DEVICE_ID, true)
            };
        }

        /// <summary>
        /// 動作類型資料設置
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        private void SetContent(EventType type, LogDetail log)
        {
            //管理人清單
            string maintainers = GetMaintainer(log.MAINTAINER_LIST);

            FIELD_LIST.Add(new Field("管理人員", maintainers, false));

            switch (type)
            {
                //恢復
                case EventType.Recover:
                    TEXT_CONTENT = "設備恢復資訊";
                    COLOR_TYPE = "good";
                    FIELD_LIST.Add(new Field("處理人員", log.USER_NAME, true));
                    FIELD_LIST.Add(new Field("恢復時間", log.UP_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    break;
                //異常
                case EventType.Error:
                    TEXT_CONTENT = "設備異常資訊";
                    COLOR_TYPE = "danger";
                    FIELD_LIST.Add(new Field("異常資訊", log.ERROR_INFO, true));
                    FIELD_LIST.Add(new Field("異常時間", log.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    break;
                //修復
                case EventType.Repair:
                    TEXT_CONTENT = "設備處理資訊";
                    COLOR_TYPE = "warning";
                    FIELD_LIST.Add(new Field("異常資訊", log.ERROR_INFO, true));
                    FIELD_LIST.Add(new Field("異常時間", log.ERROR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    FIELD_LIST.Add(new Field("處理人員", log.USER_NAME, true));
                    FIELD_LIST.Add(new Field("處理時間", log.REPAIR_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true));
                    break;
            }
        }

        /// <summary>
        /// 管理人清單取得
        /// </summary>
        /// <param name="list">管理人清單</param>
        /// <returns></returns>
        private string GetMaintainer(List<DeviceMaintainer> list)
        {
            return string.Join(" ", list.Select(s => s.USER_NAME));
        }
    }
}