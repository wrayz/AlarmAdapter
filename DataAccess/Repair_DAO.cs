using ModelLibrary;
using System;

namespace DataAccess
{
    /// <summary>
    /// 維修資料存取
    /// </summary>
    public class Repair_DAO : GenericDataAccess<Repair>
    {
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="data">實體資料</param>
        /// <returns></returns>
        public Repair Save(Repair data)
        {
            var context = ModifyContextFactory.CreateInstance<Repair>();
            var output = context.Modify("Insert", data);

            if (output.Item2.ContainsValue(null))
                throw new Exception($"設備 {data.DEVICE_SN } 並非異常狀態");

            var result = output.Item1;
            result.REGISTER_TIME = DateTime.Parse(output.Item2["REGISTER_TIME"]);

            return result;
        }
    }
}