using System;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class SalesRepositoryIntegrationTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void add_load_by_product_id_integration(
            SaleasCombinationRepository sut,
            SalesCombination comb1,
            SalesCombination comb2,
            SalesCombination comb3,
            Guid productId)
        {
            // setup
            comb1.Products.Add(productId);
            comb3.Products.Add(productId);

            // act
            sut.Save(comb1);
            sut.Save(comb2);
            sut.Save(comb3);
            var actual = sut.LoadByProduct(productId);

            // assert
            actual.Count.Should().Be(2);

        }
    }
}
