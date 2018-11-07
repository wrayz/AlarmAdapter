using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.RecordParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void 解析_CactiSnmp_ALERT訊息()
        {
            //Arrange
            var parseType = "CactiSnmp";
            var parser = ParserFactory.CreateInstance(parseType);

            var raw = "{\"id\":\"192.168.10.99 - Traffic - Gi1/0/20 [traffic_in]\", \"action\":\"ALERT\", \"info\":\"192.168.10.99 - Traffic - Gi1/0/20 [traffic_in] ; current value is 5630.6207\",\"time\":\"2018/11/06 18:08:34\"}";

            var expected = new List<DeviceMonitor>
            {
                new DeviceMonitor
                {
                    DEVICE_SN = "2018001",
                    TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                    TARGET_CONTENT = "current value is 5630.6207",
                }
            };

            //Act
            var actual = parser.ParseRecord(raw);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}