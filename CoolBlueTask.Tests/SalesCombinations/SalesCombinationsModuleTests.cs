using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoolBlueTask.Tests.Products
{
	public class SalesCombinationsModuleTests
	{
		private readonly IContainer container;

		public SalesCombinationsModuleTests()
		{
			var builder = new ContainerBuilder();
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

		//[Fact]
		//public void validator_is_registered()
		//{
		//	// Act
		//	var validator = container.Resolve<AbstractValidator<SalesCombination>>();

		//	// Assert
		//	//validator.Should().BeOfType<ProductValidator>();
		//}

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
			container.Resolve<SalesCombinationController>()
				.Should()
				.NotBeNull();
		}
	}
}
