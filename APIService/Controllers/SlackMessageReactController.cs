using APIService.Model;
using BusinessLogic.Event;
using ModelLibrary;
using Newtonsoft.Json;
using System;
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
            var payload = JsonConvert.DeserializeObject<SlackPayload>(message.payload);

            //Slack Verification Token 驗證
            if (!GenericAPIService.TokenValidate(payload.SLACK_TOKEN))
                return Content(HttpStatusCode.Unauthorized, new APIResponse("來源token未認證，請檢查 Slack Verification Token 設置"));

            //使用者資料取得 (By slack id)
            var user = GenericAPIService.GetUserInfo(payload.USER_INFO.SLACK_ID);

            //系統帳號對應Slack id 驗證
            if(string.IsNullOrEmpty(user.USERID))
                return Content(HttpStatusCode.Forbidden, new APIResponse("尚未在 EyesFree 系統建立對應帳號"));

            //紀錄動作處理物件
            var bll = new EventBusinessLogic();

            //對應設備編號
            string device = bll.GetDeviceByLog(payload.CALLBACK_ID);

            if (!string.IsNullOrEmpty(device))
            {
                //log紀錄
                var log = new Log { LOG_SN = payload.CALLBACK_ID, ACTION_TYPE = "Repair", USERID = user.USERID };
                //紀錄處理
                var deviceLog = bll.LogModify(log);
                //詳細記錄資訊取得
                var detail = bll.GetLogDetail(deviceLog.LOG_SN);
                //訊息推送結果
                var responses = await bll.PushEvent(log.ACTION_TYPE, detail);

                if (Array.IndexOf(responses, HttpStatusCode.Unauthorized) != -1)
                    return Content(HttpStatusCode.Unauthorized, new APIResponse("Log紀錄成功，但推送至IM或Slack未獲得授權"));

                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, new APIResponse("無對應設備，或對應設備狀態不符"));
            }
        }
    }
}