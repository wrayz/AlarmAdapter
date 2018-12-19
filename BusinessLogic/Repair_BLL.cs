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
            //TODO: 待IM修正維修參數並上架後再移除
            if (repair.TARGET_NAME == null)
                repair.TARGET_NAME = GetReapirTarget(repair);

            var data = new Repair
            {
                RECORD_SN = repair.RECORD_SN ?? repair.LOG_SN,
                DEVICE_SN = repair.DEVICE_SN,
                TARGET_NAME = repair.TARGET_NAME,
                USERID = login.USERID
            };

            return (_dao as Repair_DAO).Save(data);
        }

        /// <summary>
        /// 維修監控項目取得
        /// </summary>
        /// <param name="repair">維修資訊</param>
        /// <returns></returns>
        private string GetReapirTarget(Repair repair)
        {
            var bll = GenericBusinessFactory.CreateInstance<Monitor>();
            return (bll as Monitor_BLL).GetRepairTarget(repair);
        }
    }
}