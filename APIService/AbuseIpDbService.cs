using BusinessLogic;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
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
        public ReportedIP ReportedIP { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        public AbuseIpDbService(string ipAddress)
        {
            _ipAddress = ipAddress;

            InitSet();
        }

        private void InitSet()
        {
            SetAbuseDbIpSetting();
            SetReportedIP();
        }

        /// <summary>
        /// 黑名單資料庫設定資料取得
        /// </summary>
        private void SetAbuseDbIpSetting()
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
            if (_abuseIpDbSetting.ABUSE_SCORE == 0)
                return true;

            return ReportedIP.abuseConfidenceScore >= _abuseIpDbSetting.ABUSE_SCORE;
        }

        /// <summary>
        /// 使用 Abuse Api 確認黑名單
        /// </summary>
        private void SetReportedIP()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.abuseipdb.com/");

                string formatApi = "check-block/json?network={0}/{1}&key={2}&days={3}";
                var requestUri = string.Format(formatApi, _ipAddress, _abuseIpDbSetting.CIDR, _abuseIpDbSetting.API_KEY, _abuseIpDbSetting.SEARCHE_DAYS);

                using (var response = client.PostAsync(requestUri, null).Result)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();

                        var blockResult = response.Content.ReadAsAsync<BlockResult>().Result;

                        var reported = blockResult.reportedIPs.Find(x => x.IP.Contains(_ipAddress));
                        ReportedIP = reported == null ? new ReportedIP() : reported;
                    }
                    catch (HttpRequestException)
                    {
                        throw new NotSupportedException();
                    }
                }
            }
        }
    }
}