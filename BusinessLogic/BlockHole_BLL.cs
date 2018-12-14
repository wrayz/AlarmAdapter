using ModelLibrary;
using ModelLibrary.Generic;

namespace BusinessLogic
{
    /// <summary>
    /// 黑名單商業邏輯
    /// </summary>
    public class BlockHole_BLL : GenericBusinessLogic<BlockHole>
    {
        /// <summary>
        /// 黑名單資訊取得
        /// </summary>
        /// <param name="ip">黑名單 IP</param>
        /// <returns></returns>
        public BlockHole GetBlockHole(string ip)
        {
            var condition = new BlockHole { IP_ADDRESS = ip };
            return _dao.Get(new QueryOption(), condition);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="ipAddress">IP</param>
        /// <param name="score">黑名單分數</param>
        /// <returns></returns>
        public BlockHole Save(string ipAddress, int? score)
        {
            if (score == null)
                return new BlockHole { ABUSE_SCORE = 100 };

            var data = new BlockHole
            {
                IP_ADDRESS = ipAddress,
                ABUSE_SCORE = score
            };

            return _dao.Modify("Save", data);
        }
    }
}