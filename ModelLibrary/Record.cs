using System;

namespace ModelLibrary
{
    /// <summary>
    /// 數據紀錄資料
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 數據紀錄編號
        /// </summary>
        public int? RECORD_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 設備ID
        /// </summary>
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime? RECORD_TIME { get; set; }

        /// <summary>
        /// 記錄溫度
        /// </summary>
        public decimal? RECORD_TEMPERATURE { get; set; }

        /// <summary>
        /// 紀錄濕度
        /// </summary>
        public decimal? RECORD_HUMIDITY { get; set; }
    }
}