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
        /// <param name="parserType">解析器類型</param>
        /// <returns></returns>
        public static IParser CreateInstance(string parserType)
        {
            IParser parser;

            switch (parserType)
            {
                case "CactiSnmp":
                    parser = new CactiSnmpParser();
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { parserType } 解析器");
            }

            return parser;
        }
    }
}