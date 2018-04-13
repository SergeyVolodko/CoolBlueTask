using System.Collections.Generic;
using System.Linq;
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
	public class GetAllProductsTests
	{
		[Fact]
		public void routing_get_all_products()
		{
			// Arrange
			var uri = @"http://localhost:4242/products";
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("GetAllProducts");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_get_all_products(
			[Frozen] IProductService service,
			ProductController sut)
		{
			// Act
			sut.GetAllProducts();

			// Assert
			service.Received().GetAll();
		}

		[Theory]
		[ControllerAutoData]
		public void returns_products_from_service(
			[Frozen] IProductService service,
			ProductController sut,
			IList<ProductReadDto> products)
		{
			// Arrange
			service
				.GetAll()
				.Returns(products);

			// Act
			var actual = sut.GetAllProducts().ToList();

			// Assert
			actual.ShouldAllBeEquivalentTo(products);
		}
	}
}
