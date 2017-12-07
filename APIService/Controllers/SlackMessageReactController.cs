using APIService.Model;
using BusinessLogic.Event;
using ModelLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIService.Controllers
{
    /// <summary>
    /// Slack button 觸發事件
    /// </summary>
    public class SlackMessageReactController : ApiController
    {
        /// <summary>
        /// 修復紀錄
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Interact(SlackInteractMessage message)
        {
            try
            {
                var payload = JsonConvert.DeserializeObject<SlackPayload>(message.payload);

                //Slack Verification Token 驗證
                if (!GenericAPIService.TokenValidate(payload.SLACK_TOKEN))
                    return Ok(new CmdResponse { TEXT_CONTENT = "來源token未認證，請檢查 Slack Verification Token 設置" });

                //使用者資料取得 (By slack id)
                var user = GenericAPIService.GetUserInfo(payload.USER_INFO.SLACK_ID);

                //系統帳號對應Slack id 驗證
                if (string.IsNullOrEmpty(user.USERID))
                    return Ok(new CmdResponse { TEXT_CONTENT = "尚未在 EyesFree 系統建立對應帳號" });

                //紀錄動作處理物件
                var bll = new EventBusinessLogic();

                //對應設備編號
                string device = bll.GetDeviceByLog(payload.CALLBACK_ID);

                if (!string.IsNullOrEmpty(device))
                {
                    //log紀錄
                    var log = new Log { LOG_SN = payload.CALLBACK_ID, DEVICE_SN = device, ACTION_TYPE = "Repair", USERID = user.USERID };
                    //紀錄處理
                    var deviceLog = bll.LogModify(log);
                    //詳細記錄資訊取得
                    var detail = bll.GetLogDetail(deviceLog.LOG_SN);

                    //Slack訊息推送結果
                    var slackResponse = await bll.PushSlack(log.ACTION_TYPE, detail);
                    //IM訊息推送結果
                    var imResponse = await bll.PushIM(log.ACTION_TYPE, detail);

                    if (slackResponse != HttpStatusCode.OK)
                        return Ok(new CmdResponse { TEXT_CONTENT = "修復紀錄成功，但推送至Slack未獲得授權" });

                    if (imResponse != HttpStatusCode.OK)
                        return Ok(new CmdResponse { TEXT_CONTENT = "修復紀錄成功，但推送至IM時失敗" });

                    return Ok();
                }
                else
                {
                    return Ok(new CmdResponse { TEXT_CONTENT = "無對應設備，或對應設備狀態不符" });
                }
            }
            catch
            {
                return Content(HttpStatusCode.InternalServerError, new CmdResponse { TEXT_CONTENT = "500 內部伺服器錯誤" });
            }
        }
    }
}