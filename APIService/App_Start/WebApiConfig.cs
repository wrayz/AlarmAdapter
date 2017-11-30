using System.Web.Http;

namespace APIService
{
    /// <summary>
    /// Web API 設置
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
#if DEBUG
            //Exception detail
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}