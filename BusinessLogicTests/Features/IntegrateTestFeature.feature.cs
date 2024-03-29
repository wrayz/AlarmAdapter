﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BusinessLogicTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class IntegrateTestFeatureFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
#line 1 "IntegrateTestFeature.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "IntegrateTestFeature", "\tIn order to \r\n\tAs a \r\n\tI want to ", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "IntegrateTestFeature")))
            {
                global::BusinessLogicTests.Features.IntegrateTestFeatureFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "DEVICE_TYPE"});
            table1.AddRow(new string[] {
                        "2018001",
                        "192.168.10.99",
                        "N"});
            table1.AddRow(new string[] {
                        "2018002",
                        "192.168.10.98",
                        "N"});
            table1.AddRow(new string[] {
                        "2018003",
                        "192.168.60.87",
                        "S"});
            table1.AddRow(new string[] {
                        "2018004",
                        "10.2.253.5",
                        "S"});
            table1.AddRow(new string[] {
                        "2018005",
                        "10.1.10.9",
                        "D"});
#line 7
testRunner.Given("設備清單為", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "TARGET_NAME",
                        "TARGET_STATUS",
                        "OPERATOR_TYPE",
                        "IS_EXCEPTION"});
            table2.AddRow(new string[] {
                        "2018001",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "0",
                        "Equal",
                        "Y"});
            table2.AddRow(new string[] {
                        "2018001",
                        "Ping",
                        "0",
                        "In",
                        "Y"});
            table2.AddRow(new string[] {
                        "2018003",
                        "EVENT_TYPE",
                        "0",
                        "Always",
                        "Y"});
            table2.AddRow(new string[] {
                        "2018003",
                        "VIDEO",
                        "0",
                        "Always",
                        "Y"});
            table2.AddRow(new string[] {
                        "2018004",
                        "detect block ip",
                        "0",
                        "Always",
                        "Y"});
            table2.AddRow(new string[] {
                        "2018005",
                        "Humidity",
                        "0",
                        "Between",
                        "N"});
            table2.AddRow(new string[] {
                        "2018005",
                        "Temperature",
                        "0",
                        "Between",
                        "N"});
#line 14
testRunner.Given("監控項目資訊", ((string)(null)), table2, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "TARGET_NAME",
                        "TARGET_VALUE"});
            table3.AddRow(new string[] {
                        "2018001",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "ALERT"});
            table3.AddRow(new string[] {
                        "2018001",
                        "Ping",
                        "ERROR"});
            table3.AddRow(new string[] {
                        "2018001",
                        "Ping",
                        "DOWN"});
            table3.AddRow(new string[] {
                        "2018005",
                        "Humidity",
                        "3500"});
            table3.AddRow(new string[] {
                        "2018005",
                        "Humidity",
                        "4500"});
            table3.AddRow(new string[] {
                        "2018005",
                        "Temperature",
                        "3000"});
            table3.AddRow(new string[] {
                        "2018005",
                        "Temperature",
                        "5000"});
#line 23
testRunner.Given("告警條件為", ((string)(null)), table3, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "RECORD_SN",
                        "DEVICE_SN",
                        "TARGET_NAME",
                        "IS_EXCEPTION"});
            table4.AddRow(new string[] {
                        "2018111200001",
                        "2018001",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "N"});
            table4.AddRow(new string[] {
                        "2018111200002",
                        "2018002",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "Y"});
#line 32
testRunner.Given("前次監控訊息為", ((string)(null)), table4, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "NOTIFICATION_TYPE",
                        "INTERVAL_LEVEL",
                        "INTERVAL_TIME"});
            table5.AddRow(new string[] {
                        "2018001",
                        "0",
                        "0",
                        "2"});
            table5.AddRow(new string[] {
                        "2018002",
                        "0",
                        "1",
                        "0"});
            table5.AddRow(new string[] {
                        "2018003",
                        "1",
                        "1",
                        "0"});
            table5.AddRow(new string[] {
                        "2018004",
                        "1",
                        "2",
                        "1"});
            table5.AddRow(new string[] {
                        "2018005",
                        "0",
                        "0",
                        "1"});
#line 36
testRunner.Given("通知條件為", ((string)(null)), table5, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "RECORD_SN",
                        "DEVICE_SN",
                        "TARGET_NAME",
                        "TARGET_MESSAGE",
                        "NOTIFICATION_TIME"});
            table6.AddRow(new string[] {
                        "2018111200001",
                        "2018002",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "current value is 5630.6207",
                        "2018/11/06 17:08:30"});
            table6.AddRow(new string[] {
                        "2018111200001",
                        "2018004",
                        "detect block ip",
                        "From 10.2.253.5 detect block ip 103.210.135.136",
                        "2018/8/8 16:14:20"});
