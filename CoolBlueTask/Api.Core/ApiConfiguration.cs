using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace CoolBlueTask
{
	public interface IApiConfiguration
	{
		string Auth0Issuer { get; }
		string Auth0Audience { get; }
		string Auth0Secret { get; }

		string DbConnectionString { get; }
	}

	public class ApiConfiguration : IApiConfiguration
	{
		public string Auth0Issuer { get; }
		public string Auth0Audience { get; }
		public string Auth0Secret { get; }
		public string DbConnectionString { get; }

		public ApiConfiguration()
		{
			Auth0Issuer = ConfigurationManager.AppSettings["Auth0Issuer"];
			Auth0Audience = ConfigurationManager.AppSettings["Auth0Audience"];
			Auth0Secret = ConfigurationManager.AppSettings["Auth0Secret"];

			DbConnectionString = Path.Combine(AssemblyDirectory, ConfigurationManager.AppSettings["DbConnectionString"]);
		}

		private static string AssemblyDirectory
		{
			get
			{
				var codeBase = Assembly.GetAssembly(typeof(ApiConfiguration)).CodeBase;
				var uri = new UriBuilder(codeBase);
				var path = Uri.UnescapeDataString(uri.Path);

				return Path.GetDirectoryName(path);
			}
		}
	}
}