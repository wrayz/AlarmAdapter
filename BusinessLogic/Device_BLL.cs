using ModelLibrary;
using ModelLibrary.Generic;
using System;

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
                DEVICE_TYPE = deviceType,
                IS_MONITOR = "Y"
            };

            var device = _dao.Get(new QueryOption(), condition);

            if (device.DEVICE_SN == null)
                throw new Exception($"檢查 { deviceId } 是否已建立設備資訊或是否開啟監控");

            return device;
        }

        /// <summary>
        /// 設備資訊取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        public Device GetDevice(string deviceSn)
        {
            var option = new QueryOption
            {
                Plan = new QueryPlan { Join = "Groups" }
            };
            var condition = new Device { DEVICE_SN = deviceSn };

            return _dao.Get(option, condition);
        }
    }
}