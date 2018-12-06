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
        private List<GenericContentStrategy> _contents;

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
            InitContent();

            _contents.ForEach(content =>
            {
                PushDestination(content);

                Save(content);
            });
        }

        /// <summary>
        /// 通知內容初始化
        /// </summary>
        protected override void InitContent()
        {
            _contents = new List<GenericContentStrategy>();

            _notifications.ForEach(notification =>
             {
                 GenericContentStrategy content = new CactiContent(notification);
                 _contents.Add(content);
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
        /// <param name="content">通知內容</param>
        private void Save(GenericContentStrategy content)
        {
            var bll = GenericBusinessFactory.CreateInstance<Notification>();
            (bll as Notification_BLL).Update(content);
        }
    }
}