using System.Text;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Simple.Data;

[assembly: OwinStartup(typeof(CoolBlueTask.Startup))]
namespace CoolBlueTask
{
	public class Startup
	{
		/// <summary>
		/// Exposed for test purposes
		/// </summary>
		internal IContainer container;
		internal IApiConfiguration apiConfiguration;

		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			config = WebApiConfig.ConfigureRoutes(config);

			SwaggerConfig.Register(config);

			if (apiConfiguration == null)
			{
				apiConfiguration = new ApiConfiguration();
			}

			var builder = new ContainerBuilder();

			builder.RegisterInstance(apiConfiguration).As<IApiConfiguration>()
				.SingleInstance();

			builder.RegisterModule(new DomainCoreModule());
			builder.RegisterModule(new ProductModule());
			builder.RegisterModule(new SalesCombinationsModule());
			builder.RegisterType<VersionController>();

			// Critical to make filters work
			builder.RegisterWebApiFilterProvider(config);
			builder.RegisterType<ApiExceptionFilterAttribute>()
				.AsWebApiExceptionFilterFor<ApiController>();

			container = builder.Build();

			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			configureAuthZero(app, container);

			app.UseWebApi(config);
		}

		private static void configureAuthZero(IAppBuilder app, IContainer container)
		{
			var config = container.Resolve<IApiConfiguration>();
			app.UseJwtBearerAuthentication(
				new JwtBearerAuthenticationOptions
				{
					AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
					AllowedAudiences = new[] { config.Auth0Audience },
					IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
					{
						new SymmetricKeyIssuerSecurityTokenProvider(config.Auth0Issuer,
							Encoding.UTF8.GetBytes(config.Auth0Secret))
					}
				});
		}
	}
}