using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 設備商業邏輯
    /// </summary>
    public class Device_BLL : GenericBusinessLogic<Device>
    {
        /// <summary>
        /// 設備資訊取得
        /// </summary>
        /// <param name="deviceId">設備識別碼</param>
        /// <param name="deviceType">設備類型</param>
        /// <returns></returns>
        public Device GetDevice(string deviceId, string deviceType)
        {
            var condition = new Device
            {
                DEVICE_ID = deviceId,
                DEVICE_TYPE = deviceType
            };

            var query = new QueryOption
            {
                Plan = new QueryPlan
                {
                    Join = "Conditions"
                }
            };

            return _dao.Get(query, condition);
        }
    }
}