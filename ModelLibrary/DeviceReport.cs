using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 設備即時資料報表
    /// </summary>
    public class DeviceReport
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
        /// 設備狀態 N - 正常, E - 異常, R-修復中
        /// </summary>
        public string DEVICE_STATUS { get; set; }

        /// <summary>
        /// 供應商編號
        /// </summary>
        public string SUPPLIER_SN { get; set; }

        /// <summary>
        /// 修復人員帳號
        /// </summary>
        public string REPAIRMAN_ID { get; set; }

        /// <summary>
        /// 設備狀態資訊
        /// </summary>
        public Config STATUS_INFO { get; set; }

        /// <summary>
        /// 供應商資訊
        /// </summary>
        public Supplier SUPPLIER_INFO { get; set; }

        /// <summary>
        /// 修復人員資訊
        /// </summary>
        public User REPAIRMAN_INFO { get; set; }

        /// <summary>
        /// 設備異常資訊
        /// </summary>
        public DeviceLog DEVICE_LOG { get; set; }

        /// <summary>
        /// 管理人清單
        /// </summary>
        public List<DeviceMaintainer> MAINTAINER_LIST { get; set; }

        /// <summary>
        /// 管理者清單顯示
        /// </summary>
        public string MAINTAINER_DISPLAY
        {
            get
            {
                string text = "";
                foreach (var maintainer in MAINTAINER_LIST)
                {
                    text += string.Format("{0}\n", maintainer.USER_NAME);
                }
                return text;
            }
        }

        /// <summary>
        /// 廠商資訊顯示
        /// </summary>
        public string SUPPLIER_DISPLAY
        {
            get
            {
                return string.Format("{0}\n{1}\n{2}", SUPPLIER_INFO.SUPPLIER_NAME, SUPPLIER_INFO.SUPPLIER_TEL, SUPPLIER_INFO.SUPPLIER_EMAIL);
            }
        }
    }
}