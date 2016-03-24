using System.Web.Http;

namespace CoolBlueTask
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnsureInitialized();
        }
    }
}
