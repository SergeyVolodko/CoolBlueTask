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
    public class AddProduct
    {
        [Fact]
        public void routing()
        {
            // arrange
            var uri = @"http://localhost:12259/product";
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var config = new HttpConfiguration();

            // act
            WebApiConfig.Register(config);
            var route = WebApi.RouteRequest(config, request);

            // asserts
            route.Controller.Should().Be<ProductController>();
            route.Action.Should().Be("AddProduct");
        }

        [Theory]
        [ControllerAutoData]
        public void calls_service_add_product(
            [Frozen] IProductService service,
            ProductController sut,
            ProductToAdd product)
        {
            // act
            sut.AddProduct(product);

            // assert
            service.Received().AddProduct(product);
        }

        [Theory]
        [ControllerAutoData]
        public void returns_service_result_as_code(
            [Frozen] IProductService service,
            ProductController sut,
            ProductToAdd product,
            ProductResult result)
        {
            // setup
            service
                .AddProduct(product)
                .Returns(result);

            // act
            var actual = sut.AddProduct(product);

            // asserts
            actual
                .ShouldBeEquivalentTo((int)result);
        }

    }
}
