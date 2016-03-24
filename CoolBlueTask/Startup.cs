using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CoolBlueTask.Products;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CoolBlueTask.Startup))]
namespace CoolBlueTask
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config = WebApiConfig.ConfigureRoutes(config);

            SwaggerConfig.Register(config);

            var builder = new ContainerBuilder();

            builder.RegisterModule(new ProductModule());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseWebApi(config);
        }
    }
}