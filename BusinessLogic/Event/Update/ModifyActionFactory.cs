using ModelLibrary;
using SlackAPIHelper.Content.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 紀錄更新動作工廠
    /// </summary>
    public static class ModifyActionFactory
    {
        /// <summary>
        /// 紀錄動作
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">紀錄詳細資料</param>
        /// <param name="token">OAuth Acess Token</param>
        /// <param name="stamps">紀錄標記清單</param>
        /// <returns></returns>
        public static async Task Modify(EventType type, LogDetail log, string token, IEnumerable<LogSlackStamp> stamps)
        {
            //紀錄更新動作物件
            var action = new ModifyAction();
            //紀錄內容
            IEnumerable<ChatUpdateRequest> messages = new List<ChatUpdateRequest>();
            //已更新標記
            IEnumerable<LogSlackStamp> updatedStamps = new List<LogSlackStamp>();

            switch (type)
            {
                case EventType.Error:
                    //紀錄標記資料更新
                    action.ModifyLogStamp("Insert", stamps);
                    return;

                case EventType.Repair:
                    //紀錄標記清單取得
                    stamps = action.GetLogStamps(log.LOG_SN);
                    //紀錄內容產生
                    messages = action.GenerateLogMessage(log, token, stamps);
                    //Slack 訊息更新
                    updatedStamps = await action.UpdateMessages(log.LOG_SN, messages);
                    //紀錄標記資料更新
                    action.ModifyLogStamp("Delete", updatedStamps);
                    return;

                case EventType.Recover:
                    //紀錄標記清單取得
                    stamps = action.GetLogStamps(log.LOG_SN);
                    //紀錄內容產生
                    messages = action.GenerateLogMessage(log, token, stamps);
                    //Slack 訊息更新
                    updatedStamps = await action.UpdateMessages(log.LOG_SN, messages);
                    //紀錄標記資料更新
                    action.ModifyLogStamp("Delete", updatedStamps);
                    return;

                default:
                    throw new Exception("更新方法錯誤");
            }
        }
    }
}