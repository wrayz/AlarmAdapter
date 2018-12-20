using ModelLibrary;
using ModelLibrary.Generic;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace BusinessLogic
{
    /// <summary>
    /// 黑名單資料庫商業邏輯
    /// </summary>
    public class AbuseIpDbBusinessLogic
    {
        private readonly Monitor _monitor;
        private readonly AbuseIpDbSetting _setting;

        /// <summary>
        /// 建構式
        /// </summary>
        public AbuseIpDbBusinessLogic(Monitor monitor)
        {
            _monitor = monitor;
            _setting = GetAbuseIpDbSetting();
        }

        /// <summary>
        /// 黑名單分數檢查
        /// </summary>
        /// <returns></returns>
        public bool CheckScore()
        {
            if (_setting.ABUSE_SCORE == 0) return true;

            var blockHole = GetBlockHole();
            return blockHole.ABUSE_SCORE.Value >= _setting.ABUSE_SCORE;
        }

        /// <summary>
        /// 黑名單資訊取得
        /// </summary>
        /// <returns></returns>
        private BlockHole GetBlockHole()
        {
            var bll = GenericBusinessFactory.CreateInstance<BlockHole>() as BlockHole_BLL;
            var blockHole = bll.GetBlockHole(_monitor.TARGET_VALUE);

            if (IsExpired(blockHole))
            {
                var reportedIp = GetReportedIP();

                return bll.Save(_monitor.TARGET_VALUE, reportedIp.abuseConfidenceScore);
            }

            return blockHole;
        }

        /// <summary>
        /// 黑名單分數是否到期
        /// </summary>
        /// <param name="blockHole">黑名單資訊</param>
        /// <returns></returns>
        private bool IsExpired(BlockHole blockHole)
        {
            if (blockHole.ABUSE_SCORE == null) return true;

            var leadtime = new TimeSpan(_monitor.RECEIVE_TIME.Value.Ticks - blockHole.REQUEST_TIME.Value.Ticks).Hours;
            return leadtime >= _setting.CHECK_CYCLE;
        }

        /// <summary>
        /// 黑名單資料庫設定取得
        /// </summary>
        /// <returns></returns>
        private AbuseIpDbSetting GetAbuseIpDbSetting()
        {
            var bll = GenericBusinessFactory.CreateInstance<AbuseIpDbSetting>();
            return bll.Get(new QueryOption(), new UserLogin());
        }

        /// <summary>
        /// 黑名單資訊取得（AbuseDbIp API）
        /// </summary>
        public ReportedIP GetReportedIP()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.abuseipdb.com/");

                string formatApi = "check-block/json?network={0}/{1}&key={2}&days={3}";
                var requestUri = string.Format(formatApi, _monitor.TARGET_VALUE, _setting.CIDR, _setting.API_KEY, _setting.SEARCHE_DAYS);

                using (var response = client.PostAsync(requestUri, null).Result)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();

                        var result = JsonConvert.DeserializeObject<BlockResult>(response.Content.ReadAsStringAsync().Result);
                        var reported = result.reportedIPs.Find(x => x.IP.Contains(_monitor.TARGET_VALUE));

                        return reported ?? new ReportedIP { abuseConfidenceScore = 0 };
                    }
                    catch (HttpRequestException)
                    {
                        return new ReportedIP();
                    }
                }
            }
        }
    }
}