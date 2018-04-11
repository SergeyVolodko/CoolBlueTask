using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using CoolBlueTask.Products;
using Microsoft.Owin;
using NLog;
using Owin;

[assembly: OwinStartup(typeof(CoolBlueTask.Startup))]
namespace CoolBlueTask
{
	public class Startup
	{
		/// <summary>
		/// Exposed for test purposes
		/// </summary>
		internal IContainer container;

		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			config = WebApiConfig.ConfigureRoutes(config);

			SwaggerConfig.Register(config);

			var builder = new ContainerBuilder();

			builder.RegisterModule(new DomainCoreModule());
			builder.RegisterModule(new ProductModule());
			builder.RegisterType<VersionController>();

			builder.RegisterType<ApiExceptionFilterAttribute>()
				.AsWebApiExceptionFilterFor<ApiController>();

			container = builder.Build();

			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			app.UseWebApi(config);
		}
	}
}