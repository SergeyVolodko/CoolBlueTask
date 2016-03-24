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

namespace CoolBlueTask.Tests.ProductsControllerTests
{
    public class Search
    {
        [Fact]
        public void routing()
        {
            // setup
            var uri = @"http://localhost:12259/product/searchText";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<ProductController>();
            route.Action.Should().Be("Search");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_service_search_products(
            [Frozen] IProductService service,
            ProductController sut,
            string searchText)
        {
            // act
            sut.Search(searchText);

            // assert
            service.Received().SearchProducts(searchText);
        }

        [Theory]
        [ControllerAutoData]
        public void returns_all_found_products(
            [Frozen] IProductService service,
            ProductController sut,
            IList<Product> products,
            string searchText)
        {
            // setup
            service
                .SearchProducts(searchText)
                .Returns(products);

            // act
            var actual = sut.Search(searchText);

            // asserts
            actual
                .ShouldAllBeEquivalentTo(products);
        }

    }
}
