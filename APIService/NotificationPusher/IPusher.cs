using BusinessLogic.NotificationContent;
using System.Net.Http;

namespace APIService.NotificationPusher
{
    /// <summary>
    /// 推播器介面
    /// </summary>
    internal interface IPusher
    {
        /// <summary>
        /// 推播
        /// </summary>
        /// <param name="content">通知內容</param>
        /// <returns></returns>
        HttpResponseMessage Push(GenericContent content);
    }
}