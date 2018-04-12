using System;
using System.Collections.Generic;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesServiceTests
{
    public class GetByProduct
    {
        //[Theory]
        //[AutoNSubstituteData]
        //public void calls_repository_load_all(
        //    [Frozen] ISaleasCombinationRepository salesRepo,
        //    SalesCombinationService sut)
        //{
        //    // arrange
        //    var productId = Guid.NewGuid();

        //    // act
        //    sut.GetProductSalesCombinations(productId.ToString());

        //    // assert
        //    salesRepo
        //        .Received()
        //        .LoadByProduct(productId);
        //}

        //[Theory]
        //[AutoNSubstituteData]
        //public void for_wrong_id_format_returns_null(
        //    [Frozen] ISaleasCombinationRepository salesRepo,
        //    SalesCombinationService sut,
        //    string productId)
        //{
        //    // act // assert
        //    sut.GetProductSalesCombinations(productId)
        //        .Should()
        //        .BeNull();
        //}

        //[Theory]
        //[AutoNSubstituteData]
        //public void returns_all_products_sales(
        //    [Frozen] ISaleasCombinationRepository salesRepo,
        //    SalesCombinationService sut,
        //    List<SalesCombination> sales)
        //{
        //    // arrange
        //    var productId = Guid.NewGuid();

        //    salesRepo
        //        .LoadByProduct(productId)
        //        .Returns(sales);

        //    // act
        //    var actual = sut.GetProductSalesCombinations(productId.ToString());

        //    // assert
        //    actual
        //        .ShouldBeEquivalentTo(sales);
        //}
    }
}
