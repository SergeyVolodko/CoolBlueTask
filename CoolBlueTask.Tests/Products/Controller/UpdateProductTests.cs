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
	public class UpdateProductTests
	{
		[Fact]
		public void routing_get_product_by_id()
		{
			// Arrange
			var uri = @"http://localhost:4242/products/42";
			var request = new HttpRequestMessage(HttpMethod.Put, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("UpdateProduct");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_update_product(
			[Frozen] IProductService service,
			ProductController sut,
			string id,
			ProductWriteDto productToUpdate)
		{
			// Act
			sut.UpdateProduct(id, productToUpdate);

			// Assert
			service.Received().UpdateProduct(id, productToUpdate);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_updated_product_from_service(
			[Frozen] IProductService service,
			ProductController sut,
			string id,
			ProductWriteDto productToUpdate,
			ProductReadDto product)
		{
			// Arrange
			service.UpdateProduct(id, productToUpdate)
				.Returns(product);

			// Act
			var actual = sut.UpdateProduct(id, productToUpdate);

			// Assert
			actual.ShouldBeEquivalentTo(product);
		}
	}
}
