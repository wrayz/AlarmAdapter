using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// 簡易設備記錄商業邏輯
    /// </summary>
    public class SimpleLog_BLL
    {
        private IDataAccess<SimpleLog> _dao = GenericDataAccessFactory.CreateInstance<SimpleLog>();

        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _url = ConfigurationManager.AppSettings["im"];

        /// <summary>
        /// 記錄新增
        /// </summary>
        /// <param name="log">簡易設備異常記錄</param>
        /// <param name="type"></param>
        public SimpleLog ModifyLog(SimpleLog log, string type)
        {
            return (_dao as SimpleLog_DAO).ModifyLog(log, type);
        }
        
        /// <summary>
        /// 確認全部訊息通知間隔時間
        /// </summary>
        /// <param name="log">簡易設備異常記錄</param>
        /// <param name="setting">通知設定</param>
        /// <returns></returns>
        public bool CheckAllMessageInterval(APILog log, DeviceNotifySetting setting)
        {
            //通知記錄清單取得
            var records = GetRecords(new DeviceNotifyRecord { DEVICE_SN = log.DEVICE_SN });

            //沒有設備通知記錄
            if ((records as List<DeviceNotifyRecord>).Count == 0) return true;

            //最後通知時間
            var lastTime = records.OrderByDescending(x => x.NOTIFY_TIME).First().NOTIFY_TIME.Value;
            var nextTime = lastTime.AddMinutes((double)setting.MUTE_INTERVAL);

            return log.LOG_TIME > nextTime;
        }

        /// <summary>
        /// 確認相同異常訊息通知間隔時間
        /// </summary>
        /// <param name="log">簡易設備異常記錄</param>
        /// <param name="setting">通知設定</param>
        /// <returns></returns>
        public bool CheckSameMessageInterval(APILog log, DeviceNotifySetting setting)
        {
            //相同訊息通知記錄取得
            var record = GetRecord(new DeviceNotifyRecord { DEVICE_SN = log.DEVICE_SN, RECORD_ID = log.LOG_SN });

            if (record.NOTIFY_TIME == null) return true;

            //最後通知時間
            var lastTime = record.NOTIFY_TIME.Value;
            var nextTime = lastTime.AddMinutes((double)setting.MUTE_INTERVAL);

            return log.LOG_TIME > nextTime;
        }

        /// <summary>
        /// 簡易設備記錄取得
        /// </summary>
        /// <param name="simpleLog">實體資料</param>
        /// <returns></returns>
        public SimpleLog GetSimpleLog(SimpleLog simpleLog)
        {
            var option = new QueryOption { Plan = new QueryPlan { Join = "Payload" } };
            return _dao.Get(option, simpleLog);
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
    }
}