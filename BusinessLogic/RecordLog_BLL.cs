using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLogic
{
    /// <summary>
    /// 數據設備記錄商業邏輯
    /// </summary>
    public class RecordLog_BLL : GenericBusinessLogic<RecordLog>
    {
        /// <summary>
        /// 資料轉換
        /// </summary>
        /// <param name="dict">原始資料</param>
        /// <returns></returns>
        public IEnumerable<Record> DataConvert(Dictionary<string, string> dict)
        {
            //監控欄位數
            const int COLUMN_NUM = 2;
            //日期、時間所占欄位數
            const int DATETIME_NUM = 2;
            //資料數
            var dataCount = (dict.Count - DATETIME_NUM) / (COLUMN_NUM + 1);

            var data = new List<Record>();

            //紀錄時間
            DateTime recordTime = new DateTime();

            if (dict.TryGetValue("Date", out string date) && dict.TryGetValue("Time", out string time))
            {
                recordTime = DateTime.ParseExact(string.Format("{0} {1}", date, time), "MM/dd/yy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                throw new Exception("時間格式錯誤");
            }

            //紀錄資訊
            for (var i = 1; i <= dataCount; i++)
            {
                var item = new Record();

                var value = "";

                // ID
                if (dict.TryGetValue(string.Format("Name_{0}", i.ToString()), out value))
                    item.DEVICE_ID = value;
                // 溫度
                if (dict.TryGetValue(string.Format("Temperature_{0}", i.ToString()), out value))
                    item.RECORD_TEMPERATURE = decimal.Parse(value) / 100;
                // 濕度
                if (dict.TryGetValue(string.Format("Humidity_{0}", i.ToString()), out value))
                    item.RECORD_HUMIDITY = decimal.Parse(value) / 100;
                //時間
                item.RECORD_TIME = recordTime;

                data.Add(item);
            }

            return data;
        }

        /// <summary>
        /// 監控參數取得
        /// </summary>
        /// <returns></returns>
        public RecordLimit GetLimit()
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLimit>();
            return dao.Get(new QueryOption());
        }

        /// <summary>
        /// 設備資料取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        public Device GetDeviceBySn(string sn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Device>();
            var data = new Device
            {
                DEVICE_SN = sn,
                DEVICE_TYPE = "D",
                IS_MONITOR = "Y",
                RECORD_STATUS = "E"
            };
            return dao.Get(new QueryOption(), data);
        }

        /// <summary>
        /// 設備資料取得
        /// </summary>
        /// <param name="record">數據記錄資料</param>
        /// <returns></returns>
        public Device GetDeviceById(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Device>();
            var data = new Device
            {
                DEVICE_ID = record.DEVICE_ID,
                DEVICE_TYPE = "D",
                IS_MONITOR = "Y"
            };
            return dao.Get(new QueryOption(), data);
        }

        /// <summary>
        /// 數據記錄資料取得
        /// </summary>
        /// <param name="sn">記錄編號</param>
        /// <returns></returns>
        public RecordLog GetRecordLog(int? sn)
        {
            //查詢條件
            var option = new QueryOption
            {
                Plan = new QueryPlan { Join = "Payload" }
            };

            //資料
            var recordLog = new RecordLog { LOG_SN = sn };

            return _dao.Get(option, recordLog);
        }

        /// <summary>
        /// 數據設備記錄資料取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        public DeviceRecord GetDeviceRecord(string sn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceRecord>();
            return dao.Get(new QueryOption(), new DeviceRecord { DEVICE_SN = sn });
        }

        /// <summary>
        /// 數據記錄資料新增
        /// </summary>
        /// <param name="record">數據記錄</param>
        public void AddRecord(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Record>();
            dao.Modify("Insert", record);
        }

        /// <summary>
        /// 數據異常紀錄物件處理
        /// </summary>
        /// <param name="type"></param>
        /// <param name="recordLog"></param>
        public void ModifyRecordLog(string type, RecordLog recordLog)
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLog>();
            dao.Modify(type, recordLog);
        }
    }
}