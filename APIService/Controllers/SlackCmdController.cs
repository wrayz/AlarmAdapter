using APIService.Model;
using BusinessLogic.Event;
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
            try
            {
                //Slack Verification Token 驗證
                if (!GenericAPIService.TokenValidate(message.token))
                    return Ok(new CmdResponse { TEXT_CONTENT = "來源token未認證，請檢查 Slack Verification Token 設置" });

                //使用者資料取得 (By slack id)
                var user = GenericAPIService.GetUserInfo(message.user_id);

                //系統帳號對應Slack id 驗證
                if (string.IsNullOrEmpty(user.USERID))
                    return Ok(new CmdResponse { TEXT_CONTENT = "尚未在 EyesFree 系統建立對應帳號" });

                //cmd 處理物件
                var bll = SlackCmdFactory.CreateInstance(message.command);
                //cmd 處理動作
                var output = bll.Process(message.user_id, message.channel_id, message.text);

                return Ok(output);
            }
            catch
            {
                return Ok(new CmdResponse { TEXT_CONTENT = "500 內部伺服器錯誤" });
            }
        }
    }
}