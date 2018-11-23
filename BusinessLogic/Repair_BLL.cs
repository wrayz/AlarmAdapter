using DataAccess;
using ModelLibrary;
using ModelLibrary.Generic;
using System;

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
            repair.USERID = login.USERID;

            return (_dao as Repair_DAO).Save(repair);
        }
    }
}