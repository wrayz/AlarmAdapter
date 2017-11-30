using ModelLibrary;
using Newtonsoft.Json;
using SlackAPIHelper;
using SlackAPIHelper.Content;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 訊息推送處理物件(Slack)
    /// </summary>
    public class PushSlack : IPushAction
    {
        /// <summary>
        /// 編碼格式
        /// </summary>
        private readonly Encoding _encoding = new UTF8Encoding();

        /// <summary>
        /// 動作類型
        /// </summary>
        private EventType _type;

        /// <summary>
        /// OAuth Access token
        /// </summary>
        private string _token;

        /// <summary>
        /// 設備紀錄詳細資料
        /// </summary>
        private LogDetail _log;

        /// <summary>
        /// 推送資訊內容
        /// </summary>
        private List<ChatPostMessageRequest> _messages = new List<ChatPostMessageRequest>();

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="token">OAuth Access token</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public PushSlack(EventType type, string token, LogDetail log)
        {
            //動作類型
            _type = type;
            //OAuth Access token
            _token = token;
            //設備紀錄詳細資料
            _log = log;
            //推送內容清單設置
            SetContent();
        }

        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> PushMessage()
        {
            var status = HttpStatusCode.OK;

            //訊息標記清單
            var list = new List<LogSlackStamp>();

            foreach (var message in _messages)
            {
                //Slack訊息推送
                var response = (await SlackAPI.PostRequest("chat.postMessage", message)) as ChatPostMessageResponse;

                if (!response.Ok)
                    status = HttpStatusCode.Unauthorized;
                else
                    list.Add(new LogSlackStamp { LOG_SN = _log.LOG_SN, CHANNEL_ID = message.channel, TIME_STAMP = response.TimeStamp });
            }

            await ModifyActionFactory.Modify(_type, _log, _token, list);

            return status;
        }

        /// <summary>
        /// 推送內容清單設置
        /// </summary>
        private void SetContent()
        {
            //推送資訊卡片
            var attachment = new Attachment(_type, _log);

            //修復按鈕
            if (_type == EventType.Error)
            {
                attachment.BUTTON_LIST = new List<Action>
                {
                    new Action { BUTTON_NAME = "Repair", BUTTON_TEXT = "修復", BUTTON_TYPE = "button", BUTTON_STYLE = "danger" }
                };
            }

            //欲推送頻道清單
            var groups = _log.GROUP_LIST.Where(c => c.CHANNEL_ID != "");

            foreach (var group in groups)
            {
                var message = new ChatPostMessageRequest
                {
                    username = "EyesFree",
                    text = attachment.TEXT_CONTENT,
                    channel = group.CHANNEL_ID,
                    token = _token,
                    attachments = _encoding.GetString(_encoding.GetBytes(JsonConvert.SerializeObject(new List<object> { attachment })))
                };

                _messages.Add(message);
            }
        }
    }
}