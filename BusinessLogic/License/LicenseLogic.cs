using DataAccess;
using LicenseHelper.Decrypt;
using LicenseHelper.Token;
using ModelLibrary.Generic;
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
        /// MAC Address
        /// </summary>
        private static string _macAddress;

        /// <summary>
        /// Token
        /// </summary>
        public static EyesFreeToken Token;

        /// <summary>
        /// 建構式
        /// </summary>
        static LicenseLogic()
        {
            //MAC Address
            _macAddress = GetMacAddress();

            //Token
            UpdateToken(GetLicense());
        }

        /// <summary>
        /// EyesFree license token 更新
        /// </summary>
        /// <param name="serial">License key</param>
        public static void UpdateToken(string serial)
        {
            //Token
            Token = DecryptHelper.Decrypt<EyesFreeToken>(serial, _macAddress);
        }

        /// <summary>
        /// MAC Address取得
        /// </summary>
        /// <returns></returns>
        private static string GetMacAddress()
        {
            NetworkInterface[] netWorkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in netWorkInterfaces)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    // Get first adapter mac address
                    string mac = adapter.GetPhysicalAddress().ToString();
                    return Regex.Replace(mac, @"[\W_]+", "").ToUpper();
                }
            }

            return "";
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