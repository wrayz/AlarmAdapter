using System.Collections.Generic;

namespace ModelLibrary
{
    public class Target
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        public string DEVICE_SN { get; set; }

        /// <summary>
        /// 監控項目名稱
        /// </summary>
        public string TARGET_NAME { get; set; }

        /// <summary>
        /// 監控項目狀態
        /// </summary>
        public string TARGET_STATUS { get; set; }

        /// <summary>
        /// 告警運算子
        /// </summary>
        public string OPERATOR_TYPE { get; set; }

        /// <summary>
        /// 是否異常（Y - 是，N - 否）
        /// </summary>
        public string IS_EXCEPTION { get; set; }

        public List<AlarmCondition> ALARM_CONDITIONS { get; set; }
    }
}