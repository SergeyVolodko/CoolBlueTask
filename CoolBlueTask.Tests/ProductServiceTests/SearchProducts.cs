using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.ProductServiceTests
{
    public class SearchProducts
    {
        [Theory]
        [AutoNSubstituteData]
        public void calls_repository_load_by_name_or_description(
            [Frozen] IProductRepository productRepo,
            ProductService sut,
            string searchText)
        {
            // act
            sut.SearchProducts(searchText);

            // assert
            productRepo
                .Received()
                .LoadByNameOrDescription(searchText);
        }

        [Theory]
        [AutoNSubstituteData]
        public void returns_all_found_products(
            [Frozen] IProductRepository productRepo,
            ProductService sut,
            List<Product> products,
            string searchText)
        {
            // setup
            productRepo
                .LoadByNameOrDescription(searchText).Returns(products);

            // act
            var actual = sut.SearchProducts(searchText);

            // assert
            actual
                .ShouldBeEquivalentTo(products);
        }
    }
}
