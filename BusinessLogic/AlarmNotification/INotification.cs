using BusinessLogic.RemoteNotification;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System.Collections.Generic;

namespace BusinessLogic.AlarmNotification
{
    /// <summary>
    /// 通知介面
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="alarm">告警物件</param>
        /// <param name="setting">通知設定</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        bool IsNotification(Alarm alarm, NotificationCondition setting, List<NotificationRecord> records);

        /// <summary>
        /// 通知物件取得
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="logSn">記錄編號</param>
        /// <returns></returns>
        GenericRemoteContent GetPayload(EventType type, string deviceSn, int? logSn);

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="data">通知記錄</param>
        void Save(NotificationRecord data);
    }
}