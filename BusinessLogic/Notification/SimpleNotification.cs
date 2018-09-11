using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Notification
{
    public class SimpleNotification : INotification
    {
        public Payload GetPayload(EventType type, string deviceSn, int? logSn)
        {
            throw new NotImplementedException();
        }

        public bool IsNotification(DateTime? time, NotificationSetting setting, List<NotificationRecord> records)
        {
            throw new NotImplementedException();
        }
    }
}