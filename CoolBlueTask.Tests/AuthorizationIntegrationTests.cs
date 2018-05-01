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
		private readonly IApiConfiguration configuration;

		public AuthorizationIntegrationTests()
		{
			configuration = new TestApiConfiguration();

			var startup = new Startup();
			startup.apiConfiguration = configuration;
			action = new Action<IAppBuilder>(startup.Configuration);
		}

		[Fact]
		public async void empty_token_should_throw_unauthorized()
		{
			// Arrange
			var url = baseAddr + "/version/testauth";

			using (var server = WebApp.Start(baseAddr, action))
			using (var client = new HttpClient())
			{
				// Act
				var actual = await client.GetAsync(url);

				// Assert
				actual.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
			}
		}

		[Fact]
		public async void wrong_token_should_throw_unauthorized()
		{
			// Arrange
			var url = baseAddr + "/version/testauth";

			var wrongToken =
				"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

			using (var server = WebApp.Start(baseAddr, action))
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + wrongToken);

				// Act
				var actual = await client.GetAsync(url);

				// Assert
				actual.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
			}
		}

		//[Fact]
		//public async void authorization_happy_path()
		//{
		//	// arrange
		//	var url = baseAddr + "/version/testtenant";

		//	var token = JwtTokenUtils.GenerateToken(configuration, "Some User");

		//	using (var server = WebApp.Start(baseAddr, action))
		//	using (var client = new HttpClient())
		//	{
		//		client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

		//		// Act
		//		var actual = await client.GetAsync(url);

		//		// Assert
		//		actual.StatusCode.Should().Be(HttpStatusCode.OK);
		//	}
		//}
	}
}
