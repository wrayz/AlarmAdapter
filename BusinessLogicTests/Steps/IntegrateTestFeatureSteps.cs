using BusinessLogic.Director;
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

        [Given(@"設備清單為")]
        public void Given設備清單為(Table table)
        {
            _devices = table.CreateSet<Device>().ToList();
        }

        [Given(@"告警條件為")]
        public void Given告警條件為(Table table)
        {
            _alarmConditions = table.CreateSet<AlarmCondition>().ToList();
        }

        [Given(@"偵測器""(.*)""")]
        public void Given偵測器(string detector)
        {
            ScenarioContext.Current.Set(detector, "detector");
        }

        [Given(@"設備類型為""(.*)""")]
        public void Given設備類型為(string deviceType)
        {
            ScenarioContext.Current.Set(deviceType, "deviceType");
        }

        [Given(@"原始訊息為""(.*)""")]
        public void Given原始訊息為(string originRecord)
        {
            ScenarioContext.Current.Set(originRecord, "originRecord");
        }

        [When(@"執行EF作業")]
        public void When執行EF作業()
        {
            var detector= ScenarioContext.Current.Get<string>("detector");
            var deviceType = ScenarioContext.Current.Get<string>("deviceType");
            var originRecord = ScenarioContext.Current.Get<string>("originRecord");
            _workDirector = new WorkDirectorFake(detector, originRecord, deviceType, _devices, _alarmConditions);

            _workDirector.Execute();
        }

        [Then(@"EF解析告警結果為")]
        public void ThenEF解析告警結果為(Table table)
        {
            table.CompareToSet(_workDirector.Monitors);
        }
    }
}