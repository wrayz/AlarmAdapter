using System.Collections.Generic;
using ModelLibrary;
using Newtonsoft.Json;

namespace BusinessLogic.RecordParser
{
    /// <summary>
    /// Bob Cacti 解析器
    /// </summary>
    internal class BobCactiParser : IParser
    {
        public List<Monitor> ParseRecord(string raw, string sourceIp = null)
        {
            var data = JsonConvert.DeserializeObject<ReceiveFormUrlEncoded>(raw);

            return new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = data.DEVICE_ID,
                    TARGET_NAME = "Ping",
                    TARGET_VALUE = data.ACTION_TYPE,
                    TARGET_MESSAGE = data.LOG_INFO,
                    RECEIVE_TIME = data.LOG_TIME
                }
            };
        }
    }
}