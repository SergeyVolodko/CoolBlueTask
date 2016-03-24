﻿using System;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.ProductServiceTests
{
    public class AddProduct
    {
        [Theory]
        [AutoNSubstituteData]
        public void if_product_name_is_empty_return_402(
            ProductService sut)
        {
            // setup
            var product = new ProductToAdd();

            // act // assert
            sut.AddProduct(product)
                .Should()
                .Be(ProductResult.NameIsEmpty);
        }

        [Theory]
        [AutoNSubstituteData]
        public void calls_repository_save(
            [Frozen] IProductRepository productRepo,
            ProductService sut,
            ProductToAdd product)
        {
            // arrange
            Product receivedArg = null;
            productRepo.Save(
                Arg.Do<Product>(a => { receivedArg = a; }));

            // act
            sut.AddProduct(product);

            // assert
            productRepo
                .ReceivedWithAnyArgs()
                .Save(null);

            receivedArg
                .ShouldBeEquivalentTo((Product)product);
        }

        [Theory]
        [AutoNSubstituteData]
        public void saving_failed_return_500(
            [Frozen] IProductRepository productRepo,
            ProductService sut,
            ProductToAdd product,
            Exception ex)
        {
            // setup
            productRepo
                .When(r=>r.Save(Arg.Any<Product>()))
                .Do(x=> { throw ex; });

            // act
            var actual = sut.AddProduct(product);

            // assert
            actual
                .ShouldBeEquivalentTo(ProductResult.Failed);
        }

        [Theory]
        [AutoNSubstituteData]
        public void happy_path(
            ProductService sut,
            ProductToAdd product)
        {
            // act // assert
            sut.AddProduct(product)
                .Should()
                .Be(ProductResult.Ok);
        }
    }
}