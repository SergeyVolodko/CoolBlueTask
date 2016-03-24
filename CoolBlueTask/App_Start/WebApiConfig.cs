using System.Web.Http;

namespace CoolBlueTask
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureRoutes(config);


        }

        public static HttpConfiguration ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.EnsureInitialized();

            return config;
        }
    }
}
