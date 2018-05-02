using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Owin.Builder;
using NSubstitute;
using Xunit;
using Owin;

namespace CoolBlueTask.Tests
{
	public class StartupIntegrationTests
	{
		private readonly IContainer container;
		private readonly Startup startup;

		/// <summary>
		/// To enable Startup testing
		/// Remember to add Microsoft.Owin.Hosting Nuget
		/// </summary>
		public StartupIntegrationTests()
		{
			startup = new Startup {apiConfiguration = new TestApiConfiguration()};
			startup.Configuration(Substitute.For<IAppBuilder>());
			container = startup.container;
		}

		public static IEnumerable<object[]> AllControllers = Assembly.GetAssembly(typeof(VersionController))
			.GetTypes().Where(t => t != typeof(ApiController) && typeof(ApiController).IsAssignableFrom(t))
			.Select(c => new object[] { c });

		[Theory]
		[MemberData(nameof(AllControllers))]
		public void controller_can_be_resolved(
			Type controllerType)
		{
			// Arrange
			Trace.WriteLine(controllerType.FullName);

			// Act // Assert
			container.IsRegistered(controllerType)
				.Should().BeTrue(because: $"{controllerType.Name} is not registered");

			container.Resolve(controllerType)
				.Should().NotBeNull();
		}

		[Fact]
		public void api_exception_filter_registered()
		{
			// Arrange
			using (var scope = startup.container.BeginLifetimeScope("AutofacWebRequest"))
			{
				// Act
				var actual = scope.Resolve<IAutofacExceptionFilter>();

				// Assert
				actual.Should().NotBeNull();
			}
		}
	}
}
