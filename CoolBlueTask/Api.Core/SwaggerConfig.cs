using System;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Application;

namespace CoolBlueTask
{
	public class SwaggerConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;
			var assetsPath = "help/{*assetPath}";

			config.EnableSwagger(c =>
			{
#if !DEBUG // This line is added to handle virtual application paths on IIS
				//c.RootUrl(resolveRootUrlWithVirtualApplication);
#endif
				c.SingleApiVersion("v1", "Cool Blue API");
			})
			.EnableSwaggerUi(assetsPath, c =>
			{
				c.InjectStylesheet(thisAssembly, "CoolBlueTask.HelpPage.help.css");

				c.InjectStylesheet(thisAssembly, "CoolBlueTask.HelpPage.logo.png");

				c.CustomAsset("index", thisAssembly, "CoolBlueTask.HelpPage.help.html");
			});
		}

		private static string resolveRootUrlWithVirtualApplication(HttpRequestMessage req)
		{
			return $"{req.RequestUri.GetLeftPart(UriPartial.Authority)}{req.GetRequestContext().VirtualPathRoot.TrimEnd('/')}";
		}
	}
}
