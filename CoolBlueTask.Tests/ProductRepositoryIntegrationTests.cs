using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class ProductRepositoryIntegrationTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void add_load_all_integration(
            ProductRepository sut,
            Product product1,
            Product product2)
        {
            // arrange
            var expected = new List<Product>
                            { product1, product2 };

            // act 
            sut.Save(product1);
            sut.Save(product2);
            var actual = sut.LoadAll();

            // assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
