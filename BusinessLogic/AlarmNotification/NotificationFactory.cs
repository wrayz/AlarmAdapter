using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.AlarmNotification
{
    public static class NotificationFactory
    {
        public static INotification CreateInstance(DeviceType type)
        {
            INotification notification;

            switch (type)
            {
                case DeviceType.D:
                    notification = new DigitalNotification();
                    break;

                default:
                    throw new Exception("無此設備");
            }

            return notification;
        }
    }
}