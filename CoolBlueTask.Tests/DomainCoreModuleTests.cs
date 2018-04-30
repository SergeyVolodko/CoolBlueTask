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
