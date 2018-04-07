using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Owin;

namespace CoolBlueTask.Tests
{
	public class StartupIntegrationTests
	{
		private readonly IContainer container;

		public StartupIntegrationTests()
		{
			var startup = new Startup();
			startup.Configuration(Substitute.For<IAppBuilder>());
			container = startup.container;
		}

		public static IEnumerable<object[]> AllControllers = Assembly.GetAssembly(typeof(VersionController))
			.GetTypes().Where(t => t != typeof(ApiController) && typeof(ApiController).IsAssignableFrom(t))
			.Select(c => new object[] { c });

		[Theory]
		[MemberData(nameof(AllControllers))]
		public void controller_can_be_resolved(Type controllerType)
		{
			// arrange
			Trace.WriteLine(controllerType.FullName);

			// act // assert
			container.IsRegistered(controllerType)
				.Should().BeTrue(because: $"{controllerType.Name} failed to resolve");

			container.Resolve(controllerType)
				.Should().NotBeNull();
		}
	}
}
