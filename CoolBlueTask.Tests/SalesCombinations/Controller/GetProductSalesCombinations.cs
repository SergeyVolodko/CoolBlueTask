using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Controller
{
	public class GetProductSalesCombinations
	{
		[Fact]
		public void routing_get_products_sales_combinations()
		{
			// Arrange
			var uri = @"http://localhost:12259/products/42/sales_combinations";
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<SalesCombinationController>();
			route.Action.Should().Be("GetProductSalesCombinations");
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_get_product_sales_combinations(
			[Frozen] ISalesCombinationService service,
			SalesCombinationController sut,
			string productId)
		{
			// Act
			sut.GetProductSalesCombinations(productId);

			// Assert
			service.Received()
				.GetProductSalesCombinations(productId);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_found_combinations(
			[Frozen] ISalesCombinationService service,
			SalesCombinationController sut,
			IList<SalesCombinationReadDto> sales,
			string productId)
		{
			// Arrange
			service
				.GetProductSalesCombinations(productId)
				.Returns(sales);

			// Act
			var actual = sut.GetProductSalesCombinations(productId);

			// Asserts
			actual.ShouldAllBeEquivalentTo(sales);
		}
	}
}
