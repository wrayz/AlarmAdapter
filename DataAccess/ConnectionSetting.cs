using System;

namespace DataAccess
{
    /// <summary>
    /// 存取連線設定
    /// </summary>
    public static class ConnectionSetting
    {
        /// <summary>
        /// 預設連線字串取得
        /// </summary>
        /// <param name="type">預設連線類型</param>
        /// <returns></returns>
        public static string GetConnection(ConnectionType type)
        {
            string name;

            switch (type)
            {
                //預設連線
                case ConnectionType.Default:
                    name = "connectionString";
                    break;
                //未設定對象
                default:
                    throw new Exception("預設連線類型指定錯誤");
            }

            return name;
        }
    }
}