using DataAccess.Log;
using ModelLibrary;
using System;

namespace BusinessLogic.Event
{
    /// <summary>
    /// 對應設備取得工廠
    /// </summary>
    public static class CorrespondDeviceFactory
    {
        /// <summary>
        /// 對應設備取得
        /// </summary>
        /// <param name="log">設備紀錄</param>
        /// <returns></returns>
        public static Device GetDevice(Log log)
        {
            //查詢ID
            string id = log.DEVICE_ID;
            //動作類型
            EventType type = (EventType)Enum.Parse(typeof(EventType), log.ACTION_TYPE);

            switch (type)
            {
                //異常
                case EventType.Error:
                    //正常設備取得
                    return LogDataAccess.GetNormalDevice(id);
                //修復
                case EventType.Repair:
                    //異常設備取得(不包含正在修復)
                    return LogDataAccess.GetErrorDevice(id);
                //恢復
                case EventType.Recover:
                    //異常設備取得
                    return LogDataAccess.GetAbnormalDevice(id);
                //default
                default:
                    throw new Exception("錯誤的動作類別");
            }
        }
    }
}