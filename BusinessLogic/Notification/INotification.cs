using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Notification
{
    /// <summary>
    /// 通知介面
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// 是否通知
        /// </summary>
        /// <param name="time">記錄時間</param>
        /// <param name="setting">通知設定</param>
        /// <param name="records">通知記錄清單</param>
        /// <returns></returns>
        bool IsNotification(DateTime? time, NotificationSetting setting, List<NotificationRecord> records);

        /// <summary>
        /// 通知物件取得
        /// </summary>
        /// <param name="type">事件類型</param>
        /// <param name="deviceSn">設備編號</param>
        /// <param name="logSn">記錄編號</param>
        /// <returns></returns>
        Payload GetPayload(string type, string deviceSn, int? logSn);
    }
}