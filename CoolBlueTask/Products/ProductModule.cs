
using Autofac;

namespace CoolBlueTask.Products
{
    public class ProductModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .SingleInstance();

            builder.RegisterType<ProductService>()
                .As<IProductService>();

            builder.RegisterType<ProductController>()
                .AsSelf();
        }
    }
}