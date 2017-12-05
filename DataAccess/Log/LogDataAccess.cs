using ModelLibrary;
using SourceHelper.Enumerate;
using System.Collections.Generic;

namespace DataAccess.Log
{
    /// <summary>
    /// Log處理資料處理
    /// </summary>
    public static class LogDataAccess
    {
        /// <summary>
        /// 正常設備取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        public static Device GetNormalDevice(string id)
        {
            return DeviceQuery(id, OperatorType.Equal, new List<object> { "N" });
        }

        /// <summary>
        /// 異常設備取得(不含修復中)
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        public static Device GetErrorDevice(string id)
        {
            return DeviceQuery(id, OperatorType.Equal, new List<object> { "E" });
        }

        /// <summary>
        /// 異常設備取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        public static Device GetAbnormalDevice(string id)
        {
            return DeviceQuery(id, OperatorType.NotEqual, new List<object> { "N" });
        }

        /// <summary>
        /// 設備查詢計畫
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Device DeviceQuery(string id, OperatorType type, List<object> subCondition)
        {
            var context = QueryContextFactory.CreateInstance<Device>();
            //對應設備ID且為監控狀態之設備
            var condition = new Device() { DEVICE_ID = id, IS_MONITOR = "Y" };

            context.Main.Query(condition)
                        .Query("DEVICE_STATUS", type, subCondition);

            return context.GetEntity();
        }
    }
}