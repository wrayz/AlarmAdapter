using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Notification
{
    public class DigitalNotification : INotification
    {
        public Payload GetPayload(string type, string deviceSn, int? logSn)
        {
            throw new NotImplementedException();
        }

        public bool IsNotification(DateTime? time, NotificationSetting setting, List<NotificationRecord> records)
        {
            throw new NotImplementedException();
        }
    }
}