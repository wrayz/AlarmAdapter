using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.RecordParser
{
    public class CameraParser : IParser
    {
        /// <summary>
        /// 原始記錄解析
        /// </summary>
        /// <param name="raw">原始記錄</param>
        /// <returns></returns>
        public List<Monitor> ParseRecord(string raw, string sourceIp = null)
        {
            var monitors = new List<Monitor>();
            var data = raw.Split('&');

            foreach (var item in data)
            {
                Monitor monitor = Parsing(item, sourceIp);
                monitors.Add(monitor);
            }

            return monitors;
        }

        private Monitor Parsing(string item, string sourceIp)
        {
            var result = item.Split('=');
            return new Monitor
            {
                DEVICE_ID = sourceIp,
                TARGET_NAME = result[0],
                TARGET_VALUE = result[1],
                TARGET_MESSAGE = result[1],
                RECEIVE_TIME = DateTime.Now
            };
        }
    }
}