using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// 數據紀錄資料商業邏輯
    /// </summary>
    public class Record_BLL
    {
        private IDataAccess<Record> _dao = GenericDataAccessFactory.CreateInstance<Record>();

        /// <summary>
        /// 記錄資料
        /// </summary>
        /// <param name="dict">原始資料</param>
        /// <returns></returns>
        public void ModifyRecords(Dictionary<string, string> dict)
        {
            //資料轉換
            var data = DataConvert(dict);
            //紀錄處理
            ProcessRecord(data);
        }

        /// <summary>
        /// 資料轉換
        /// </summary>
        /// <param name="dict">原始資料</param>
        /// <returns></returns>
        private IEnumerable<Record> DataConvert(Dictionary<string, string> dict)
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
                recordTime = DateTime.Parse(string.Format("{0} {1}", date, time));
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
        /// 紀錄處理
        /// </summary>
        /// <param name="list">紀錄清單</param>
        private void ProcessRecord(IEnumerable<Record> list)
        {
            //監控參數
            var limit = GetLimit();
            var dao = GenericDataAccessFactory.CreateInstance<Device>();
            var option = new QueryOption();

            foreach (var record in list)
            {
                //設備取得
                var device = dao.Get(option, new Device { DEVICE_ID = record.DEVICE_ID, DEVICE_TYPE = "D", IS_MONITOR = "Y" });

                if (!string.IsNullOrEmpty(device.DEVICE_SN))
                {
                    record.DEVICE_SN = device.DEVICE_SN;
                    _dao.Modify("Insert", record);

                    if ((record.RECORD_TEMPERATURE >= limit.TEMPERATURE || record.RECORD_HUMIDITY >= limit.HUMIDITY) && device.RECORD_STATUS == "N")
                        AbnormalRecord(record);
                    else if ((record.RECORD_TEMPERATURE < limit.TEMPERATURE && record.RECORD_HUMIDITY < limit.HUMIDITY) && (device.RECORD_STATUS == "E" || device.RECORD_STATUS == "R"))
                        RecoverRecord(record);
                }
            }
        }

        /// <summary>
        /// 監控參數取得
        /// </summary>
        /// <returns></returns>
        private RecordLimit GetLimit()
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLimit>();
            return dao.Get(new QueryOption());
        }

        /// <summary>
        /// 異常紀錄處理
        /// </summary>
        /// <param name="record"></param>
        private RecordLog AbnormalRecord(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLog>();

            //異常紀錄資料
            var recordLog = new RecordLog
            {
                DEVICE_SN = record.DEVICE_SN,
                RECORD_TIME = record.RECORD_TIME,
                RECORD_TEMPERATURE = record.RECORD_TEMPERATURE,
                RECORD_HUMIDITY = record.RECORD_HUMIDITY
            };

            return dao.Modify("Abnormal", recordLog);
        }

        /// <summary>
        /// 恢復紀錄處理
        /// </summary>
        /// <param name="record"></param>
        private RecordLog RecoverRecord(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLog>();

            //恢復紀錄資料
            var recordLog = new RecordLog
            {
                DEVICE_SN = record.DEVICE_SN,
                RECOVER_TIME = record.RECORD_TIME
            };
            
            return dao.Modify("Recover", recordLog);
        }
    }
}