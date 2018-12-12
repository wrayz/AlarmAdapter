using BusinessLogic.NotificationContent;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace APIService.NotificationPusher
{
    internal class DesktopPusher : IPusher
    {
        private readonly string _url;

        public DesktopPusher()
        {
            _url = ConfigurationManager.AppSettings["socket"];
        }

        public HttpResponseMessage Push(GenericContent content)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                var response = client.PostAsync(_url, stringContent).Result;

                return response.EnsureSuccessStatusCode();
            }
        }
    }
}