using ModelLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public List<DeviceMonitor> ParseRecord(string raw)
        {
            var monitors = new List<DeviceMonitor>();
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);

            return new List<DeviceMonitor>
            {
                new DeviceMonitor
                {
                    DEVICE_ID = data["id"],
                    TARGET_NAME = data["target"],
                    TARGET_VALUE = data["action"],
                    TARGET_CONTENT = data["info"],
                    RECEIVE_TIME = data["time"]
                }
            };
        }
    }
}