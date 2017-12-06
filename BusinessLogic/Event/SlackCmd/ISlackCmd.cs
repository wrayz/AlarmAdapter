namespace BusinessLogic.Event
{
    /// <summary>
    /// Slack cmd 處理動作介面
    /// </summary>
    public interface ISlackCmd
    {
        /// <summary>
        /// Slack cmd 處理動作
        /// </summary>
        /// <param name="id">使用者Slack ID</param>
        /// <param name="channel">頻道 ID</param>
        /// <param name="text">參數字串</param>
        /// <returns></returns>
        CmdResponse Process(string id, string channel, string text);
    }
}