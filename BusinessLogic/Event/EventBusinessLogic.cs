using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
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
        /// 對應設備編號取得 By 設備ID
        /// </summary>
        /// <param name="log">設備紀錄資料</param>
        /// <returns></returns>
        public string GetDeviceByID(Log log)
        {
            //設備取得
            return CorrespondDeviceFactory.GetDevice(log).DEVICE_SN;
        }

        /// <summary>
        /// 對應設備編號取得 By 紀錄SN
        /// </summary>
        /// <param name="sn">設備紀錄編號</param>
        /// <returns></returns>
        public string GetDeviceByLog(int sn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceLog>();
            return dao.Get(new QueryOption(), new DeviceLog { LOG_SN = sn }).DEVICE_SN;
        }

        /// <summary>
        /// 紀錄處理
        /// </summary>
        /// <param name="log">設備紀錄資料</param>
        /// <returns></returns>
        public Log LogModify(Log log)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Log>();
            return dao.Modify(log.ACTION_TYPE, log);
        }

        /// <summary>
        /// 紀錄詳細資料取得
        /// </summary>
        /// <param name="log">紀錄編號</param>
        /// <returns></returns>
        public LogDetail GetLogDetail(int log)
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
        public async Task<bool> PushEvent(string type, LogDetail log)
        {
            EventType enumType = (EventType)Enum.Parse(typeof(EventType), type);

            return await PushIM(enumType, log) == HttpStatusCode.OK;
        }

        /// <summary>
        /// IM訊息推送事件
        /// </summary>
        /// <param name="type">動作類型</param>
        /// <param name="log">設備紀錄詳細資料</param>
        /// <returns></returns>

        private async Task<HttpStatusCode> PushIM(EventType type, LogDetail log)
        {
            var im = new PushIM(type, log);

            //訊息推送
            return await im.PushMessage();
        }
    }
}