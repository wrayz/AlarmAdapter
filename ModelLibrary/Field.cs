using Newtonsoft.Json;

namespace ModelLibrary
{
    /// <summary>
    /// 推送訊息物件詳細內容
    /// </summary>
    public class Field
    {
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="title">標題</param>
        /// <param name="value">內容</param>
        /// <param name="type">子方塊長短</param>
        public Field(string title, string value, bool type)
        {
            //標題
            FIELD_NAME = title;
            //內容
            FIELD_CONTENT = value;
            //子方塊長短
            FIELD_TYPE = type;
        }

        /// <summary>
        /// 標題
        /// </summary>
        [JsonProperty("title")]
        public string FIELD_NAME { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [JsonProperty("value")]
        public string FIELD_CONTENT { get; set; }

        /// <summary>
        /// 子方塊長短 (true - 短, false - 長)
        /// </summary>
        [JsonProperty("short")]
        public bool FIELD_TYPE { get; set; }
    }
}