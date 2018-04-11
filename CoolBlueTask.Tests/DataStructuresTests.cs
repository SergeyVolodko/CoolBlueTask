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
            ProductWriteDto writeDto)
        {
            // act
            var product = (Product) writeDto;
            var actual = (ProductWriteDto) product;

            // assert
            actual.ShouldBeEquivalentTo(writeDto);
        }
    }
}
