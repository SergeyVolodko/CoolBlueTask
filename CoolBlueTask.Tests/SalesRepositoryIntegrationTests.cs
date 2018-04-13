using System;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Simple.Data;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class SalesRepositoryIntegrationTests
    {
        public SalesRepositoryIntegrationTests()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        //[Theory]
        //[AutoNSubstituteData]
        //public void add_load_by_product_id_integration(
        //    SalesCombinationRepository sut,
        //    SalesCombination comb1,
        //    SalesCombination comb2,
        //    SalesCombination comb3,
        //    Guid productId)
        //{
        //    // setup
        //    comb1.Products = $"{comb1.Products}|{productId}|";
        //    comb3.Products = $"{comb3.Products}|{productId}|";

        //    // act
        //    sut.Save(comb1);
        //    sut.Save(comb2);
        //    sut.Save(comb3);
        //    var actual = sut.LoadByProduct(productId);

        //    // assert
        //    actual.Count.Should().Be(2);

        //}
    }
}
