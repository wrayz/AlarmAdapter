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
        private static Target _target;

        [ClassInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            _target = new Target
            {
                DEVICE_SN = "2018001",
                TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                TARGET_STATUS = "0",
                OPERATOR_TYPE = "Equal",
                ALARM_CONDITIONS = new List<AlarmCondition>
                {
                    new AlarmCondition
                    {
                        DEVICE_SN = "2018001",
                        TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                        TARGET_VALUE = "ALERT",
                    }
                }
            };
        }

        [TestMethod]
        public void Cacti_ALERT訊息_為異常()
        {
            //Arrange
            var expected = "Y";

            var monitor = new Monitor
            {
                DEVICE_SN = "2018001",
                DEVICE_ID = "192.168.10.99",
                TARGET_NAME = "Traffic - Gi1/0/20 [traffic_in]",
                TARGET_VALUE = "ALERT",
                TARGET_MESSAGE = "current value is 5630.6207",
                RECEIVE_TIME = DateTime.Parse("2018/11/06 18:08:34", CultureInfo.InvariantCulture)
            };

            var alarm = new Alarmer();

            //Act
            var actual = alarm.IsException(monitor, _target);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}