#line 43
testRunner.Given("通知記錄為", ((string)(null)), table6, "Given ");
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Cacti_ALERT訊息")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void Cacti_ALERT訊息()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cacti_ALERT訊息", null, ((string[])(null)));
#line 48
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 49
 testRunner.Given("偵測器\"Cacti\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.And("設備類型為\"N\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("原始訊息為\"{ \"id\":\"192.168.10.99\", \"target\": \"Traffic - Gi1/0/20 [traffic_in]\", \"actio" +
                    "n\":\"ALERT\", \"info\":\"current value is 5630.6207\",\"time\":\"2018/11/06 18:08:34\" }\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION"});
            table7.AddRow(new string[] {
                        "2018001",
                        "192.168.10.99",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "ALERT",
                        "current value is 5630.6207",
                        "2018/11/06 18:08:34",
                        "Y"});
#line 54
 testRunner.Then("EF解析告警結果為", ((string)(null)), table7, "Then ");
#line 57
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table8.AddRow(new string[] {
                        "2018001",
                        "192.168.10.99",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "ALERT",
                        "current value is 5630.6207",
                        "2018/11/06 18:08:34",
                        "Y",
                        "Y"});
#line 58
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table8, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Cacti_NORMAL訊息")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void Cacti_NORMAL訊息()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cacti_NORMAL訊息", null, ((string[])(null)));
#line 62
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 63
 testRunner.Given("偵測器\"Cacti\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 64
 testRunner.And("設備類型為\"N\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("原始訊息為\"{ \"id\":\"192.168.10.98\", \"target\": \"Traffic - Gi1/0/20 [traffic_in]\", \"actio" +
                    "n\":\"NORMAL\", \"info\":\"current value is 5630.6207\",\"time\":\"2018/11/06 18:08:34\" }\"" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION"});
            table9.AddRow(new string[] {
                        "2018002",
                        "192.168.10.98",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "NORMAL",
                        "current value is 5630.6207",
                        "2018/11/06 18:08:34",
                        "N"});
#line 68
 testRunner.Then("EF解析告警結果為", ((string)(null)), table9, "Then ");
#line 71
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table10.AddRow(new string[] {
                        "2018002",
                        "192.168.10.98",
                        "Traffic - Gi1/0/20 [traffic_in]",
                        "NORMAL",
                        "current value is 5630.6207",
                        "2018/11/06 18:08:34",
                        "N",
                        "Y"});
#line 72
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table10, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("BobCacti_Error訊息")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void BobCacti_Error訊息()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BobCacti_Error訊息", null, ((string[])(null)));
#line 76
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 77
 testRunner.Given("偵測器\"BobCacti\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 78
 testRunner.And("設備類型為\"N\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And("原始訊息為\"{ \"DEVICE_ID\":\"192.168.10.99\", \"ACTION_TYPE\":\"ERROR\", \"LOG_INFO\":\"ping down" +
                    "\",\"LOG_TIME\":\"2018-09-13T13:21:30\" }\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION"});
            table11.AddRow(new string[] {
                        "2018001",
                        "192.168.10.99",
                        "Ping",
                        "ERROR",
                        "ping down",
                        "2018/09/13 13:21:30",
                        "Y"});
#line 82
 testRunner.Then("EF解析告警結果為", ((string)(null)), table11, "Then ");
#line 85
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table12.AddRow(new string[] {
                        "2018001",
                        "192.168.10.99",
                        "Ping",
                        "ERROR",
                        "ping down",
                        "2018/09/13 13:21:30",
                        "Y",
                        "Y"});
#line 86
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table12, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Camera發送告警")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void Camera發送告警()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Camera發送告警", null, ((string[])(null)));
#line 90
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 91
 testRunner.Given("偵測器\"Camera\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 92
 testRunner.And("設備類型為\"S\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.And("原始訊息為\"EVENT_TYPE=Camera tampering detection&VIDEO=123\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And("來源IP為\"192.168.60.87\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "IS_EXCEPTION"});
            table13.AddRow(new string[] {
                        "2018003",
                        "192.168.60.87",
                        "EVENT_TYPE",
                        "Camera tampering detection",
                        "Camera tampering detection",
                        "Y"});
            table13.AddRow(new string[] {
                        "2018003",
                        "192.168.60.87",
                        "VIDEO",
                        "123",
                        "123",
                        "Y"});
#line 96
 testRunner.Then("EF解析告警結果為", ((string)(null)), table13, "Then ");
#line 100
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table14.AddRow(new string[] {
                        "2018003",
                        "192.168.60.87",
                        "EVENT_TYPE",
                        "Camera tampering detection",
                        "Camera tampering detection",
                        "Y",
                        "Y"});
            table14.AddRow(new string[] {
                        "2018003",
                        "192.168.60.87",
                        "VIDEO",
                        "123",
                        "123",
                        "Y",
                        "Y"});
#line 101
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table14, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Logmaster黑名單告警_通知")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void Logmaster黑名單告警_通知()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Logmaster黑名單告警_通知", null, ((string[])(null)));
#line 107
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 108
 testRunner.Given("偵測器\"Logmaster\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 109
 testRunner.And("設備類型為\"S\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
 testRunner.And("原始訊息為\"{ \"DEVICE_ID\": \"10.2.253.5\", \"LOG_INFO\": \"From 10.2.253.5 detect block ip 1" +
                    "03.210.135.136\", \"LOG_TIME\": \"2018/8/8 16:20:10\" }\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION"});
            table15.AddRow(new string[] {
                        "2018004",
                        "10.2.253.5",
                        "detect block ip",
                        "103.210.135.136",
                        "From 10.2.253.5 detect block ip 103.210.135.136",
                        "2018/8/8 16:20:10",
                        "Y"});
#line 113
 testRunner.Then("EF解析告警結果為", ((string)(null)), table15, "Then ");
#line 116
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table16.AddRow(new string[] {
                        "2018004",
                        "10.2.253.5",
                        "detect block ip",
                        "103.210.135.136",
                        "From 10.2.253.5 detect block ip 103.210.135.136",
                        "2018/8/8 16:20:10",
                        "Y",
                        "Y"});
#line 117
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table16, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Logmaster黑名單告警_不通知")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void Logmaster黑名單告警_不通知()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Logmaster黑名單告警_不通知", null, ((string[])(null)));
#line 121
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 122
 testRunner.Given("偵測器\"Logmaster\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 123
 testRunner.And("設備類型為\"S\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 124
 testRunner.And("原始訊息為\"{ \"DEVICE_ID\": \"10.2.253.5\", \"LOG_INFO\": \"From 10.2.253.5 detect block ip 1" +
                    "03.210.135.136\", \"LOG_TIME\": \"2018/8/8 16:14:30\" }\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 125
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 126
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION"});
            table17.AddRow(new string[] {
                        "2018004",
                        "10.2.253.5",
                        "detect block ip",
                        "103.210.135.136",
                        "From 10.2.253.5 detect block ip 103.210.135.136",
                        "2018/8/8 16:14:30",
                        "Y"});
#line 127
 testRunner.Then("EF解析告警結果為", ((string)(null)), table17, "Then ");
#line 130
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "RECEIVE_TIME",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table18.AddRow(new string[] {
                        "2018004",
                        "10.2.253.5",
                        "detect block ip",
                        "103.210.135.136",
                        "From 10.2.253.5 detect block ip 103.210.135.136",
                        "2018/8/8 16:14:30",
                        "Y",
                        "N"});
#line 131
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table18, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("溫濕度計告警")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "IntegrateTestFeature")]
        public virtual void 溫濕度計告警()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("溫濕度計告警", null, ((string[])(null)));
#line 135
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
this.FeatureBackground();
#line 136
 testRunner.Given("偵測器\"iFace\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 137
 testRunner.And("設備類型為\"D\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 138
 testRunner.And("原始訊息為\"{\"Date\":\"08/10/18\",\"Time\":\"17:51:48\",\"Name_1\":\"10.1.10.9\",\"Humidity_1\":3045" +
                    ",\"Temperature_1\":3266,\"Name_2\":\"\",\"Humidity_2\":0,\"Temperature_2\":0,\"Name_3\":\"\",\"" +
                    "Humidity_3\":0,\"Temperature_3\":0,\"Name_4\":\"\",\"Humidity_4\":0,\"Temperature_4\":0}\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 139
 testRunner.And("來源IP為\"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 140
 testRunner.When("執行EF告警作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "IS_EXCEPTION"});
            table19.AddRow(new string[] {
                        "2018005",
                        "10.1.10.9",
                        "Humidity",
                        "3045",
                        "3045",
                        "Y"});
            table19.AddRow(new string[] {
                        "2018005",
                        "10.1.10.9",
                        "Temperature",
                        "3266",
                        "3266",
                        "N"});
#line 141
 testRunner.Then("EF解析告警結果為", ((string)(null)), table19, "Then ");
#line 145
 testRunner.When("執行EF通知檢查作業", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "DEVICE_SN",
                        "DEVICE_ID",
                        "TARGET_NAME",
                        "TARGET_VALUE",
                        "TARGET_MESSAGE",
                        "IS_EXCEPTION",
                        "IS_NOTIFICATION"});
            table20.AddRow(new string[] {
                        "2018005",
                        "10.1.10.9",
                        "Humidity",
                        "3045",
                        "3045",
                        "Y",
                        "Y"});
            table20.AddRow(new string[] {
                        "2018005",
                        "10.1.10.9",
                        "Temperature",
                        "3266",
                        "3266",
                        "N",
                        "N"});
#line 146
 testRunner.Then("EF通知檢查結果為", ((string)(null)), table20, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
