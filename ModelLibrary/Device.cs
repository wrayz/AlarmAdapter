using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 設備資料
    /// </summary>
    public class Device
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備對應ID
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DEVICE_NAME { get; set; }

        /// <summary>
        /// 設備類型
        /// </summary>
        public string DEVICE_TYPE { get; set; }

        /// <summary>
        /// 是否監控
        /// </summary>
        public string IS_MONITOR { get; set; }

        /// <summary>
        /// 設備狀態 N - 正常, E - 異常, R - 修復中
        /// </summary>
        public string DEVICE_STATUS { get; set; }

        /// <summary>
        /// 數值記錄狀態 N - 正常, E - 異常, R - 修復中
        /// </summary>
        public string RECORD_STATUS { get; set; }

        /// <summary>
        /// 通知設定
        /// </summary>
        public NotificationCondition NOTIFICATION_CONDITION { get; set; }

        /// <summary>
        /// 設備群組清單
        /// </summary>
        public List<GroupDevice> GROUPS { get; set; }
    }
}