using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using SlackAPIHelper;
using SlackAPIHelper.Content;
using SlackAPIHelper.Content.Request;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 紀錄更新動作物件
    /// </summary>
    public class ModifyAction
    {
        /// <summary>
        /// 編碼格式
        /// </summary>
        private readonly Encoding _encoding = new UTF8Encoding();

        /// <summary>
        /// 紀錄標記取得
        /// </summary>
        /// <param name="sn">紀錄編號</param>
        /// <returns></returns>
        public IEnumerable<LogSlackStamp> GetLogStamps(string sn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<LogSlackStamp>();
            return dao.GetList(new QueryOption(), new LogSlackStamp { LOG_SN = sn });
        }

        /// <summary>
        /// 紀錄內容產生
        /// </summary>
        /// <param name="log">紀錄詳細資料</param>
        /// <param name="token">OAuth Acess Token</param>
        /// <param name="stamps">紀錄標記清單</param>
        /// <returns></returns>
        public IEnumerable<ChatUpdateRequest> GenerateLogMessage(LogDetail log, string token, IEnumerable<LogSlackStamp> stamps)
        {
            //Content 清單
            var list = new List<ChatUpdateRequest>();

            //推送資訊卡片
            var attachment = new Attachment(EventType.Error, log);

            foreach (var stamp in stamps)
            {
                var message = new ChatUpdateRequest
                {
                    channel = stamp.CHANNEL_ID,
                    text = attachment.TEXT_CONTENT,
                    token = token,
                    ts = stamp.TIME_STAMP,
                    attachments = _encoding.GetString(_encoding.GetBytes(JsonConvert.SerializeObject(new List<object> { attachment })))
                };

                list.Add(message);
            }

            return list;
        }

        /// <summary>
        /// Slack 訊息更新
        /// </summary>
        /// <param name="sn">紀錄編號</param>
        /// <param name="messages">待更新內容清單</param>
        /// <returns></returns>
        public async Task<IEnumerable<LogSlackStamp>> UpdateMessages(string sn, IEnumerable<ChatUpdateRequest> messages)
        {
            var list = new List<LogSlackStamp>();

            foreach (var message in messages)
            {
                //Slack 訊息更新
                var response = (await SlackAPI.PostRequest("chat.update", message)) as ChatUpdateResponse;

                if (response.Ok)
                    list.Add(new LogSlackStamp { LOG_SN = sn, CHANNEL_ID = response.Channel, TIME_STAMP = response.TimeStamp });
            }

            return list;
        }

        /// <summary>
        /// 紀錄標記更新
        /// </summary>
        /// <param name="type">資料處理類型</param>
        /// <param name="stamps">紀錄標記清單</param>
        /// <returns></returns>
        public void ModifyLogStamp(string type, IEnumerable<LogSlackStamp> stamps)
        {
            var dao = GenericDataAccessFactory.CreateInstance<LogSlackStamp>();
            dao.ModifyList(type, stamps);
        }
    }
}