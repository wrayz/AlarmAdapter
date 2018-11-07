using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.RecordParser
{
    public interface IParser
    {
        /// <summary>
        /// 原始記錄解析
        /// </summary>
        /// <param name="record">原始記錄</param>
        /// <returns></returns>
        List<DeviceMonitor> ParseRecord(string record);
    }
}