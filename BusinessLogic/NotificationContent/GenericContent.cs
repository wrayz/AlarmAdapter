using ModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessLogic.NotificationContent
{
    /// <summary>
    /// 通用通知內容
    /// </summary>
    public abstract class GenericContent
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// 紀錄編號
        /// </summary>
        [JsonProperty("LOG_SN")]
        public string RECORD_SN { get; protected set; }

        /// <summary>
        /// 紀錄類型
        /// </summary>
        public string LOG_TYPE { get; protected set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; protected set; }

        /// <summary>
        /// 監控項目
        /// </summary>
        public string TARGET_NAME { get; protected set; }

        /// <summary>
        /// 推送名稱
        /// </summary>
        public string SYSTEM_NAME
        {
            get
            {
                return "EyesFree";
            }
        }

        /// <summary>
        /// 按鈕狀態 N - 正常, E - 異常, R-修復中
        /// </summary>
        public string BUTTON_STATUS { get; protected set; }

        /// <summary>
        /// 顏色
        /// </summary>
        public string COLOR { get; protected set; }

        /// <summary>
        /// 訊息標題
        /// </summary>
        public string TITLE { get; protected set; }

        /// <summary>
        /// 設備群組清單
        /// </summary>
        public List<DeviceGroup> GROUP_LIST { get; protected set; }

        /// <summary>
        /// 附加欄位清單
        /// </summary>
        public List<Field> FIELD_LIST { get; protected set; }
    }
}