using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 群組資料
    /// </summary>
    public class Group
    {
        /// <summary>
        /// 群組編號
        /// </summary>
        public string GROUP_SN { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        public string GROUP_NAME { get; set; }

        /// <summary>
        /// 群組設備清單
        /// </summary>
        public List<GroupDevice> DEVICE_LIST { get; set; }

        /// <summary>
        /// 群組成員清單
        /// </summary>
        public List<GroupMember> MEMBER_LIST { get; set; }
    }
}