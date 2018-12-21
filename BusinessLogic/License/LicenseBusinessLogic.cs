using DataAccess;
using LicenseHelper.Decrypt;
using LicenseHelper.Token;
using ModelLibrary;
using ModelLibrary.Generic;
using System;
using System.Net.Http;

namespace BusinessLogic.License
{
    /// <summary>
    /// License 商業邏輯
    /// </summary>
    public class LicenseBusinessLogic
    {
        private string _licenseKey;

        private EyesFreeToken _token;

        private string _validMacAddress;

        /// <summary>
        /// 建構式
        /// </summary>
        public LicenseBusinessLogic()
        {
            Initialize();
        }

        /// <summary>
        /// License 驗證
        /// </summary>
        /// <param name="time">驗證時間</param>
        public void Verify(DateTime? time)
        {
            if (_token == null)
                throw new HttpRequestException("License key 無效，請檢查License Key");

            if (!(time >= _token.StartDate && time <= _token.EndDate))
                throw new HttpRequestException("License key 已過期，請檢查License Key");
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            SetLicense();
            SetToken();
        }

        /// <summary>
        /// EyesFree license token 更新
        /// </summary>
        private void SetToken()
        {
            var result = new EyesFreeToken();

            foreach (var address in NetworkCard.MacAddresses)
            {
                result = DecryptHelper.Decrypt<EyesFreeToken>(_licenseKey, address);

                if (result != null)
                {
                    // Token
                    _token = result;
                    // Valid MAC
                    _validMacAddress = address;

                    return;
                }
            }
        }

        /// <summary>
        /// License key 設置
        /// </summary>
        /// <returns></returns>
        private void SetLicense()
        {
            var dao = GenericDataAccessFactory.CreateInstance<LicenseConfig>();
            var option = new QueryOption();

            _licenseKey = dao.Get(option).LICENSE_KEY;
        }
    }
}