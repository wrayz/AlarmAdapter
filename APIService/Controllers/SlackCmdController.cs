using APIService.Model;
using BusinessLogic.Event;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Slack cmd 觸發事件
    /// </summary>
    public class SlackCmdController : ApiController
    {
        /// <summary>
        /// Slack cmd
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetResource(SlackCmdMessage message)
        {
            //Slack Verification Token 驗證
            if (!GenericAPIService.TokenValidate(message.token))
                return Content(HttpStatusCode.Unauthorized, new APIResponse("來源token未認證，請檢查 Slack Verification Token 設置"));

            //使用者資料取得 (By slack id)
            var user = GenericAPIService.GetUserInfo(message.user_id);

            //系統帳號對應Slack id 驗證
            if (string.IsNullOrEmpty(user.USERID))
                return Content(HttpStatusCode.Forbidden, new APIResponse("尚未在 EyesFree 系統建立對應帳號"));

            //cmd 處理物件
            var bll = SlackCmdFactory.CreateInstance(message.command);
            //cmd 處理動作
            var output = bll.Process(message.user_id, message.channel_id, message.text);

            return Ok(output);
        }
    }
}