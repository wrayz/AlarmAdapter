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
        /// 建構式
        /// </summary>
        public AbuseIpDbService(string ipAddress)
        {
            _ipAddress = ipAddress;
        }

        /// <summary>
        /// 使用 Abuse Api 確認黑名單
        /// </summary>
        public ReportedIP GetReportedIP(AbuseIpDbSetting abuse)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.abuseipdb.com/");

                string formatApi = "check-block/json?network={0}/{1}&key={2}&days={3}";
                var requestUri = string.Format(formatApi, _ipAddress, abuse.CIDR, abuse.API_KEY, abuse.SEARCHE_DAYS);

                using (var response = client.PostAsync(requestUri, null).Result)
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();

                        var blockResult = response.Content.ReadAsAsync<BlockResult>().Result;

                        var reported = blockResult.reportedIPs.Find(x => x.IP.Contains(_ipAddress));
                        return reported ?? new ReportedIP();
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