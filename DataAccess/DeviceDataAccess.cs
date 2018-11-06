using ModelLibrary;
using SourceHelper.Core;
using SourceHelper.Enumerate;
using System.Collections.Generic;

namespace DataAccess
{
    /// <summary>
    /// 設備資料處理
    /// </summary>
    public class DeviceDataAccess
    {
        private QueryContext<Device> _context = QueryContextFactory.CreateInstance<Device>();

        //查詢對象
        private string _target;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="target">查詢對象</param>
        public DeviceDataAccess(string target)
        {
            _target = target;
        }

        /// <summary>
        /// 正常設備取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <param name="">查詢類型</param>
        /// <returns></returns>
        public Device GetNormalDevice(string id)
        {
            return DeviceQuery(id, OperatorType.Equal, new List<object> { "N" });
        }

        /// <summary>
        /// 異常設備取得(不含修復中)
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        public Device GetErrorDevice(string id)
        {
            return DeviceQuery(id, OperatorType.Equal, new List<object> { "E" });
        }

        /// <summary>
        /// 異常設備取得
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        public Device GetAbnormalDevice(string id)
        {
            return DeviceQuery(id, OperatorType.NotEqual, new List<object> { "N" });
        }

        /// <summary>
        /// 設備查詢計畫
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Device DeviceQuery(string id, OperatorType type, List<object> subCondition)
        {
            //對應設備ID且為監控狀態之設備
            var condition = new Device() { DEVICE_ID = id, IS_MONITOR = "Y", DEVICE_TYPE = "N" };

            _context.Main.Query(condition)
                        .Query(_target, type, subCondition);

            return _context.GetEntity();
        }
    }
}