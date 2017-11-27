using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
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
        /// 推送資訊內容
        /// </summary>
        private List<ChatPostRequest> _messages = new List<ChatPostRequest>();

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        public PushSlack(EventType type, LogDetail log)
        {
            //推送內容清單設置
            SetContent(type, log);
        }

        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        public async Task<HttpStatusCode> PushMessage()
        {
            var status = HttpStatusCode.OK;

            if (_messages.Count() > 0)
            {
                foreach (var message in _messages)
                {
                    var response = await SlackAPI.PostRequest("chat.postMessage", message);
                    //Slack訊息推送
                    if ((response as ChatPostResponse).Ok == false)
                        status = HttpStatusCode.Unauthorized;
                }
            }

            return status;
        }

        /// <summary>
        /// 推送內容清單設置
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        private void SetContent(EventType type, LogDetail log)
        {
            //Content 清單
            var list = new List<ChatPostRequest>();

            //Slack設置取得
            var dao = GenericDataAccessFactory.CreateInstance<SlackConfig>();
            var option = new QueryOption();

            var config = dao.Get(option);

            //推送資訊卡片
            var attachment = new Attachment(type, log);

            //修復按鈕
            if (type == EventType.Error)
            {
                attachment.BUTTON_LIST = new List<Action>
                {
                    new Action { BUTTON_NAME = "Repair", BUTTON_TEXT = "修復", BUTTON_TYPE = "button", BUTTON_STYLE = "danger" }
                };
            }

            //欲推送頻道清單
            var groups = log.GROUP_LIST.Where(c => c.CHANNEL_ID != "");

            foreach (var group in groups)
            {
                var message = new ChatPostRequest
                {
                    username = "EyesFree",
                    text = attachment.TEXT_CONTENT,
                    channel = group.CHANNEL_ID,
                    token = config.SLACK_TOKEN,
                    attachments = _encoding.GetString(_encoding.GetBytes(JsonConvert.SerializeObject(new List<object> { attachment })))
                };

                _messages.Add(message);
            }
        }
    }
}