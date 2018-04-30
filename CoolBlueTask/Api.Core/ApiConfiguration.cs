using System.Configuration;

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

			DbConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
		}
	}
}