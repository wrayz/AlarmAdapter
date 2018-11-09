using BusinessLogic.Director;
using ModelLibrary;
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

        [Given(@"原始訊息為""(.*)""")]
        public void Given原始訊息為(string originRecord)
        {
            ScenarioContext.Current.Set(originRecord, "originRecord");
        }

        [When(@"執行EF作業")]
        public void When執行EF作業()
        {
            var originRecord = ScenarioContext.Current.Get<string>("originRecord");
            _workDirector = new WorkDirector(originRecord);

            _workDirector.Execute();
        }

        [Then(@"EF解析告警結果為")]
        public void ThenEF解析告警結果為(Table table)
        {
            table.CompareToSet(_workDirector.Monitors);
        }
    }
}