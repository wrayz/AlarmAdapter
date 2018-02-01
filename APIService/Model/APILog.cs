using System;

namespace APIService.Model
{
    /// <summary>
    /// API Log 紀錄接口物件
    /// </summary>
    public class APILog
    {
        /// <summary>
        /// 紀錄編號
        /// </summary>
        public int? LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備對應ID
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 動作類型
        /// </summary>
        public string ACTION_TYPE { get; set; }

        /// <summary>
        /// 紀錄資訊
        /// </summary>
        public string LOG_INFO { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime? LOG_TIME { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string USERID { get; set; }
    }
}