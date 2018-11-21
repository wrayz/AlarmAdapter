using BusinessLogic.ContentStrategy;
using ModelLibrary;

namespace APIService.PushStrategy
{
    /// <summary>
    /// 維修資訊推播策略
    /// </summary>
    internal class RepairPushStrategy : GenericPushStrategy
    {
        private readonly Repair _repair;
        private GenericContentStrategy _content;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="repair">維修資訊</param>
        public RepairPushStrategy(Repair repair)
        {
            _repair = repair;
        }

        /// <summary>
        /// 推播執行
        /// </summary>
        public override void Execute()
        {
            InitContent();

            PushDestination(_content);
        }

        /// <summary>
        /// 通知內容初始化
        /// </summary>
        protected override void InitContent()
        {
            _content = new RepairContent(_repair);
        }
    }
}