using BusinessLogic.NotificationContent;
using ModelLibrary;

namespace APIService.PushStrategy
{
    /// <summary>
    /// 維修資訊推播策略
    /// </summary>
    internal class RepairPushStrategy : GenericPushStrategy
    {
        private readonly Repair _repair;

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
            var content = new RepairContent(_repair);
            PushDestination(content);
        }
    }
}