using System.Collections.Generic;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.ProductServiceTests
{
    public class GetAllProducts
    {
        //[Theory]
        //[AutoNSubstituteData]
        //public void calls_repository_load_all(
        //    [Frozen] IProductRepository productRepo,
        //    ProductService sut)
        //{
        //    // act
        //    sut.GetAllProducts();

        //    // assert
        //    productRepo
        //        .Received()
        //        .LoadAll();
        //}

        //[Theory]
        //[AutoNSubstituteData]
        //public void returns_all_stored_products(
        //    [Frozen] IProductRepository productRepo,
        //    ProductService sut,
        //    List<Product> products)
        //{
        //    // setup
        //    productRepo
        //        .LoadAll().Returns(products);
            
        //    // act
        //    var actual = sut.GetAllProducts();

        //    // assert
        //    actual
        //        .ShouldBeEquivalentTo(products);
        //}
    }
}
