using BusinessLogic.RecordAlarm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLogicTests
{
    [TestClass]
    public class AlarmTests
    {
        private static List<AlarmCondition> _alarmConditions;

        [ClassInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            _alarmConditions = new List<AlarmCondition>
            {
                new AlarmCondition
                {
                    DEVICE_SN = "2018001",
                    TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                    TARGET_VALUE = "ALERT",
                    IS_EXCEPTION = true
                }
            };
        }

        [TestMethod]
        public void Cacti_ALERT訊息_為異常()
        {
            //Arrange
            var expected = true;

            var deviceMonitor = new DeviceMonitor
            {
                DEVICE_SN = "2018001",
                DEVICE_ID = "192.168.10.99",
                TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                TARGET_VALUE = "ALERT",
                TARGET_CONTENT = "current value is 5630.6207",
                RECEIVE_TIME = DateTime.Parse("2018/11/06 18:08:34", CultureInfo.InvariantCulture)
            };

            var type = "Cacti";
            var alarm = AlarmFactory.CreateInstance(type);

            //Act
            var actual = alarm.IsException(deviceMonitor, _alarmConditions);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}