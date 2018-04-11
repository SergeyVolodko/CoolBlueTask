using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.ProductsControllerTests
{
    public class GetProductSalesCombinations
    {
        [Fact]
        public void routing()
        {
            // setup
            var uri = @"http://localhost:12259/products/42/salesCombinations";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<ProductController>();
            route.Action.Should().Be("GetProductSalesCombinations");
        }
        
        [Theory]
        [ControllerAutoData]
        public void calls_service_get_all_combinations(
            [Frozen] ISalesCombinationService service,
            ProductController sut,
            string productId)
        {
            // act
            sut.GetProductSalesCombinations(productId);

            // assert
            service.Received().GetByProduct(productId);
        }
        
        [Theory]
        [ControllerAutoData]
        public void throws_400_if_no_service_retuned_null(
            [Frozen] ISalesCombinationService service,
            ProductController sut,
            string productId)
        {
            // arrange
            service
                .GetByProduct(productId)
                .Returns(x=>null);

            // act // assert
            sut.Invoking(s => s.GetProductSalesCombinations(productId))
                .ShouldThrow<HttpResponseException>()
                .And
                .Response.StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);
        }
        

        [Theory]
        [ControllerAutoData]
        public void returns_found_sales(
            [Frozen] ISalesCombinationService service,
            ProductController sut,
            IList<SalesCombination> sales,
            string productId)
        {
            // arrange
            service
                .GetByProduct(productId)
                .Returns(sales);

            // act
            var actual = sut.GetProductSalesCombinations(productId);

            // asserts
            actual
                .ShouldAllBeEquivalentTo(sales);
        }

    }
}
