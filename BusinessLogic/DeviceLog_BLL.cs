using BusinessLogic.Event;
using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System.Net.Http;

namespace BusinessLogic
{
    /// <summary>
    /// 一般網路設備告警記錄商業邏輯
    /// </summary>
    public class DeviceLog_BLL : GenericBusinessLogic<Log>
    {
        /// <summary>
        /// 異常紀錄儲存
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        public Log SaveErrorLog(Log log)
        {
            //設備編號取得
            log.DEVICE_SN = GetDeviceByID(log);
            //儲存
            _dao.Modify(log.ACTION_TYPE, log);

            log.LOG_SN = GetDeviceLog(log.DEVICE_SN).LOG_SN;

            return log;
        }

        /// <summary>
        /// 恢復記錄儲存
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        public Log SaveRecoveryLog(Log log)
        {
            //設備編號取得
            log.DEVICE_SN = GetDeviceByID(log);
            //儲存
            log.LOG_SN = GetDeviceLog(log.DEVICE_SN).LOG_SN;

            _dao.Modify(log.ACTION_TYPE, log);

            return log;
        }

        /// <summary>
        /// 對應設備編號取得 By 設備ID
        /// </summary>
        /// <param name="log">告警訊息</param>
        /// <returns></returns>
        private string GetDeviceByID(Log log)
        {
            //設備取得
            var deviceSn = CorrespondDeviceFactory.GetDevice(log).DEVICE_SN;

            if (!string.IsNullOrEmpty(deviceSn))
                return deviceSn;
            else
                throw new HttpRequestException("無對應設備，或對應設備狀態不符");
        }

        /// <summary>
        /// 紀錄對應設備資料取得
        /// </summary>
        /// <param name="sn">設備編號</param>
        /// <returns></returns>
        private DeviceLog GetDeviceLog(string sn)
        {
            //設備紀錄資料處理物件
            var dao = GenericDataAccessFactory.CreateInstance<DeviceLog>();
            //查詢條件
            var condition = new DeviceLog { DEVICE_SN = sn };

            return dao.Get(new QueryOption(), condition);
        }
    }
}