using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.SalesCombinations.Validators;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations
{
	public class SalesCombinationsModuleTests
	{
		private readonly IContainer container;

		public SalesCombinationsModuleTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance(new TestApiConfiguration()).As<IApiConfiguration>();
			builder.RegisterModule(new DomainCoreModule());
			builder.RegisterModule(new ProductModule());
			builder.RegisterModule(new SalesCombinationsModule());
			container = builder.Build();
		}

		[Fact]
		public void repository_is_registered()
		{
			// Act
			var repo = container.Resolve<ISalesCombinationRepository>();

			// Assert
			repo.Should().BeOfType<SalesCombinationRepository>();
		}

		[Fact]
		public void repository_is_a_singletones()
		{
			// Act
			var repo1 = container.Resolve<ISalesCombinationRepository>();
			var repo2 = container.Resolve<ISalesCombinationRepository>();

			// Assert
			repo1.Should().Be(repo2);
		}

		[Fact]
		public void combination_builder_is_registered()
		{
			// Act
			var builder = container.Resolve<ISalesCombinationBuilder>();

			// Assert
			builder.Should().BeOfType<SalesCombinationBuilder>();
		}

		[Fact]
		public void validators_are_registered()
		{
			// Act
			var inputValidator = container.Resolve<AbstractValidator<SalesCombinationWriteDto>>();
			var combinationValidator = container.Resolve<AbstractValidator<SalesCombination>>();

			// Assert
			inputValidator.Should().BeOfType<SalesCombinationWriteDtoValidator>();
			combinationValidator.Should().BeOfType<SalesCombinationValidator>();
		}

		[Fact]
		public void service_is_registered()
		{
			// Act
			var service = container.Resolve<ISalesCombinationService>();

			// Assert
			service.Should().BeOfType<SalesCombinationService>();
		}

		[Fact]
		public void controller_is_registered()
		{
			// Act
			// Assert
			container.Resolve<SalesCombinationController>()
				.Should()
				.NotBeNull();
		}
	}
}
