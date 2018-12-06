using BusinessLogic;
using BusinessLogic.ContentStrategy;
using ModelLibrary;
using System.Collections.Generic;

namespace APIService.PushStrategy
{
    /// <summary>
    /// 監控資訊推播策略
    /// </summary>
    internal class MonitorPushStrategy : GenericPushStrategy
    {
        private readonly List<Notification> _notifications;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="monitors">監控資訊清單</param>
        public MonitorPushStrategy()
        {
            _notifications = GetNotifications();
        }

        /// <summary>
        /// 推播執行
        /// </summary>
        public override void Execute()
        {
            _notifications.ForEach(notification =>
             {
                 GenericContentStrategy content = new CactiContent(notification);
                 PushDestination(content);
                 Save(notification);
             });
        }

        /// <summary>
        /// 待通知清單取得
        /// </summary>
        /// <returns></returns>
        private List<Notification> GetNotifications()
        {
            var bll = GenericBusinessFactory.CreateInstance<Notification>();
            return (bll as Notification_BLL).GetPendingNotifications();
        }

        /// <summary>
        /// 通知儲存
        /// </summary>
        /// <param name="condition">實體條件</param>
        private void Save(Notification condition)
        {
            var bll = GenericBusinessFactory.CreateInstance<Notification>();
            (bll as Notification_BLL).Update(condition);
        }
    }
}