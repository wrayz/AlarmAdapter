using BusinessLogic.ContentStrategy;
using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace APIService.PushStrategy
{
    /// <summary>
    /// 監控資訊推播策略
    /// </summary>
    internal class MonitorPushStrategy : GenericPushStrategy
    {
        private readonly List<Monitor> _monitors;
        private List<GenericContentStrategy> _contents;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="monitors">監控資訊清單</param>
        public MonitorPushStrategy(List<Monitor> monitors)
        {
            _monitors = monitors;
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
            });

            Save();
        }

        /// <summary>
        /// 通知內容初始化
        /// </summary>
        protected override void InitContent()
        {
            _contents = new List<GenericContentStrategy>();

            _monitors.Where(x => x.IS_NOTIFICATION == "Y")
                     .ToList()
                     .ForEach(monitor =>
                     {
                         GenericContentStrategy content = new CactiContent(monitor);
                         _contents.Add(content);
                     });
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        private void Save()
        {
            //TODO: 通知層獨立後，方法改用更新通知記錄

            //var bll = GenericBusinessFactory.CreateInstance<Notification>();
            //(bll as Notification_BLL).SaveMonitorNotifications(_monitors);
        }
    }
}