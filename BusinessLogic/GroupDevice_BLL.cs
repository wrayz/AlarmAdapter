using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// 設備群組商業邏輯
    /// </summary>
    public class GroupDevice_BLL : GenericBusinessLogic<GroupDevice>
    {
        /// <summary>
        /// 群組清單取得
        /// </summary>
        /// <param name="deviceSn">設備編號</param>
        /// <returns></returns>
        internal List<GroupDevice> GetGroups(string deviceSn)
        {
            var condition = new GroupDevice
            {
                DEVICE_SN = deviceSn
            };

            return _dao.GetList(new QueryOption(), condition).ToList();
        }
    }
}