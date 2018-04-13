using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.Products.Controller
{
	public class GetProductsByIdTests
	{
		[Fact]
		public void routing_get_product_by_id()
		{
			// Arrange
			var uri = @"http://localhost:4242/products/42";
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("GetProduct");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_get_product_by_id(
			[Frozen] IProductService service,
			ProductController sut,
			string id)
		{
			// Act
			sut.GetProduct(id);

			// Assert
			service.Received().GetProductById(id);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_product_from_service(
			[Frozen] IProductService service,
			ProductController sut,
			ProductReadDto product,
			string id)
		{
			// Arrange
			service.GetProductById(id)
				.Returns(product);

			// Act
			var actual = sut.GetProduct(id);

			// Assert
			actual.ShouldBeEquivalentTo(product);
		}
	}
}
