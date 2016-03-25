using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class DataStructuresTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void product_product_to_add_convertions(
            ProductToAdd toAdd)
        {
            // act
            var product = (Product) toAdd;
            var actual = (ProductToAdd) product;

            // assert
            actual.ShouldBeEquivalentTo(toAdd);
        }
    }
}
