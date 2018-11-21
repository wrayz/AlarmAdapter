using ModelLibrary.Enumerate;
using System;

namespace BusinessLogic.RecordNotifier
{
    /// <summary>
    /// 狀態通知器產生工程
    /// </summary>
    public static class StatusNotifierFactory
    {
        /// <summary>
        /// 狀態通知器實體建立
        /// </summary>
        /// <param name="type">狀態類型</param>
        /// <returns></returns>
        public static IStatusNotifier CreateInstance(NotificationType type)
        {
            IStatusNotifier notifier;

            switch (type)
            {
                case NotificationType.StatusChange:
                    notifier = new StatusChangeNotifier();
                    break;

                default:
                    throw new Exception($"尚未實作 {type} 通知器");
            }

            return notifier;
        }
    }
}