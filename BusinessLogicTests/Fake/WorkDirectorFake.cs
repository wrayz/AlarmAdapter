using BusinessLogic.Director;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTests.Fake
{
    internal class WorkDirectorFake : WorkDirector
    {
        private List<Device> _devices;
        private List<AlarmCondition> _alarmConditions;
        private List<DeviceMonitor> _previousMonitors;
        private List<NotificationSetting> _notificationConditions;

        public WorkDirectorFake(string detector, string originRecord, DeviceType deviceType, 
                                List<Device> devices,
                                List<AlarmCondition> alarmConditions, 
                                List<DeviceMonitor> previousMonitors,
                                List<NotificationSetting> notificationConditions)
            : base(detector, originRecord, deviceType)
        {
            _devices = devices;
            _alarmConditions = alarmConditions;
            _previousMonitors = previousMonitors;
            _notificationConditions = notificationConditions;
        }

        protected override Device GetDevice(string deviceId, string deviceType)
        {
            var device = _devices.Find(x => x.DEVICE_ID == deviceId && x.DEVICE_TYPE == deviceType);

            device.ALARM_CONDITIONS = _alarmConditions.Where(x => x.DEVICE_SN == device.DEVICE_SN)
                                                      .ToList();

            return device;
        }

        protected override DeviceMonitor GetPreviousMonitor(DeviceMonitor monitor)
        {
            return _previousMonitors.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME)
                                    .OrderByDescending(y => y.RECORD_SN)
                                    .First();
        }

        protected override NotificationSetting GetNotificationCondition(string deviceSn)
        {
            return _notificationConditions.Find(x => x.DEVICE_SN == deviceSn);
        }
    }
}