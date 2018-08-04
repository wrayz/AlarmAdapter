namespace BusinessLogic
{
    public interface IPayload
    {
        /// <summary>
        /// 設備編號
        /// </summary>
        string DEVICE_SN { get; set; }

        /// <summary>
        /// 資料設置
        /// </summary>
        /// <param name="type">動作類型</param>
        void SetData(EventType type);
    }
}
