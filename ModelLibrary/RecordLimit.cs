using System;

namespace ModelLibrary
{
    /// <summary>
    /// 設備溫溼度監控參數資料
    /// </summary>
    public class RecordLimit
    {
        /// <summary>
        /// 監控溫度
        /// </summary>
        public string LIMIT_TEMPERATURE { get; set; }

        /// <summary>
        /// 監控濕度
        /// </summary>
        public string LIMIT_HUMIDITY { get; set; }

        /// <summary>
        /// 溫度
        /// </summary>
        public decimal? TEMPERATURE {
            get
            {
                return Convert.ToDecimal(LIMIT_TEMPERATURE);
            }
        }

        /// <summary>
        /// 濕度
        /// </summary>
        public decimal? HUMIDITY {
            get
            {
                return Convert.ToDecimal(LIMIT_HUMIDITY);
            }
        }
    }
}