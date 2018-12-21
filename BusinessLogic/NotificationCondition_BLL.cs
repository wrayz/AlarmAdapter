using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 通知條件商業邏輯
    /// </summary>
    public class NotificationCondition_BLL : GenericBusinessLogic<NotificationCondition>
    {
        /// <summary>
        /// 通知條件取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        public NotificationCondition GetNotificationCondition(string deviceSn)
        {
            var condition = new NotificationCondition
            {
                DEVICE_SN = deviceSn
            };

            return _dao.Get(new QueryOption(), condition);
        }
    }
}