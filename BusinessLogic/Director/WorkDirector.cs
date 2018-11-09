using ModelLibrary;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Director
{
    /// <summary>
    /// 工作管理站
    /// </summary>
    public class WorkDirector
    {
        private readonly string _originRecord;
        internal IEnumerable<DeviceMonitor> Monitors { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="originRecord">原始訊息</param>
        public WorkDirector(string originRecord)
        {
            _originRecord = originRecord;
        }

        /// <summary>
        /// 執行
        /// </summary>
        public void Execute()
        {
            //解析器建立、告警器建立
            //解析原始訊息
            //使用解析後訊息取得對應設備
            //使用設備編號取得告警條件 | 如果沒有設備，告警條件會由程式自己給
            //使用告警條件檢查解析後訊息是否告警
            throw new NotImplementedException();
        }
    }
}