using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.RecordParser
{
    public interface IParser
    {
        /// <summary>
        /// 原始記錄解析
        /// </summary>
        /// <param name="raw">原始記錄</param>
        /// <param name="sourceIp">來源 IP</param>
        /// <returns></returns>
        List<Monitor> ParseRecord(string raw, string sourceIp = null);
    }
}