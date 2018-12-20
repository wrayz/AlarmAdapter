using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.RecordParser
{
    /// <summary>
    /// iFace 溫濕度計解析器
    /// </summary>
    public class IfaceParser : IParser
    {
        /// <summary>
        /// 訊息解析
        /// </summary>
        /// <param name="raw">原始訊息</param>
        /// <param name="sourceIp">來源 IP</param>
        /// <returns></returns>
        public List<Monitor> ParseRecord(string raw, string sourceIp = null)
        {
            var result = new List<Monitor>();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);

            var targetNames = data.Keys.Where(x => x.Contains("_"))
                                       .Select(y =>
                                       {
                                           return y.Split('_')[0];
                                       })
                                       .Distinct()
                                       .Skip(1);

            var count = data.Keys.Count(key => key.Contains("Name_"));

            for (int i = 1; i <= count; i++)
            {
                if (string.IsNullOrWhiteSpace(data[$"Name_{ i }"]))
                    continue;

                var monitors = targetNames.Select(name => new Monitor
                {
                    DEVICE_ID = data[$"Name_{ i }"],
                    TARGET_NAME = name,
                    TARGET_VALUE = data[$"{name}_{i}"],
                    TARGET_MESSAGE = data[$"{name}_{i}"],
                    RECEIVE_TIME = DateTime.Now
                });

                result.AddRange(monitors);
            }

            return result;
        }
    }
}