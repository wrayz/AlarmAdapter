using ModelLibrary.Enumerate;
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
        /// <param name="detector">偵測器</param>
        /// <returns></returns>
        public static IParser CreateInstance(Detector detector)
        {
            IParser parser;

            switch (detector)
            {
                case Detector.Cacti:
                    parser = new CactiParser();
                    break;

                case Detector.Camera:
                    parser = new CameraParser();
                    break;

                case Detector.Logmaster:
                    parser = new LogmasterParser();
                    break;

                default:
                    throw new NotImplementedException($"尚未實作 { detector } 解析器");
            }

            return parser;
        }
    }
}