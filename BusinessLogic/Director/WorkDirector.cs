using BusinessLogic.RecordAlarm;
using BusinessLogic.RecordNotifier;
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
        private INotifier _notifier;

        /// <summary>
        /// 設備監控資訊清單
        /// </summary>
        public List<Monitor> Monitors { get; private set; }

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
                monitor.DeviceName = device.DEVICE_NAME;
                monitor.DEVICE_SN = device.DEVICE_SN;

                monitor.IS_EXCEPTION = _alarmer.IsException(monitor, device.ALARM_CONDITIONS);

                var condition = GetNotificationCondition(device.DEVICE_SN);

                //TODO: 使用靜態 Dictionary 將前次監控資訊存在 Memory（重開機要在初始化進資料庫）
                var previousMonitor = GetPreviousMonitor(monitor);
                var record = GetNotificationRecord(monitor, condition);

                //TODO: 之後通知層獨立於 WorkDirector
                monitor.IS_NOTIFICATION = _notifier.IsNotification(condition, monitor, previousMonitor, record);
            });

            Save();
        }

        /// <summary>
        /// 初始工作站
        /// </summary>
        private void InitWorkStation()
        {
            _parser = ParserFactory.CreateInstance(_detector);
            _alarmer = AlarmFactory.CreateInstance(_detector);
            _notifier = new Notifier();
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
        /// 前次監控訊息取得
        /// </summary>
        /// <param name="monitor">當前監控訊息</param>
        /// <returns></returns>
        protected virtual Monitor GetPreviousMonitor(Monitor monitor)
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            return (bll as Monitor_BLL).GetPreviousMonitor(monitor);
        }

        /// <summary>
        /// 通知條件取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        protected virtual NotificationCondition GetNotificationCondition(string deviceSn)
        {
            var bll = GenericBusinessFactory.CreateInstance<NotificationCondition>();
            return (bll as NotificationCondition_BLL).GetNotificationCondition(deviceSn);
        }

        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <param name="condition">通知條件</param>
        /// <returns></returns>
        protected virtual RecordNotification GetNotificationRecord(Monitor monitor, NotificationCondition condition)
        {
            var bll = GenericBusinessFactory.CreateInstance<RecordNotification>();
            return (bll as RecordNotification_BLL).GetRecord(monitor, condition);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected virtual void Save()
        {
            throw new NotImplementedException();
        }
    }
}