using BusinessLogic;
using BusinessLogic.NotificationContent;
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
        /// <summary>
        /// 推播執行
        /// </summary>
        public override void Execute()
        {
            var notifications = GetNotifications();

            var list = notifications.GroupBy(x => x.RECORD_SN, (k, r) => new
            {
                Key = k,
                Result = r.ToList()
            });

            foreach (var data in list)
            {
                var type = data.Result.First().DEVICE.DEVICE_TYPE;
                var content = NotificationContentFactory.CreateInstance(type, data.Result);
                PushDestination(content);
                Save(data.Result);
            }
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
        /// <param name="data">實體資料</param>
        private void Save(List<Notification> data)
        {
            var bll = GenericBusinessFactory.CreateInstance<Notification>();
            data.ForEach(x => (bll as Notification_BLL).Update(x));
        }
    }
}