
using Autofac;
using CoolBlueTask.SalesCombinations;

namespace CoolBlueTask.Products
{
    public class ProductModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .WithParameter("connectionString", @"C:\temp\db.sqlite")
                .SingleInstance();

            builder.RegisterType<SaleasCombinationRepository>()
                .As<ISaleasCombinationRepository>()
                .WithParameter("connectionString", @"C:\temp\db.sqlite")
                .SingleInstance();

            builder.RegisterType<ProductService>()
                .As<IProductService>();

            builder.RegisterType<SalesCombinationService>()
                .As<ISalesCombinationService>();

            builder.RegisterType<ProductController>()
                .AsSelf();
        }
    }
}