using System.Net;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 訊息推送處理介面
    /// </summary>
    public interface IPushAction
    {
        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        Task<HttpStatusCode> PushMessage();
    }
}