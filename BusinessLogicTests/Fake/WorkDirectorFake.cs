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

        public WorkDirectorFake(string detector, string originRecord, string deviceType,
                                List<Device> devices, List<AlarmCondition> alarmConditions)
            : base(detector, originRecord, deviceType)
        {
            _devices = devices;
            _alarmConditions = alarmConditions;
        }

        protected override Device GetDevice(string deviceId, string deviceType)
        {
            var device = _devices.Find(x => x.DEVICE_ID == deviceId && x.DEVICE_TYPE == deviceType);

            device.ALARM_CONDITIONS = _alarmConditions.Where(x => x.DEVICE_SN == device.DEVICE_SN)
                                                      .ToList();

            return device;
        }
    }
}