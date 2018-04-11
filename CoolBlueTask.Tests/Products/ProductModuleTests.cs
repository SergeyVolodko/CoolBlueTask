using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CoolBlueTask.Tests.Products
{
	public class ProductModuleTests
	{
		private readonly IContainer container;

		public ProductModuleTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new DomainCoreModule());
			builder.RegisterModule(new ProductModule());
			container = builder.Build();
		}

		[Fact]
		public void repositories_are_registered()
		{
			// Act
			var productRepo = container.Resolve<IProductRepository>();
			var salesRepo = container.Resolve<ISaleasCombinationRepository>();

			// Assert
			productRepo.Should().BeOfType<ProductRepository>();
			salesRepo.Should().BeOfType<SaleasCombinationRepository>();
		}

		[Fact]
		public void repositories_are_singletones()
		{
			// Act
			var productRepo1 = container.Resolve<IProductRepository>();
			var productRepo2 = container.Resolve<IProductRepository>();

			var salesRepo1 = container.Resolve<ISaleasCombinationRepository>();
			var salesRepo2 = container.Resolve<ISaleasCombinationRepository>();

			// Assert
			productRepo1.Should().Be(productRepo2);
			salesRepo1.Should().Be(salesRepo2);
		}

		[Fact]
		public void validator_is_registered()
		{
			// Act
			var validator = container.Resolve<AbstractValidator<Product>>();

			// Assert
			validator.Should().BeOfType<ProductValidator>();
		}

		[Fact]
		public void services_are_registered()
		{
			// Act
			var productService = container.Resolve<IProductService>();
			var salesService = container.Resolve<ISalesCombinationService>();

			// Assert
			productService.Should().BeOfType<ProductService>();
			salesService.Should().BeOfType<SalesCombinationService>();
		}

		[Fact]
		public void controller_is_registered()
		{
			container.Resolve<ProductController>()
				.Should()
				.NotBeNull();
		}
	}
}
