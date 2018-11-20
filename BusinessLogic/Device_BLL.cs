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

            var query = new QueryOption
            {
                Plan = new QueryPlan
                {
                    Join = "Conditions"
                }
            };

            var device = _dao.Get(query, condition);

            if (device == null)
                throw new Exception($"檢查 { deviceId } 是否已建立設備資訊或是否開啟監控");

            return device;
        }

        /// <summary>
        /// 設備名稱和識別碼取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        public Device GetNameAndId(string deviceSn)
        {
            var option = new QueryOption
            {
                Plan = new QueryPlan
                {
                    Select = "Info"
                }
            };

            var condition = new Device
            {
                DEVICE_SN = deviceSn
            };

            return _dao.Get(option, condition);
        }

        /// <summary>
        /// 檢查狀態
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        internal void CheckStatus(string deviceSn)
        {
            var condition = new Device
            {
                DEVICE_SN = deviceSn,
                DEVICE_STATUS = "E",
            };

            var count = _dao.GetCount(new QueryOption(), condition);

            if (count == 0)
                throw new Exception($"設備編號 { deviceSn } 並非異常狀態");
        }

        /// <summary>
        /// 更新設備狀態為維修狀態
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        internal void UpdateRepairStatus(string deviceSn)
        {
            var data = new Device
            {
                DEVICE_SN = deviceSn,
                DEVICE_STATUS = "R"
            };

            _dao.Modify("Update", data);
        }
    }
}