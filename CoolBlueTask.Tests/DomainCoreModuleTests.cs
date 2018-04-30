using Autofac;
using AutoMapper;
using FluentAssertions;
using NLog;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class DomainCoreModuleTests
	{
		private readonly IContainer container;

		public DomainCoreModuleTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new DomainCoreModule());

			container = builder.Build();
		}

		[Fact]
		public void configuration_is_registered_and_singleton()
		{
			// Act
			var configuration1 = container.Resolve<IApiConfiguration>();
			var configuration2 = container.Resolve<IApiConfiguration>();

			// Assert
			configuration1.Should().BeOfType<ApiConfiguration>();
			configuration2.Should().Be(configuration1);
		}

		[Fact]
		public void mapper_is_registered()
		{
			// Act
			var actual = container.Resolve<IMapper>();

			// Assert
			actual.Should().NotBeNull();
		}

		[Fact]
		public void logger_is_registered()
		{
			// Act
			var actual = container.Resolve<ILogger>();

			// Assert
			actual.Should().NotBeNull();
		}
	}
}
