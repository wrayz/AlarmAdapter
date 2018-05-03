using DataAccess;
using LicenseHelper.Decrypt;
using LicenseHelper.Token;
using ModelLibrary.Generic;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    /// <summary>
    /// License 服務
    /// </summary>
    public static class LicenseLogic
    {
        /// <summary>
        /// MAC Address list
        /// </summary>
        private static List<string> _macAddresss;

        /// <summary>
        /// Token
        /// </summary>
        public static EyesFreeToken Token;

        /// <summary>
        /// Valid MAC address
        /// </summary>
        public static string ValidMAC;

        /// <summary>
        /// 建構式
        /// </summary>
        static LicenseLogic()
        {
            // MAC Address list
            _macAddresss = GetMacAddress();

            // Update token
            UpdateToken(GetLicense());
        }

        /// <summary>
        /// EyesFree license token 更新
        /// </summary>
        /// <param name="serial">License key</param>
        public static void UpdateToken(string serial)
        {
            var result = new EyesFreeToken();

            foreach (var address in _macAddresss)
            {
                result = DecryptHelper.Decrypt<EyesFreeToken>(serial, address);

                if (result != null)
                {
                    // Token
                    Token = result;
                    // Valid MAC
                    ValidMAC = address;

                    return;
                }
            }
        }

        /// <summary>
        /// MAC Address 取得
        /// </summary>
        /// <returns></returns>
        private static List<string> GetMacAddress()
        {
            var netWorkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            var macList = new List<string>();

            foreach (var adapter in netWorkInterfaces)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    // Get first adapter mac address
                    var mac = adapter.GetPhysicalAddress().ToString();
                    macList.Add(Regex.Replace(mac, @"[\W_]+", "").ToUpper());
                }
            }

            return macList;
        }

        /// <summary>
        /// License key取得
        /// </summary>
        /// <returns></returns>
        private static string GetLicense()
        {
            var dao = GenericDataAccessFactory.CreateInstance<LicenseConfig>();
            var option = new QueryOption();

            return dao.Get(option).LICENSE_KEY;
        }
    }
}