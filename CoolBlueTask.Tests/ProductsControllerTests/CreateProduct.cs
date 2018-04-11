using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.ProductsControllerTests
{
	public class CreateProduct
	{
		[Fact]
		public void create_product_routing()
		{
			// Arrange
			var uri = @"http://localhost:12259/product";
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("CreateProduct");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_create_product(
			[Frozen] IProductService service,
			ProductController sut,
			ProductWriteDto product)
		{
			// Act
			sut.CreateProduct(product);

			// Assert
			service.Received().CreateProduct(product);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_created_product_from_service(
			[Frozen] IProductService service,
			ProductController sut,
			ProductWriteDto product,
			ProductReadDto createdProduct)
		{
			//// Arrange
			//service
			//	.CreateProduct(product)
			//	.Returns(createdProduct);

			//// Act
			//var actual = sut.CreateProduct(product);

			//// Asserts
			//actual.ShouldBeEquivalentTo(createdProduct);
		}
	}
}
