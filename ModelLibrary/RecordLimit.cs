using System;

namespace ModelLibrary
{
    /// <summary>
    /// 設備溫溼度監控參數資料
    /// </summary>
    public class RecordLimit
    {
        /// <summary>
        /// 最高限制溫度
        /// </summary>
        public string MAX_TEMPERATURE { get; set; }

        /// <summary>
        /// 最高限制濕度
        /// </summary>
        public string MAX_HUMIDITY { get; set; }

        /// <summary>
        /// 最低限制溫度
        /// </summary>
        public string MIN_TEMPERATURE { get; set; }

        /// <summary>
        /// 最低限制濕度
        /// </summary>
        public string MIN_HUMIDITY { get; set; }

        /// <summary>
        /// 最高溫度
        /// </summary>
        public decimal? MAX_TEMPERATURE_VAL {
            get
            {
                return Convert.ToDecimal(MAX_TEMPERATURE);
            }
        }

        /// <summary>
        /// 最高濕度
        /// </summary>
        public decimal? MAX_HUMIDITY_VAL {
            get
            {
                return Convert.ToDecimal(MAX_HUMIDITY);
            }
        }

        /// <summary>
        /// 最低溫度
        /// </summary>
        public decimal? MIN_TEMPERATURE_VAL
        {
            get
            {
                return Convert.ToDecimal(MIN_TEMPERATURE);
            }
        }

        /// <summary>
        /// 最低濕度
        /// </summary>
        public decimal? MIN_HUMIDITY_VAL
        {
            get
            {
                return Convert.ToDecimal(MIN_HUMIDITY);
            }
        }
    }
}