using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// 設備群組商業邏輯
    /// </summary>
    public class DeviceGroup_BLL : GenericBusinessLogic<DeviceGroup>
    {
        /// <summary>
        /// 群組清單取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        internal List<DeviceGroup> GetGroups(string deviceSn)
        {
            var condition = new DeviceGroup
            {
                DEVICE_SN = deviceSn
            };

            return _dao.GetList(new QueryOption(), condition).ToList();
        }
    }
}