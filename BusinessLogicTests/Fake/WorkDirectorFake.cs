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
        private List<Target> _targets;
        private List<AlarmCondition> _alarmConditions;

        public WorkDirectorFake(string detector, string originRecord, DeviceType deviceType,
                                List<Device> devices,
                                List<Target> targets,
                                List<AlarmCondition> alarmConditions)
            : base(detector, originRecord, deviceType)
        {
            _devices = devices;
            _targets = targets;
            _alarmConditions = alarmConditions;
        }

        protected override Device GetDevice(string deviceId, string deviceType)
        {
            var device = _devices.Find(x => x.DEVICE_ID == deviceId && x.DEVICE_TYPE == deviceType);

            return device;
        }

        protected override Target GetTarget(string deviceSn, string targetName)
        {
            var target = _targets.Find(x => x.DEVICE_SN == deviceSn && x.TARGET_NAME == targetName);

            if (target == null)
            {
                var value = targetName == "Ping" ? "DOWN" : "ALERT";
                target = new Target
                {
                    DEVICE_SN = deviceSn,
                    TARGET_NAME = targetName,
                    TARGET_STATUS = "0",
                    OPERATOR_TYPE = "Equal",
                    IS_EXCEPTION = "Y",
                    ALARM_CONDITIONS = new List<AlarmCondition>
                    {
                        new AlarmCondition
                        {
                            DEVICE_SN = deviceSn,
                            TARGET_NAME = targetName,
                            TARGET_VALUE = value
                        }
                    }
                };
            }
            else
                target.ALARM_CONDITIONS = _alarmConditions.Where(x => x.DEVICE_SN == deviceSn && x.TARGET_NAME == targetName).ToList();

            return target;
        }

        protected override void SaveList()
        {
            //假資料不實作
        }
    }
}