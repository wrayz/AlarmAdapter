using System;

namespace BusinessLogic.Event
{
    /// <summary>
    /// Slack cmd 處理工廠
    /// </summary>
    public static class SlackCmdFactory
    {
        /// <summary>
        /// cmd 處理物件取得
        /// </summary>
        /// <param name="cmd">指令字串</param>
        /// <returns></returns>
        public static ISlackCmd CreateInstance(string cmd)
        {
            switch (cmd)
            {
                //異常設備報表
                case "/report":
                    return new SlackCmdReport();

                default:
                    throw new Exception("指令錯誤，請檢查輸入的指令內容");
            }
        }
    }
}