using BusinessLogic.NotificationStrategy;
using ModelLibrary;
using System.Collections.Generic;
using System.Linq;

namespace APIService.Strategy
{
    /// <summary>
    /// 監控資訊推播策略
    /// </summary>
    internal class MonitorPushStrategy : PushStrategy
    {
        private readonly List<Monitor> _monitors;
        private List<ContentStrategy> _contents;

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
        }

        /// <summary>
        /// 通知內容初始化
        /// </summary>
        protected override void InitContent()
        {
            _contents = new List<ContentStrategy>();

            _monitors.Where(x => x.IS_NOTIFICATION == "Y")
                     .ToList()
                     .ForEach(monitor =>
                     {
                         ContentStrategy content = new CactiContent(monitor);
                         _contents.Add(content);
                     });
        }
    }
}