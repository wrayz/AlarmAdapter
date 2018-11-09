using System;

namespace BusinessLogic.RecordParser
{
    /// <summary>
    /// 解析器工廠
    /// </summary>
    public static class ParserFactory
    {
        /// <summary>
        /// 解析器產生
        /// </summary>
        /// <param name="type">解析器類型</param>
        /// <returns></returns>
        public static IParser CreateInstance(string type)
        {
            IParser parser;

            switch (type)
            {
                case "Cacti":
                    parser = new CactiParser();
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { type } 解析器");
            }

            return parser;
        }

        internal static object CreateInstance(object detectorType)
        {
            throw new NotImplementedException();
        }
    }
}