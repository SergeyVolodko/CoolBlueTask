namespace CoolBlueTask.Tests.Scenarios.Data
{
	public class ScenariosApiConfiguration : IApiConfiguration
	{
		public string Auth0Issuer => "my-test-api.eu.auth0.com";
		public string Auth0Audience => "Q3XqA2aXJjZYIqZ86PhszauwIHzTZ1TE";
		public string Auth0Secret => "SqhWVyDUjx6b53DyLt7YooFAXkH0U3kJ7ulLHOrPpXmpmz0qxhaqShwm5LPR-tWi";
		public string DbConnectionString => $"{Consts.TestDataFolder}\\test-db.sqlite";
	}
}
