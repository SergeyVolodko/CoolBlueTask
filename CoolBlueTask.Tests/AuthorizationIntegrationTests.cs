using System;
using System.Net;
using System.Net.Http;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Owin.Hosting;
using Owin;
using Xunit;


/// <summary>
/// Microsoft.Owin.Host.HttpListener; has to be included into a project
/// </summary>
namespace CoolBlueTask.Tests
{
	public class AuthorizationIntegrationTests
	{
		private readonly string baseAddr = "http://localhost:7777";
		private readonly Action<IAppBuilder> action;

		public AuthorizationIntegrationTests()
		{
			var startup = new Startup();
			startup.apiConfiguration = new TestApiConfiguration();
			action = new Action<IAppBuilder>(startup.Configuration);
		}

		[Fact]
		public async void empty_token_should_throw_unauthorized()
		{
			// arrange
			var url = baseAddr + "/version/testauth";

			using (var server = WebApp.Start(baseAddr, action))
			using (var client = new HttpClient())
			{
				// act
				var actual = await client.GetAsync(url);

				// assert
				actual.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
			}
		}

		[Fact]
		public async void wrong_token_should_throw_unauthorized()
		{
			// arrange
			var url = baseAddr + "/version/testauth";

			var wrongToken =
				"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ";

			using (var server = WebApp.Start(baseAddr, action))
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + wrongToken);

				// act
				var actual = await client.GetAsync(url);

				// assert
				actual.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
			}
		}
	}
}
