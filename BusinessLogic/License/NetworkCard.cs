using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace BusinessLogic.License
{
    /// <summary>
    /// 網卡資訊
    /// </summary>
    public static class NetworkCard
    {
        /// <summary>
        /// 網卡實體位址清單
        /// </summary>
        public static List<string> MacAddresses { get; private set; }

        /// <summary>
        /// 建構式
        /// </summary>
        static NetworkCard()
        {
            SetMacAddress();
        }

        /// <summary>
        /// MAC Address 設置
        /// </summary>
        /// <returns></returns>
        private static void SetMacAddress()
        {
            var netWorkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            MacAddresses = new List<string>();

            foreach (var adapter in netWorkInterfaces)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    // Get first adapter mac address
                    var mac = adapter.GetPhysicalAddress().ToString();
                    MacAddresses.Add(Regex.Replace(mac, @"[\W_]+", "").ToUpper());
                }
            }
        }
    }
}