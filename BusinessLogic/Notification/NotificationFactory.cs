﻿using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.Notification
{
    public static class NotificationFactory
    {
        public static INotification CreateInstance(DeviceType type)
        {
            INotification notification;

            switch (type)
            {
                case DeviceType.Network:
                    notification = new NetworkNotification();
                    break;

                case DeviceType.Digital:
                    notification = new DigitalNotification();
                    break;

                case DeviceType.Simple:
                    notification = new SimpleNotification();
                    break;

                default:
                    throw new Exception("無此設備");
            }

            return notification;
        }
    }
}