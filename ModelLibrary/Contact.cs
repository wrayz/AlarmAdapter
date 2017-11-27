namespace ModelLibrary
{
    /// <summary>
    /// 供應商聯絡人資料
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// 供應商編號
        /// </summary>
        public string SUPPLIER_SN { get; set; }

        /// <summary>
        /// 聯絡人編號
        /// </summary>
        public string CONTACT_SN { get; set; }

        /// <summary>
        /// 聯絡人名稱
        /// </summary>
        public string CONTACT_NAME { get; set; }

        /// <summary>
        /// 聯絡人性別
        /// </summary>
        public string CONTACT_SEX { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public Config SEX_INFO { get; set; }

        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 聯絡人email
        /// </summary>
        public string EMAIL { get; set; }

        /// <summary>
        /// 聯絡人電話
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 聯絡人分機
        /// </summary>
        public string EXT { get; set; }

        /// <summary>
        /// 聯絡人手機
        /// </summary>
        public string PHONE { get; set; }

        /// <summary>
        /// 聯絡人傳真
        /// </summary>
        public string FAX { get; set; }
    }
}