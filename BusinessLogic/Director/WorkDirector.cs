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
        private readonly string _deviceType;
        private readonly string _originRecord;

        private IParser _parser;
        private RecordAlarm.Alarm _alarmer;

        internal List<DeviceMonitor> Monitors { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="originRecord">原始訊息</param>
        /// <param name="deviceType">設備類型</param>
        public WorkDirector(string detector, string originRecord, string deviceType)
        {
            _deviceType = deviceType;
            _originRecord = originRecord;
            _detector = detector;
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            InitWorkStation();

            Monitors = _parser.ParseRecord(_originRecord);

            Monitors.ForEach(monitor =>
            {
                var device = GetDevice(monitor.DEVICE_ID, _deviceType);
                monitor.DEVICE_SN = device.DEVICE_SN;
                monitor.IS_EXCEPTION = _alarmer.IsException(monitor, device.ALARM_CONDITIONS);
            });
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
            throw new NotImplementedException();
        }
    }
}