using BusinessLogic.Notification;
using DataAccess;
using ModelLibrary;
using ModelLibrary.Enumerate;
using ModelLibrary.Generic;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    /// <summary>
    /// 簡易設備記錄商業邏輯
    /// </summary>
    public class SimpleLog_BLL : GenericBusinessLogic<SimpleLog>
    {
        public Device Device { get; private set; }
        public AbuseIpDbSetting AbuseIpDbSetting { get; private set; }
        public SimpleLog SimpleLog { get; private set; }
        public string IpAddress { get; private set; }
        public INotification Notification { get; private set; }

        /// <summary>
        /// 資料初始化
        /// </summary>
        /// <param name="log">告警記錄</param>
        public void InitLogmasterData(APILog log)
        {
            SetDevice(log.DEVICE_ID);

            AbuseIpDbSetting = GetAbuseSetting();

            var data = new SimpleLog
            {
                DEVICE_SN = Device.DEVICE_SN,
                ERROR_TIME = log.LOG_TIME,
                ERROR_INFO = log.LOG_INFO,
            };

            SimpleLog = ModifyLog(data, "L");

            IpAddress = GetBlockIP();

            Notification = NotificationFactory.CreateInstance(DeviceType.S);
        }

        /// <summary>
        /// 通知檢查
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool IsNotification()
        {
            var alarm = new Alarm { Time = SimpleLog.ERROR_TIME, Content = SimpleLog.ERROR_INFO };

            return Notification.IsNotification(alarm, Device.NOTIFICATION_CONDITION, Device.NOTIFICATION_RECORDS);
        }

        /// <summary>
        /// 黑名單分數檢查
        /// </summary>
        /// <returns></returns>
        public bool IsBlockHole(BlockHole blockHole)
        {
            return blockHole.ABUSE_SCORE.Value >= AbuseIpDbSetting.ABUSE_SCORE;
        }

        /// <summary>
        /// 檢查黑名單請求週期
        /// </summary>
        /// <param name="blockHole"></param>
        /// <returns></returns>
        public bool CheckBlockCycle(BlockHole blockHole)
        {
            if (blockHole.ABUSE_SCORE == null)
                return true;

            var leadtime = new TimeSpan(SimpleLog.ERROR_TIME.Value.Ticks - blockHole.REQUEST_TIME.Value.Ticks).Hours;
            return leadtime >= AbuseIpDbSetting.CHECK_CYCLE;
        }

        /// <summary>
        /// 設備資料設置
        /// </summary>
        /// <param name="id">設備ID</param>
        /// <returns></returns>
        private void SetDevice(string id)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            var option = new QueryOption { Relation = true, Plan = new QueryPlan { Join = "Records" } };
            var condition = new Device { DEVICE_ID = id, DEVICE_TYPE = "S", IS_MONITOR = "Y" };

            Device = bll.Get(option, new UserLogin(), condition);

            if (string.IsNullOrEmpty(Device.DEVICE_SN))
                throw new HttpRequestException($"{ id } 無對應設備，請檢查 EyesFree 設備設定");
        }

        /// <summary>
        /// 黑名單查詢設定取得
        /// </summary>
        /// <returns></returns>
        private AbuseIpDbSetting GetAbuseSetting()
        {
            var bll = GenericBusinessFactory.CreateInstance<AbuseIpDbSetting>();
            return bll.Get(new QueryOption(), new UserLogin());
        }

        /// <summary>
        /// 記錄新增
        /// </summary>
        /// <param name="log">告警記錄</param>
        public SimpleLog ModifyLog(SimpleLog data, string type)
        {
            return (_dao as SimpleLog_DAO).ModifyLog(data, type);
        }

        /// <summary>
        /// 待查黑名單取得
        /// </summary>
        /// <returns></returns>
        private string GetBlockIP()
        {
            var list = Regex.Split(SimpleLog.ERROR_INFO, "detect block ip ");
            return list[1];
        }
    }
}