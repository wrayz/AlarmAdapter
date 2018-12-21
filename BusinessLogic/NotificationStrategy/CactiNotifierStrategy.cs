using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic.NotificationStrategy
{
    public class CactiNotifierStrategy : NotifierStrategy
    {
        public override string IsNotification(NotificationCondition condition, Monitor monitor, Monitor previousMonitor, Notification notification)
        {
            var mathSetting = GetConfigParams();
            if (monitor.TARGET_NAME != "Ping" && mathSetting.PARAMETER_VALUE == "N")
                return "N";

            var result = CheckStatusNotification(condition, monitor, previousMonitor) &&
                         CheckNotificationInterval(condition, monitor, notification);

            return result ? "Y" : "N";
        }

        /// <summary>
        /// 數學所參數設定取得
        /// </summary>
        /// <returns></returns>
        private ConfigParams GetConfigParams()
        {
            var bll = GenericBusinessFactory.CreateInstance<ConfigParams>();
            var condition = new ConfigParams
            {
                SYSTEM_NAME = "MATH_SETTING",
                PARAMETER_NAME = "PORT_NOTIFICATION"
            };

            return bll.Get(new QueryOption(), new UserLogin(), condition);
        }
    }
}