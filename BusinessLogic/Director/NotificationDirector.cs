﻿using BusinessLogic.NotificationStrategy;
using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.Director
{
    /// <summary>
    /// 通知管理站
    /// </summary>
    public class NotificationDirector
    {
        private readonly NotifierStrategy _notifierStrategy;

        protected internal List<Monitor> Monitors { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="strategy">通知策略</param>
        public NotificationDirector(NotifierStrategy strategy)
        {
            _notifierStrategy = strategy;
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            Monitors = GetMonitors();

            Monitors.ForEach(monitor =>
            {
                //TODO: 使用靜態 Dictionary 將前次監控資訊存在 Memory（重開機要在初始化進資料庫）
                var previousMonitor = GetPreviousMonitor(monitor);
                var condition = GetNotificationCondition(monitor.DEVICE_SN);
                var notification = GetNotificationRecord(monitor, condition);

                monitor.IS_NOTIFICATION = _notifierStrategy.IsNotification(condition, monitor, previousMonitor, notification);
            });

            Save();
        }

        /// <summary>
        /// 通知待檢查監控資訊清單
        /// </summary>
        /// <returns></returns>
        protected virtual List<Monitor> GetMonitors()
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            return (bll as Monitor_BLL).GetNotificationMonitors();
        }

        /// <summary>
        /// 前次監控訊息取得
        /// </summary>
        /// <param name="monitor">當前監控訊息</param>
        /// <returns></returns>
        protected virtual Monitor GetPreviousMonitor(Monitor monitor)
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            return (bll as Monitor_BLL).GetPreviousMonitor(monitor);
        }

        /// <summary>
        /// 通知條件取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        protected virtual NotificationCondition GetNotificationCondition(string deviceSn)
        {
            var bll = GenericBusinessFactory.CreateInstance<NotificationCondition>();
            return (bll as NotificationCondition_BLL).GetNotificationCondition(deviceSn);
        }

        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="monitor">監控資訊</param>
        /// <param name="condition">通知條件</param>
        /// <returns></returns>
        protected virtual Notification GetNotificationRecord(Monitor monitor, NotificationCondition condition)
        {
            var bll = GenericBusinessFactory.CreateInstance<Notification>();
            return (bll as Notification_BLL).GetRecord(monitor, condition);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        protected virtual void Save()
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            (bll as Monitor_BLL).UpdateList(Monitors);
        }
    }
}