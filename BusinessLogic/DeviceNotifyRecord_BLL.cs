using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class DeviceNotifyRecord_BLL : GenericBusinessLogic<DeviceNotifyRecord>
    {
        /// <summary>
        /// 所有通知記錄確認
        /// </summary>
        /// <param name="deviceSn"></param>
        /// <param name="sourceTime"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool CheckAllRecord(string deviceSn, DateTime sourceTime, EventType type, DeviceNotifySetting setting)
        {
            //通知記錄清單取得
            var records = GetRecords(new DeviceNotifyRecord { DEVICE_SN = deviceSn });

            //沒有設備通知記錄
            if ((records as List<DeviceNotifyRecord>).Count == 0) return true;

            //最後通知時間
            var lastTime = records.OrderByDescending(x => x.NOTIFY_TIME).First().NOTIFY_TIME.Value;
            var nextTime = lastTime.AddMinutes((double)setting.MUTE_INTERVAL);

            return sourceTime > nextTime;
        }

        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="record">實體資料</param>
        /// <returns></returns>
        private DeviceNotifyRecord GetRecord(DeviceNotifyRecord record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceNotifyRecord>();
            return dao.Get(new QueryOption(), record);
        }

        /// <summary>
        /// 通知記錄清單取得
        /// </summary>
        /// <param name="record">實體資料</param>
        /// <returns></returns>
        private IEnumerable<DeviceNotifyRecord> GetRecords(DeviceNotifyRecord record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceNotifyRecord>();
            return dao.GetList(new QueryOption(), record);
        }

        /// <summary>
        /// 通知記錄儲存
        /// </summary>
        /// <param name="data">設備通知記錄</param>
        /// <returns></returns>
        public DeviceNotifyRecord SaveNotifyRecord(DeviceNotifyRecord data)
        {
            return new DeviceNotifyRecord();
            //if (_dao.GetCount(new QueryOption(), new DeviceNotifyRecord { DEVICE_SN = data.DEVICE_SN, ERROR_INFO = data.ERROR_INFO }) > 0)
            //{
            //    return _dao.Modify("Update", data);
            //}

            //return _dao.Modify("Insert", data);
        }
    }
}
