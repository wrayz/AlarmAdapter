using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLogic.RecordParser
{
    /// <summary>
    /// Cacti 解析器
    /// </summary>
    public class CactiParser : IParser
    {
        /// <summary>
        /// 訊息解析
        /// </summary>
        /// <param name="raw">原始訊息</param>
        /// <returns></returns>
        public List<Monitor> ParseRecord(string raw, string sourceIp = null)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);

            return new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = data["id"],
                    TARGET_NAME = data["target"],
                    TARGET_VALUE = data["action"],
                    TARGET_MESSAGE = data["info"],
                    RECEIVE_TIME = DateTime.Parse(data["time"], CultureInfo.InvariantCulture)
                }
            };
        }
    }
}