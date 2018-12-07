using DataAccess;
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
        public Repair Save(Repair repair, UserLogin login)
        {
            var data = new Repair
            {
                RECORD_SN = repair.RECORD_SN == null ? repair.LOG_SN : repair.RECORD_SN,
                DEVICE_SN = repair.DEVICE_SN,
                USERID = login.USERID
            };

            return (_dao as Repair_DAO).Save(data);
        }
    }
}