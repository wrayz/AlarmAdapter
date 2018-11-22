using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 維修資訊商業邏輯
    /// </summary>
    public class Repair_BLL : GenericBusinessLogic<Repair>
    {
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="repair">維修資訊</param>
        /// <param name="login">使用者登入</param>
        public void Save(Repair repair, UserLogin login)
        {
            CheckDeviceStatus(repair.DEVICE_SN);

            Modify("Insert", login, repair, null, false, true);
        }

        private void CheckDeviceStatus(string deviceSn)
        {
            var bll = GenericBusinessFactory.CreateInstance<Device>();
            (bll as Device_BLL).CheckStatus(deviceSn);
        }
    }
}