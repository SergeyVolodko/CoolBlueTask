using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
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
			builder.RegisterInstance(new TestApiConfiguration()).As<IApiConfiguration>();
			builder.RegisterModule(new DomainCoreModule());
			builder.RegisterModule(new ProductModule());
			container = builder.Build();
		}

		[Fact]
		public void repository_is_registered()
		{
			// Act
			var productRepo = container.Resolve<IProductRepository>();

			// Assert
			productRepo.Should().BeOfType<ProductRepository>();
		}

		[Fact]
		public void repository_is_singletones()
		{
			// Act
			var productRepo1 = container.Resolve<IProductRepository>();
			var productRepo2 = container.Resolve<IProductRepository>();

			// Assert
			productRepo1.Should().Be(productRepo2);
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
		public void service_is_registered()
		{
			// Act
			var productService = container.Resolve<IProductService>();

			// Assert
			productService.Should().BeOfType<ProductService>();
		}

		[Fact]
		public void controller_is_registered()
		{
			// Act
			// Assert
			container.Resolve<ProductController>()
				.Should()
				.NotBeNull();
		}
	}
}
