using System.Reflection;
using System.Web.Http;
using CoolBlueTask.Api.Core;

namespace CoolBlueTask
{
	[RoutePrefix("version")]
	public class VersionController : ApiController
	{
		[HttpGet]
		[Route("")]
		public string Get()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;

			return version.ToString();
		}

		[HttpGet]
		[Auth0Authorization]
		[Route("testauth")]
		public string TestAuth()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version;

			return version.ToString();
		}
	}
}