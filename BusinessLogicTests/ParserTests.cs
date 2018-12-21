using BusinessLogic.RecordParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLogicTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void 解析_CactiSnmp_ALERT訊息()
        {
            //Arrange
            var parser = ParserFactory.CreateInstance(Detector.Cacti);

            var raw = "{\"id\":\"192.168.10.99\", \"target\": \"Traffic - Gi1/0/20 [traffic_in]\", \"action\":\"ALERT\", \"info\":\"current value is 5630.6207\",\"time\":\"2018/11/06 18:08:34\"}";

            var expected = new List<Monitor>
            {
                new Monitor
                {
                    DEVICE_ID = "192.168.10.99",
                    TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                    TARGET_VALUE = "ALERT",
                    TARGET_MESSAGE = "current value is 5630.6207",
                    RECEIVE_TIME = DateTime.Parse("2018/11/06 18:08:34", CultureInfo.InvariantCulture)
                }
            };

            //Act
            var actual = parser.ParseRecord(raw);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}