using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Linq;
using System.Collections.Generic;

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
        /// <param name="logDetail">詳細紀錄</param>
        /// <returns></returns>
        public LogDetail GetLogDetail(LogDetail logDetail)
        {
            //紀錄詳細資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<LogDetail>();
            //查詢參數
            var option = new QueryOption
            {
                Plan = new QueryPlan { Join = "Detail" },
            };

            return dao.Get(option, logDetail);
        }

        /// <summary>
        /// 紀錄對應設備資料取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        public DeviceLog GetDeviceLog(string sn)
        {
            //設備紀錄資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<DeviceLog>();
            //查詢條件
            var condition = new DeviceLog { DEVICE_SN = sn };

            return dao.Get(new QueryOption(), condition);
        }

        /// <summary>
        /// 設備資料取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        public Device GetDevice(string sn)
        {
            var dao = GenericDataAccessFactory.CreateInstance<Device>();
            //查詢條件
            var condition = new Device { DEVICE_SN = sn };
            //設備資料
            return dao.Get(new QueryOption { Relation = true }, condition);
        }

        /// <summary>
        /// 通知記錄取得
        /// </summary>
        /// <param name="record">實體資料</param>
        /// <returns></returns>
        public DeviceNotifyRecord GetRecord(DeviceNotifyRecord record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceNotifyRecord>();
            return dao.Get(new QueryOption(), record);
        }

        /// <summary>
        /// 通知記錄清單取得
        /// </summary>
        /// <param name="record">實體資料</param>
        /// <returns></returns>
        public IEnumerable<DeviceNotifyRecord> GetRecords(DeviceNotifyRecord record)
        {
            var dao = GenericDataAccessFactory.CreateInstance<DeviceNotifyRecord>();
            return dao.GetList(new QueryOption(), record);
        }
    }
}