﻿using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 事件處理商業邏輯
    /// </summary>
    public class EventBusinessLogic
    {
        /// <summary>
        /// 對應設備取得
        /// </summary>
        /// <param name="log">設備紀錄資料</param>
        /// <returns></returns>
        public Device GetDevice(APILog log)
        {
            //設備取得
            return CorrespondDeviceFactory.GetDevice(log);
        }

        /// <summary>
        /// 紀錄處理
        /// </summary>
        /// <param name="log">設備紀錄資料</param>
        /// <returns></returns>
        public APILog LogModify(APILog log)
        {
            var dao = GenericDataAccessFactory.CreateInstance<APILog>();
            return dao.Modify(log.ACTION_TYPE, log);
        }

        /// <summary>
        /// 紀錄詳細資料取得
        /// </summary>
        /// <param name="log">紀錄編號</param>
        /// <returns></returns>
        public LogDetail GetLogDetail(string log)
        {
            //紀錄詳細資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<LogDetail>();
            //查詢條件
            var condition = new LogDetail { LOG_SN = log };
            //查詢參數
            var option = new QueryOption
            {
                Plan = new QueryPlan { Join = "Detail" },
            };

            return dao.Get(option, condition);
        }

        /// <summary>
        /// 紀錄對應設備資料取得
        /// </summary>
        /// <param name="device">設備編號</param>
        /// <returns></returns>
        public DeviceLog GetDeviceLog(string device)
        {
            //設備紀錄資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<DeviceLog>();
            //查詢條件
            var condition = new DeviceLog { DEVICE_SN = device };

            return dao.Get(new QueryOption(), condition);
        }

        /// <summary>
        /// 訊息推送事件
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        /// <returns></returns>
        public async Task<HttpStatusCode[]> PushEvent(string type, LogDetail log)
        {
            EventType enumType = (EventType)Enum.Parse(typeof(EventType), type);

            var im = new PushIM(enumType, log);
            var slack = new PushSlack(enumType, log);

            return await Task.WhenAll(im.PushMessage(), slack.PushMessage());
        }
    }
}