using System.Net.Http.Formatting;
using System.Web.Http;
using Swashbuckle.Application;

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

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Routes.MapHttpRoute(
                name: "help_ui_shortcut",
                routeTemplate: "help",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "help/index"));

            // start page is help peage
            config.Routes.MapHttpRoute(
                name: "start_url",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "help/index"));

            config.EnsureInitialized();

            return config;
        }
    }
}
