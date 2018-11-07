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
        /// <param name="parserName">解析器名稱</param>
        /// <returns></returns>
        public static IParser CreateInstance(string parserName)
        {
            IParser parser;

            switch (parserName)
            {
                case "Cacti":
                    parser = new CactiParser();
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { parserName } 解析器");
            }

            return parser;
        }
    }
}