using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace APIService
{
    /// <summary>
    /// 黑名單資料庫查詢服務
    /// </summary>
    public class AbuseIpDbService
    {
        /// <summary>
        /// 待查黑名單 IP
        /// </summary>
        private string _ipAddress;

        /// <summary>
        /// 黑名單資料庫設定
        /// </summary>
        private AbuseIpDbSetting _abuseIpDbSetting;

        /// <summary>
        /// 黑名單報告清單
        /// </summary>
        private ReportedIP _reportedIP;

        /// <summary>
        /// 建構式
        /// </summary>
        public AbuseIpDbService(string ipAddress)
        {
            _ipAddress = ipAddress;

            GetAbuseDbIpSetting();
        }

        /// <summary>
        /// 黑名單資料庫設定資料取得
        /// </summary>
        private void GetAbuseDbIpSetting()
        {
            var bll = GenericBusinessFactory.CreateInstance<AbuseIpDbSetting>();
            _abuseIpDbSetting = bll.Get(new QueryOption(), new UserLogin());
        }

        /// <summary>
        /// 黑名單是否已報告
        /// </summary>
        /// <returns></returns>
        public bool IsReported()
        {
            if (_abuseIpDbSetting.CONFIDENCE_SCORE == 0)
                return true;

            PostCheckBlock();

            if (_reportedIP == null)
                _reportedIP = new ReportedIP();

            return _reportedIP.abuseConfidenceScore >= _abuseIpDbSetting.CONFIDENCE_SCORE;
        }

        /// <summary>
        /// 使用 Abuse Api 確認黑名單
        /// </summary>
        private void PostCheckBlock()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.abuseipdb.com/");

                string formatApi = "check-block/json?network={0}/31&key={1}&days=1";
                var requestUri = string.Format(formatApi, _ipAddress, _abuseIpDbSetting.API_KEY);

                using (var response = client.PostAsync(requestUri, null).Result)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();

                        var blockResult = response.Content.ReadAsAsync<BlockResult>().Result;

                        _reportedIP = blockResult.reportedIPs.Find(x => x.IP.Contains(_ipAddress));
                    }
                    catch (HttpRequestException)
                    {
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 黑名單結果
    /// </summary>
    internal class BlockResult
    {
        public string networkAddress { get; set; }

        public string netmask { get; set; }

        public string minAddress { get; set; }

        public string maxAddress { get; set; }

        public int numPossibleHosts { get; set; }

        public string addressSpaceDesc { get; set; }

        public List<ReportedIP> reportedIPs { get; set; }
    }

    /// <summary>
    /// 已反應名單
    /// </summary>
    internal class ReportedIP
    {
        public string IP { get; set; }

        public string NumReports { get; set; }

        public string MostRecentReport { get; set; }

        public int Public { get; set; }

        public string CountryCode { get; set; }

        public int IsWhitelisted { get; set; }

        public int abuseConfidenceScore { get; set; }
    }
}