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
        public List<Monitor> ParseRecord(string raw, string souceIp = null)
        {
            var data = raw.Split('=');

            return new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = souceIp,
                    TARGET_NAME = data[0],
                    TARGET_VALUE = data[1],
                    TARGET_MESSAGE = data[1],
                    RECEIVE_TIME = DateTime.Now
                }
            };
        }
    }
}