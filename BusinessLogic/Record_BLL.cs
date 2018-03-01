using BusinessLogic.Event;
using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// 數據紀錄資料商業邏輯
    /// </summary>
    public class Record_BLL
    {
        private IDataAccess<Record> _dao = GenericDataAccessFactory.CreateInstance<Record>();

        /// <summary>
        /// IM 伺服器位址
        /// </summary>
        private readonly string _url = ConfigurationManager.AppSettings["im"];

        /// <summary>
        /// 系統名稱
        /// </summary>
        private readonly string _system = "EyesFree";

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

                    if (((record.RECORD_TEMPERATURE > limit.MAX_TEMPERATURE_VAL || record.RECORD_TEMPERATURE < limit.MIN_TEMPERATURE_VAL) ||
                        (record.RECORD_HUMIDITY > limit.MAX_HUMIDITY_VAL || record.RECORD_HUMIDITY < limit.MIN_HUMIDITY_VAL)) &&
                         device.RECORD_STATUS == "N")
                        AbnormalRecord(record);
                    else if ((record.RECORD_TEMPERATURE <= limit.MAX_TEMPERATURE_VAL && record.RECORD_TEMPERATURE >= limit.MIN_TEMPERATURE_VAL) &&
                             (record.RECORD_HUMIDITY <= limit.MAX_HUMIDITY_VAL && record.RECORD_HUMIDITY >= limit.MIN_HUMIDITY_VAL) &&
                             (device.RECORD_STATUS == "E" || device.RECORD_STATUS == "R"))
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
        private void AbnormalRecord(Record record)
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

            dao.Modify("Abnormal", recordLog);

            var logDao = GenericDataAccessFactory.CreateInstance<DeviceRecord>();
            var log = logDao.Get(new QueryOption(), new DeviceRecord { DEVICE_SN = recordLog.DEVICE_SN });
            recordLog = dao.Get(new QueryOption { Plan = new QueryPlan { Join = "Payload" } }, new RecordLog { LOG_SN = log.LOG_SN });

            var fields = new List<Field>
            {
                new Field("主機名稱", recordLog.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", recordLog.DEVICE_INFO.DEVICE_ID, true),
                new Field("異常資訊", string.Format("溫度: {0}, 濕度: {1} ", recordLog.RECORD_TEMPERATURE, record.RECORD_HUMIDITY), false),
                new Field("異常時間", recordLog.RECORD_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true)
            };

            var payload = new Payload
            {
                LOG_SN = recordLog.LOG_SN,
                LOG_TYPE = "D",
                DEVICE_SN = recordLog.DEVICE_SN,
                SYSTEM_NAME = _system,
                BUTTON_STATUS = "E",
                COLOR = "danger",
                TITLE = "溫溼度數據異常資訊",
                GROUP_LIST = recordLog.GROUP_LIST,
                FIELD_LIST = fields
            };

            //推送訊息
            PushMessage(payload);
        }

        /// <summary>
        /// 恢復紀錄處理
        /// </summary>
        /// <param name="record"></param>
        private void RecoverRecord(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<RecordLog>();

            //恢復紀錄資料
            var recordLog = new RecordLog
            {
                DEVICE_SN = record.DEVICE_SN,
                RECOVER_TIME = record.RECORD_TIME
            };

            var logDao = GenericDataAccessFactory.CreateInstance<DeviceRecord>();
            var log = logDao.Get(new QueryOption(), new DeviceRecord { DEVICE_SN = recordLog.DEVICE_SN });

            dao.Modify("Recover", recordLog);

            recordLog = dao.Get(new QueryOption { Plan = new QueryPlan { Join = "Payload" } }, new RecordLog { LOG_SN = log.LOG_SN });

            var fields = new List<Field>
            {
                new Field("主機名稱", recordLog.DEVICE_INFO.DEVICE_NAME, true),
                new Field("設備位址", recordLog.DEVICE_INFO.DEVICE_ID, true),
                new Field("恢復時間", recordLog.RECOVER_TIME.Value.ToString(@"MM\/dd\/yyyy HH:mm"), true),
                new Field("處理人員", recordLog.USER_INFO.USER_NAME, true)
            };

            var payload = new Payload
            {
                LOG_SN = recordLog.LOG_SN,
                LOG_TYPE = "D",
                DEVICE_SN = recordLog.DEVICE_SN,
                SYSTEM_NAME = _system,
                BUTTON_STATUS = "N",
                COLOR = "good",
                TITLE = "溫溼度數據恢復資訊",
                GROUP_LIST = recordLog.GROUP_LIST,
                FIELD_LIST = fields
            };

            //推送訊息
            PushMessage(payload);
        }

        /// <summary>
        /// 訊息推送
        /// </summary>
        /// <returns></returns>
        private async Task<HttpStatusCode> PushMessage(Payload payload)
        {
            //http POST推送設定
            using (var client = new HttpClient())
            {
                //伺服器位址
                client.BaseAddress = new Uri(_url);

                //內容
                var content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("info", JsonConvert.SerializeObject(payload))
                });

                //post
                var result = await client.PostAsync("im/eyesFreeLog", content);

                return result.StatusCode;
            }
        }
    }
}