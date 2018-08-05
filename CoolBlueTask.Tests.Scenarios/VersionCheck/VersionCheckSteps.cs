using System.Net;
using CoolBlueTask.Tests.Scenarios.Data;
using CoolBlueTask.Tests.Scenarios.Infrastructure;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoolBlueTask.Tests.Scenarios.Version
{
	[Binding]
	public class VersionCheckSteps
	{
		private string url = "/version";
		private TypedResponse response;

		[When(@"I check the API version")]
		public void WhenICheckTheAPIVersion()
		{
			response = Locator.HttpHelper.Get(url, token: null);
		}

		[Then(@"I should see a valid response with the version number")]
		public void ThenIShouldSeeAValidResponseWithTheVersionNumber()
		{
			response.StatusCode
				.Should().Be(HttpStatusCode.OK);

			response.Content.Should().NotBeNullOrWhiteSpace();
		}
	}
}
