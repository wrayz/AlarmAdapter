using APIService.Model;
using BusinessLogic;
using BusinessLogic.Event;
using ModelLibrary;
using ModelLibrary.Generic;
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

            //token 驗證
            if (Validate(payload.SLACK_TOKEN))
                return Content(HttpStatusCode.Unauthorized, new APIResponse("來源token未認證"));

            //紀錄動作處理物件
            var bll = new EventBusinessLogic();

            //對應設備編號
            string device = bll.GetDeviceByLog(payload.CALLBACK_ID);

            if (!string.IsNullOrEmpty(device))
            {
                //log紀錄
                var log = new Log { LOG_SN = payload.CALLBACK_ID, ACTION_TYPE = "Repair" };
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

        /// <summary>
        /// token驗證
        /// </summary>
        /// <param name="token">slack token</param>
        /// <returns></returns>
        private bool Validate(string token)
        {
            //邏輯物件
            var bll = GenericBusinessFactory.CreateInstance<SlackConfig>();
            //Slack設置物件
            var config = bll.Get(new QueryOption(), new UserLogin());

            return config.OAUTH_TOKEN == token;
        }
    }
}