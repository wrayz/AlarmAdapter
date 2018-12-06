using Newtonsoft.Json;
using System.Collections.Generic;

namespace ModelLibrary
{
    public class GroupService
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        public string GROUP_SN { get; set; }

        /// <summary>
        /// 紀錄編號
        /// </summary>
        [JsonProperty("LOG_SN")]
        public string RECORD_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備類型
        /// </summary>
        public string DEVICE_TYPE { get; set; }

        /// <summary>
        /// 設備對應ID
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        public string DEVICE_NAME { get; set; }

        /// <summary>
        /// 服務狀態 N - 正常, E - 異常, R - 修復中
        /// </summary>
        public string SERVICE_STATUS { get; set; }

        /// <summary>
        /// 維修人員帳號
        /// </summary>
        public string USERID { get; set; }

        /// <summary>
        /// 維修人員資訊
        /// </summary>
        public User REPAIRMAN_INFO { get; set; }

        /// <summary>
        /// 管理人清單
        /// </summary>
        public List<DeviceMaintainer> MAINTAINER_LIST { get; set; }

        /// <summary>
        /// 類型描述
        /// </summary>
        public string TYPE_DESC
        {
            get
            {
                return DEVICE_TYPE == "N" ? "設備網路狀態" : "設備數據狀態";
            }
        }
    }
}