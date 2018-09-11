using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 簡易設備記錄商業邏輯
    /// </summary>
    public class SimpleLog_BLL : GenericBusinessLogic<SimpleLog>
    {
        /// <summary>
        /// 記錄新增
        /// </summary>
        /// <param name="log">簡易設備異常記錄</param>
        /// <param name="type"></param>
        public SimpleLog ModifyLog(SimpleLog log, string type)
        {
            var result = (_dao as SimpleLog_DAO).ModifyLog(log, type);
            log.LOG_SN = result.LOG_SN;

            return log;
        }
    }
}