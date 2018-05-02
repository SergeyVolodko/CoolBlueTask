using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Api.Core;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.Products.Controller
{
	public class CreateProductTests
	{
		[Fact]
		public void routing_create_products()
		{
			// Arrange
			var uri = @"http://localhost:4242/products";
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("CreateProduct");
		}

		[Fact]
		public void has_authorization_attribute()
		{
			// Arrange
			var method = typeof(ProductController)
				.Methods()
				.FirstOrDefault(m => m.Name == "CreateProduct");

			// Act
			var attributes = method.GetCustomAttributes(false);

			// Assert
			attributes.Should()
				.Contain(a => a is Auth0AuthorizationAttribute);
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
			// Arrange
			service
				.CreateProduct(product)
				.Returns(createdProduct);

			// Act
			var actual = sut.CreateProduct(product);

			// Asserts
			actual.ShouldBeEquivalentTo(createdProduct);
		}
	}
}
