using System;

namespace BusinessLogic
{
    public interface IPayload
    {
        /// <summary>
        /// 記錄編號
        /// </summary>
        int? LOG_SN { get; set; }

        /// <summary>
        /// 設備編號
        /// </summary>
        string DEVICE_SN { get; set; }

        /// <summary>
        /// 記錄時間
        /// </summary>
        DateTime? RECORD_TIME { get; set; }

        /// <summary>
        /// 資料設置
        /// </summary>
        /// <param name="type">動作類型</param>
        void SetData(EventType type);
    }
}
