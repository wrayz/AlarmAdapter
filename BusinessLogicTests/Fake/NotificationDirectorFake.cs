using BusinessLogic.Director;
using BusinessLogic.NotificationStrategy;
using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogicTests.Fake
{
    internal class NotificationDirectorFake : NotificationDirector
    {
        public NotificationDirectorFake(NotifierStrategy strategy) : base(strategy)
        {
        }

        protected override List<Monitor> GetMonitors()
        {
            return base.GetMonitors();
        }

        protected override NotificationCondition GetNotificationCondition(string deviceSn)
        {
            return base.GetNotificationCondition(deviceSn);
        }

        protected override Notification GetNotificationRecord(Monitor monitor, NotificationCondition condition)
        {
            return base.GetNotificationRecord(monitor, condition);
        }

        protected override Monitor GetPreviousMonitor(Monitor monitor)
        {
            return base.GetPreviousMonitor(monitor);
        }

        protected override void Save()
        {
            base.Save();
        }
    }
}