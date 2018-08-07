using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    public class DeviceNotifyRecord_BLL : GenericBusinessLogic<DeviceNotifyRecord>
    {
        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="data">設備通知記錄</param>
        /// <returns></returns>
        public DeviceNotifyRecord SaveNotifyRecord(DeviceNotifyRecord data)
        {
            if (_dao.GetCount(new QueryOption(), new DeviceNotifyRecord { DEVICE_SN = data.DEVICE_SN, ERROR_INFO = data.ERROR_INFO }) > 0)
            {
                return _dao.Modify("Update", data);
            }

            return _dao.Modify("Insert", data);
        }
    }
}
