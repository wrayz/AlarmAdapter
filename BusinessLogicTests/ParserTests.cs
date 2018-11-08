using BusinessLogic.RecordParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void 解析_CactiSnmp_ALERT訊息()
        {
            //Arrange
            var type = "Cacti";
            var parser = ParserFactory.CreateInstance(type);

            var raw = "{\"id\":\"192.168.10.99\", \"target\": \"Traffic - Gi1/0/20 [traffic_in]\", \"action\":\"ALERT\", \"info\":\"current value is 5630.6207\",\"time\":\"2018/11/06 18:08:34\"}";

            var expected = new List<DeviceMonitor>
            {
                new DeviceMonitor
                {
                    DEVICE_ID = "192.168.10.99",
                    TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                    TARGET_VALUE = "ALERT",
                    TARGET_CONTENT = "current value is 5630.6207",
                    RECEIVE_TIME = "2018/11/06 18:08:34"
                }
            };

            //Act
            var actual = parser.ParseRecord(raw);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}