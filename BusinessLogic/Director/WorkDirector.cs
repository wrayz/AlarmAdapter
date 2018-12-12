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
        private readonly Detector _detector;
        private readonly string _originRecord;
        private readonly string _deviceType;
        private readonly string _sourceIp;

        private IParser _parser;
        private Alarmer _alarmer;

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
        public WorkDirector(Detector detector, string originRecord, DeviceType deviceType, string sourceIp)
        {
            _detector = detector;
            _originRecord = originRecord;
            _deviceType = Enum.GetName(typeof(DeviceType), deviceType);
            _sourceIp = sourceIp;
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            InitWorkStation();

            Monitors = _parser.ParseRecord(_originRecord, _sourceIp);

            //TODO: ForEach 想辦法調掉
            Monitors.ForEach(monitor =>
            {
                var device = GetDevice(monitor.DEVICE_ID, _deviceType);
                monitor.DEVICE_SN = device.DEVICE_SN;

                Target target = GetTarget(device.DEVICE_SN, monitor.TARGET_NAME);
                monitor.IS_EXCEPTION = _alarmer.IsException(monitor, target);
            });

            SaveList();
        }

        /// <summary>
        /// 初始工作站
        /// </summary>
        private void InitWorkStation()
        {
            _parser = ParserFactory.CreateInstance(_detector);
            _alarmer = new Alarmer();
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
        /// 監控項目資訊取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="targetName">監控項目名稱</param>
        /// <returns></returns>
        protected virtual Target GetTarget(string deviceSn, string targetName)
        {
            var bll = GenericBusinessFactory.CreateInstance<Target>();
            return (bll as Target_BLL).GetTarget(_detector, deviceSn, targetName);
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