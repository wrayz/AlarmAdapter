using DataExpansion;
using System.Collections.Generic;

namespace ModelLibrary
{
    /// <summary>
    /// 供應商資料
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 廠商編號
        /// </summary>
        [Key]
        public string SUPPLIER_SN { get; set; }

        /// <summary>
        /// 廠商名稱
        /// </summary>
        public string SUPPLIER_NAME { get; set; }

        /// <summary>
        /// 廠商電話
        /// </summary>
        public string SUPPLIER_TEL { get; set; }

        /// <summary>
        /// 廠商信箱
        /// </summary>
        public string SUPPLIER_EMAIL { get; set; }

        /// <summary>
        /// 啟用狀態
        /// </summary>
        [Status]
        public string STS_INFO { get; set; }

        /// <summary>
        /// 連絡人清單
        /// </summary>
        public List<Contact> CONTACT_LIST { get; set; }
    }
}