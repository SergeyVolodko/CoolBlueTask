using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
    public class ProductModuleTests
    {
        private IContainer container;

        public ProductModuleTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ProductModule());
            container = builder.Build();
        }

        [Fact]
        public void repository_registered()
        {
            var repository1 = container.Resolve<IProductRepository>();
            var repository2 = container.Resolve<IProductRepository>();

            repository1.Should().BeOfType<ProductRepository>();
            repository2.Should().BeOfType<ProductRepository>();

            // repo is singletone
            repository1
                .Should()
                .Be(repository2);
        }

        [Fact]
        public void services_registered()
        {
            var productService = container.Resolve<IProductService>();
            var salesService = container.Resolve<ISalesCombinationService>();
            
            productService.Should().BeOfType<ProductService>();
            salesService.Should().BeOfType<SalesCombinationService>();
        }

        [Fact]
        public void controller_registered()
        {
            container.Resolve<ProductController>()
                .Should()
                .NotBeNull();
        }
    }
}
