using BusinessLogic.Director;
using BusinessLogic.NotificationStrategy;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTests.Fake
{
    internal class NotificationDirectorFake : NotificationDirector
    {
        private List<Monitor> _monitors;
        private readonly List<Monitor> _previousMonitors;
        private readonly List<NotificationCondition> _notificationConditions;
        private readonly List<Notification> _notifications;

        public NotificationDirectorFake(
            NotifierStrategy strategy,
            List<Monitor> monitors,
            List<Monitor> previousMonitors,
            List<NotificationCondition> notificationConditions,
            List<Notification> notifications
            ) : base(strategy)
        {
            _monitors = monitors;
            _previousMonitors = previousMonitors;
            _notificationConditions = notificationConditions;
            _notifications = notifications;
        }

        protected override List<Monitor> GetMonitors()
        {
            return _monitors.Where(x => string.IsNullOrWhiteSpace(x.IS_NOTIFICATION)).ToList();
        }

        protected override Monitor GetPreviousMonitor(Monitor monitor)
        {
            var result = _previousMonitors.Where(x => x.DEVICE_SN == monitor.DEVICE_SN)
                                          .OrderByDescending(y => y.RECORD_SN);

            if (result.Count() == 0)
                return new Monitor();

            return result.First();
        }

        protected override NotificationCondition GetNotificationCondition(string deviceSn)
        {
            return _notificationConditions.Find(x => x.DEVICE_SN == deviceSn);
        }

        protected override Notification GetNotificationRecord(Monitor monitor, NotificationCondition condition)
        {
            IEnumerable<Notification> records;
            var level = (IntervalLevel)Enum.Parse(typeof(IntervalLevel), condition.INTERVAL_LEVEL);

            switch (level)
            {
                case IntervalLevel.Device:
                    records = _notifications.Where(x => x.DEVICE_SN == monitor.DEVICE_SN)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                case IntervalLevel.MonitorTarget:
                    records = _notifications.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                case IntervalLevel.TargetMessage:
                    records = _notifications.Where(x => x.DEVICE_SN == monitor.DEVICE_SN && x.TARGET_NAME == monitor.TARGET_NAME && x.TARGET_MESSAGE == monitor.TARGET_MESSAGE)
                                                 .OrderByDescending(y => y.NOTIFICATION_TIME);
                    break;

                default:
                    throw new Exception($"無 { condition.INTERVAL_LEVEL } 層級定義");
            }

            return records.Count() > 0 ? records.First() : new Notification();
        }

        protected override void Save()
        {
            //不實作
        }
    }
}