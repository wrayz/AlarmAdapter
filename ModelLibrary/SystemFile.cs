using System;
using System.Configuration;

namespace ModelLibrary
{
    /// <summary>
    /// 系統檔案
    /// </summary>
    public class SystemFile
    {
        /// <summary>
        /// 附件編號
        /// </summary>
        public string ATTACH_SN { get; set; }

        /// <summary>
        /// 使用表格名稱
        /// </summary>
        public string TB_NAME { get; set; }

        /// <summary>
        /// 使用表格PK值_1
        /// </summary>
        public string TB_KEY_VALUE_1 { get; set; }

        /// <summary>
        /// 使用表格PK值_2
        /// </summary>
        public string TB_KEY_VALUE_2 { get; set; }

        /// <summary>
        /// 使用表格PK值_3
        /// </summary>
        public string TB_KEY_VALUE_3 { get; set; }

        /// <summary>
        /// 正式檔案位址
        /// </summary>
        public string FORMAL_DIR { get; set; }

        /// <summary>
        /// 原檔名
        /// </summary>
        public string ORIGINAL_NAME { get; set; }

        /// <summary>
        /// 副檔名
        /// </summary>
        public string FILE_TYPE { get; set; }

        /// <summary>
        /// 檔案分類
        /// </summary>
        public string FILE_CATEGORY { get; set; }

        /// <summary>
        /// 上傳日期
        /// </summary>
        public DateTime? ATTACH_DATE { get; set; }

        /// <summary>
        /// 是否為歷史檔案
        /// </summary>
        public string IS_HISTORY { get; set; }

        /// <summary>
        /// 檔案上傳名稱
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 檔案HTTP路徑
        /// </summary>
        public string FILE_URL
        {
            get
            {
                string url = ConfigurationManager.AppSettings["host"];
                return $"{ url }{ this.FORMAL_DIR }/{ this.FILE_NAME }.{ this.FILE_TYPE }";
            }
        }

        /// <summary>
        /// 檔案絕對路徑
        /// </summary>
        public string FILE_PATH { get; set; }
    }
}