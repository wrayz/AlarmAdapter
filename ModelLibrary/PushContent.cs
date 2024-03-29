﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 推播內容
    /// </summary>
    public class PushContent
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        [JsonProperty("LOG_SN")]
        public string RECORD_SN { get; set; }

        /// <summary>
        /// 紀錄類型
        /// </summary>
        public string LOG_TYPE { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目
        /// </summary>
        public string TARGET_NAME { get; set; }

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
        public string BUTTON_STATUS { get; set; }

        /// <summary>
        /// 顏色
        /// </summary>
        public string COLOR { get; set; }

        /// <summary>
        /// 訊息標題
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 設備群組清單
        /// </summary>
        public List<GroupDevice> GROUP_LIST { get; set; }

        /// <summary>
        /// 附加欄位清單
        /// </summary>
        public List<Field> FIELD_LIST { get; set; }
    }
}