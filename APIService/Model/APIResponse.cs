using Newtonsoft.Json;

namespace APIService.Model
{
    /// <summary>
    /// 錯誤訊息API物件
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="message">錯誤訊息</param>
        public APIResponse(string message = null)
        {
            Message = message;
        }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}