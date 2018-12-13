using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLogic.RecordParser
{
    /// <summary>
    /// Logmaster 解析器
    /// </summary>
    public class LogmasterParser : IParser
    {
        /// <summary>
        /// 原始記錄解析
        /// </summary>
        /// <param name="raw">原始記錄</param>
        /// <param name="sourceIp">來源 IP</param>
        /// <returns></returns>
        public List<Monitor> ParseRecord(string raw, string sourceIp = null)
        {
            var data = JsonConvert.DeserializeObject<ReceiveFormUrlEncoded>(raw);

            return new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = data.DEVICE_ID,
                    TARGET_NAME = "block ip",
                    TARGET_VALUE = "detect block ip",
                    TARGET_MESSAGE = data.LOG_INFO,
                    RECEIVE_TIME = data.LOG_TIME
                }
            };
        }
    }
}