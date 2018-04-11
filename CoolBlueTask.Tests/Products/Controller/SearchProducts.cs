using System.Collections.Generic;
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
	public class SearchProducts
	{
		[Fact]
		public void routing_search_products()
		{
			// Arrange
			var uri = @"http://localhost:12259/products/search-text-input";
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<ProductController>();
			route.Action.Should().Be("SearchProducts");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_search_products(
			[Frozen] IProductService service,
			ProductController sut,
			string searchText)
		{
			// Act
			sut.SearchProducts(searchText);

			// Assert
			service.Received().SearchByText(searchText);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_all_found_products(
			[Frozen] IProductService service,
			ProductController sut,
			IList<ProductReadDto> products,
			string searchText)
		{
			// setup
			service
				.SearchByText(searchText)
				.Returns(products);

			// Act
			var actual = sut.SearchProducts(searchText);

			// Asserts
			actual.ShouldAllBeEquivalentTo(products);
		}
	}
}
