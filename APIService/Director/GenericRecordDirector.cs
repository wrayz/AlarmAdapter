using APIService.PushStrategy;
using BusinessLogic.Director;
using BusinessLogic.License;
using BusinessLogic.NotificationStrategy;
using ModelLibrary.Enumerate;
using System;

namespace APIService.Director
{
    /// <summary>
    /// 一般記錄管理站
    /// </summary>
    internal class GenericRecordDirector
    {
        private readonly LicenseBusinessLogic _license;
        private readonly WorkDirector _workDirector;
        private readonly NotificationDirector _notificationDirector;
        private readonly MonitorPushStrategy _pusher;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="detector">偵測器</param>
        /// <param name="record">接收記錄訊息</param>
        /// <param name="deviceType">設備類型</param>
        /// <param name="sourceIp">來源 IP</param>
        public GenericRecordDirector(Detector detector, string record, DeviceType deviceType, string sourceIp = null)
        {
            _license = new LicenseBusinessLogic();
            _workDirector = new WorkDirector(detector, record, deviceType, sourceIp);

            var strategy = new GenericNotifier();
            _notificationDirector = new NotificationDirector(strategy);

            _pusher = new MonitorPushStrategy();
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            _license.Verify(DateTime.Now);

            _workDirector.Execute();

            _notificationDirector.Execute();

            _pusher.Execute();
        }
    }
}