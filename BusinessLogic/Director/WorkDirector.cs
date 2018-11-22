using BusinessLogic.RecordAlarm;
using BusinessLogic.RecordParser;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Director
{
    /// <summary>
    /// 工作管理站
    /// </summary>
    public class WorkDirector
    {
        private readonly string _detector;
        private readonly string _originRecord;
        private readonly string _deviceType;

        private IParser _parser;
        private RecordAlarm.Alarm _alarmer;

        /// <summary>
        /// 設備監控資訊清單
        /// </summary>
        internal List<Monitor> Monitors { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="originRecord">原始訊息</param>
        /// <param name="deviceType">設備類型</param>
        public WorkDirector(string detector, string originRecord, DeviceType deviceType)
        {
            _detector = detector;
            _originRecord = originRecord;
            _deviceType = Enum.GetName(typeof(DeviceType), deviceType);
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            InitWorkStation();

            Monitors = _parser.ParseRecord(_originRecord);

            //TODO: ForEach 想辦法調掉
            Monitors.ForEach(monitor =>
            {
                var device = GetDevice(monitor.DEVICE_ID, _deviceType);
                monitor.DEVICE_SN = device.DEVICE_SN;

                monitor.IS_EXCEPTION = _alarmer.IsException(monitor, device.ALARM_CONDITIONS);
            });

            SaveList();
        }

        /// <summary>
        /// 初始工作站
        /// </summary>
        private void InitWorkStation()
        {
            _parser = ParserFactory.CreateInstance(_detector);
            _alarmer = AlarmFactory.CreateInstance(_detector);
        }

        /// <summary>
        /// 設備取得
        /// </summary>
        /// <param name="deviceId">設備識別碼</param>
        /// <param name="deviceType">設備類型</param>
        /// <returns></returns>
        protected virtual Device GetDevice(string deviceId, string deviceType)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            return (bll as Device_BLL).GetDevice(deviceId, deviceType);
        }

        /// <summary>
        /// 多筆儲存
        /// </summary>
        protected virtual void SaveList()
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            (bll as Monitor_BLL).SaveList(Monitors);
        }
    }
}