﻿using BusinessLogic.Director;
using BusinessLogic.NotificationStrategy;
using BusinessLogicTests.Fake;
using ModelLibrary;
using ModelLibrary.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BusinessLogicTests.Steps
{
    [Binding]
    public class IntegrateTestFeatureSteps
    {
        private List<Device> _devices;
        private List<AlarmCondition> _alarmConditions;
        private WorkDirector _workDirector;
        private List<Monitor> _previousMonitors;
        private List<NotificationCondition> _notificationConditions;
        private List<Notification> _notifications;
        private List<Target> _targets;
        private NotificationDirector _notificationDirector;

        [Given(@"設備清單為")]
        public void Given設備清單為(Table table)
        {
            _devices = table.CreateSet<Device>().ToList();
        }

        [Given(@"監控項目資訊")]
        public void Given監控項目資訊(Table table)
        {
            _targets = table.CreateSet<Target>().ToList();
        }

        [Given(@"告警條件為")]
        public void Given告警條件為(Table table)
        {
            _alarmConditions = table.CreateSet<AlarmCondition>().ToList();
        }

        [Given(@"前次監控訊息為")]
        public void Given前次監控訊息為(Table table)
        {
            _previousMonitors = table.CreateSet<Monitor>().ToList();
        }

        [Given(@"通知條件為")]
        public void Given通知條件為(Table table)
        {
            _notificationConditions = table.Rows.Select(x =>
            {
                var condition = new NotificationCondition
                {
                    DEVICE_SN = x[0],
                    NOTIFICATION_TYPE = x[1],
                    INTERVAL_LEVEL = x[2],
                    INTERVAL_TIME = Convert.ToInt32(x[3])
                };

                return condition;
            }).ToList();
        }

        [Given(@"通知記錄為")]
        public void Given通知記錄為(Table table)
        {
            _notifications = table.CreateSet<Notification>().ToList();
        }

        [Given(@"偵測器""(.*)""")]
        public void Given偵測器(string type)
        {
            var detector = Enum.Parse(typeof(Detector), type);
            ScenarioContext.Current.Set(detector, "detector");
        }

        [Given(@"設備類型為""(.*)""")]
        public void Given設備類型為(string deviceType)
        {
            ScenarioContext.Current.Set((DeviceType)Enum.Parse(typeof(DeviceType), deviceType), "deviceType");
        }

        [Given(@"原始訊息為""(.*)""")]
        public void Given原始訊息為(string originRecord)
        {
            ScenarioContext.Current.Set(originRecord, "originRecord");
        }

        [Given(@"來源IP為""(.*)""")]
        public void Given來源IP為(string sourceIp)
        {
            ScenarioContext.Current.Set(sourceIp, "sourceIp");
        }


        [When(@"執行EF告警作業")]
        public void When執行EF告警作業()
        {
            var detector = ScenarioContext.Current.Get<Detector>("detector");
            var originRecord = ScenarioContext.Current.Get<string>("originRecord");
            var deviceType = ScenarioContext.Current.Get<DeviceType>("deviceType");
            var sourceIp = ScenarioContext.Current.Get<string>("sourceIp");
            _workDirector = new WorkDirectorFake(detector, originRecord, deviceType, sourceIp, _devices, _targets, _alarmConditions);

            _workDirector.Execute();
        }

        [Then(@"EF解析告警結果為")]
        public void ThenEF解析告警結果為(Table table)
        {
            table.CompareToSet(_workDirector.Monitors);
        }

        [When(@"執行EF通知檢查作業")]
        public void When執行EF通知檢查作業()
        {
            var strategy = new GenericNotifierStrategy();
            _notificationDirector = new NotificationDirectorFake(strategy, _workDirector.Monitors, _previousMonitors, _notificationConditions, _notifications);
            _notificationDirector.Execute();
        }

        [Then(@"EF通知檢查結果為")]
        public void ThenEF通知檢查結果為(Table table)
        {
            table.CompareToSet(_notificationDirector.Monitors);
        }
    }
}