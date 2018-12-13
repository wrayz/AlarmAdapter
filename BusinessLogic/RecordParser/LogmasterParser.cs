using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

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
            var target = "detect block ip";
            var value = Regex.Split(data.LOG_INFO, target)[1].Trim();

            return new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = data.DEVICE_ID,
                    TARGET_NAME = target,
                    TARGET_VALUE = value,
                    TARGET_MESSAGE = data.LOG_INFO,
                    RECEIVE_TIME = data.LOG_TIME
                }
            };
        }
    }
}