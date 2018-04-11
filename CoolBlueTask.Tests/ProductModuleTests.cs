using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class ProductModuleTests
	{
		private readonly IContainer container;

		public ProductModuleTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new ProductModule());
			builder.RegisterModule(new DomainCoreModule());
			container = builder.Build();
		}

		[Fact]
		public void repositories_are_registered()
		{
			var productRepo = container.Resolve<IProductRepository>();
			var salesRepo = container.Resolve<ISaleasCombinationRepository>();

			productRepo.Should().BeOfType<ProductRepository>();
			salesRepo.Should().BeOfType<SaleasCombinationRepository>();
		}

		[Fact]
		public void repositories_are_singletones()
		{
			var productRepo1 = container.Resolve<IProductRepository>();
			var productRepo2 = container.Resolve<IProductRepository>();

			var salesRepo1 = container.Resolve<ISaleasCombinationRepository>();
			var salesRepo2 = container.Resolve<ISaleasCombinationRepository>();

			productRepo1.Should().Be(productRepo2);
			salesRepo1.Should().Be(salesRepo2);
		}

		[Fact]
		public void services_are_registered()
		{
			var productService = container.Resolve<IProductService>();
			var salesService = container.Resolve<ISalesCombinationService>();

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
