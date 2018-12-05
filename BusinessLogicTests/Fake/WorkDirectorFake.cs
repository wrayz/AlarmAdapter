using BusinessLogic.Director;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTests.Fake
{
    internal class WorkDirectorFake : WorkDirector
    {
        private List<Device> _devices;
        private List<AlarmCondition> _alarmConditions;
        private List<Monitor> _previousMonitors;
        private List<NotificationCondition> _notificationConditions;
        private List<Notification> _notificationRecords;

        public WorkDirectorFake(string detector, string originRecord, DeviceType deviceType,
                                List<Device> devices,
                                List<AlarmCondition> alarmConditions,
                                List<Monitor> previousMonitors,
                                List<NotificationCondition> notificationConditions,
                                List<Notification> notificationRecords)
            : base(detector, originRecord, deviceType)
        {
            _devices = devices;
            _alarmConditions = alarmConditions;
            _previousMonitors = previousMonitors;
            _notificationConditions = notificationConditions;
            _notificationRecords = notificationRecords;
        }

        protected override Device GetDevice(string deviceId, string deviceType)
        {
            var device = _devices.Find(x => x.DEVICE_ID == deviceId && x.DEVICE_TYPE == deviceType);

            return device;
        }

        protected Monitor GetPreviousMonitor(Monitor monitor)
        {
            return _previousMonitors.Where(x => x.DEVICE_SN == monitor.DEVICE_SN)
                                    .OrderByDescending(y => y.RECORD_SN)
                                    .First();
        }

        protected NotificationCondition GetNotificationCondition(string deviceSn)
        {
            return _notificationConditions.Find(x => x.DEVICE_SN == deviceSn);
        }

        protected Notification GetNotificationRecord(Monitor monitor, NotificationCondition condition)
        {
            IEnumerable<Notification> records;
            var level = (IntervalLevel)Enum.Parse(typeof(IntervalLevel), condition.INTERVAL_LEVEL);

            switch (level)
            {
                case IntervalLevel.Device:
                    records = _notificationRecords.Where(x => x.DEVICE_SN == monitor.DEVICE_SN)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                case IntervalLevel.MonitorTarget:
                    records = _notificationRecords.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                case IntervalLevel.TargetMessage:
                    records = _notificationRecords.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME && x.TARGET_MESSAGE == monitor.TARGET_MESSAGE)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                default:
                    throw new Exception($"無 { condition.INTERVAL_LEVEL } 層級定義");
            }

            return records.Count() > 0 ? records.First() : new Notification();
        }

        protected override void SaveList()
        {
            //假資料不實作
        }
    }
}