using ModelLibrary;

namespace BusinessLogic
{
    /// <summary>
    /// 告警通知記錄商業邏輯
    /// </summary>
    public class NotificationRecord_BLL : GenericBusinessLogic<NotificationRecord>
    {
        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="data">設備通知記錄</param>
        /// <returns></returns>
        public void SaveNotification(NotificationRecord data)
        {
            _dao.Modify("Save", data);
        }


        public bool CheckNotification(NotificationRecord condition)
        {
            return _dao.GetCount(new ModelLibrary.Generic.QueryOption(), condition) > 0;
        }
    }
}