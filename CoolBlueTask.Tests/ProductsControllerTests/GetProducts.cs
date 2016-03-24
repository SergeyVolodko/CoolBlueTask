using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    public class GetProducts
    {
        [Fact]
        public void routing()
        {
            // setup
            var uri = @"http://localhost:12259/product";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<ProductController>();
            route.Action.Should().Be("GetProducts");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_service_get_all_products(
            [Frozen] IProductService service,
            ProductController sut)
        {
            // act
            sut.GetProducts();

            // assert
            service.Received().GetAllProducts();
        }

        [Theory]
        [ControllerAutoData]
        public void returns_all_stored_products(
            [Frozen] IProductService service,
            ProductController sut,
            IList<Product> products)
        {
            // setup
            service
                .GetAllProducts()
                .Returns(products);

            // act
            var actual = sut.GetProducts().ToList();

            // asserts
            actual
                .ShouldAllBeEquivalentTo(products);
        }

    }
}
