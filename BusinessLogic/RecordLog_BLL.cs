﻿using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
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
        /// <param name="content">原始資料</param>
        /// <returns></returns>
        public IEnumerable<Record> ParseData(string content)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

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
        /// 是否異常
        /// </summary>
        /// <param name="record">告警訊息</param>
        /// <param name="limit">告警限制</param>
        /// <param name="recordStatus">設備記錄狀態</param>
        /// <returns></returns>
        public bool IsError(Record record, RecordLimit limit, string recordStatus)
        {
            return
                ( ( (record.RECORD_TEMPERATURE > limit.MAX_TEMPERATURE_VAL || record.RECORD_TEMPERATURE < limit.MIN_TEMPERATURE_VAL) ||
                    (record.RECORD_HUMIDITY > limit.MAX_HUMIDITY_VAL || record.RECORD_HUMIDITY < limit.MIN_HUMIDITY_VAL)
                  ) && recordStatus == "N"
                );
        }

        /// <summary>
        /// 是否正常 
        /// </summary>
        /// <param name="record">告警訊息</param>
        /// <param name="limit">告警限制</param>
        /// <param name="recordStatus">設備記錄狀態</param>
        /// <returns></returns>
        public bool IsRecover(Record record, RecordLimit limit, string recordStatus)
        {
            return ( (record.RECORD_TEMPERATURE <= limit.MAX_TEMPERATURE_VAL && record.RECORD_TEMPERATURE >= limit.MIN_TEMPERATURE_VAL) &&
                     (record.RECORD_HUMIDITY <= limit.MAX_HUMIDITY_VAL && record.RECORD_HUMIDITY >= limit.MIN_HUMIDITY_VAL) &&
                     (recordStatus == "E" || recordStatus == "R"));
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
        /// 數據記錄資料新增
        /// </summary>
        /// <param name="record">數據記錄</param>
        public void InsertRecord(Record record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Record>();
            dao.Modify("Insert", record);
        }

        /// <summary>
        /// 數據異常紀錄物件處理
        /// </summary>
        /// <param name="type">處理類型</param>
        /// <param name="data">實體資料</param>
        public void ModifyRecordLog(string type, RecordLog data)
        {
            _dao.Modify(type, data);
        }
    }
}