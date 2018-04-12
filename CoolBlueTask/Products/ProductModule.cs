
using Autofac;
using CoolBlueTask.Products.Models;
using FluentValidation;

namespace CoolBlueTask.Products
{
	public class ProductModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ProductRepository>()
				.As<IProductRepository>()
				.WithParameter("connectionString", @"C:\temp\db.sqlite")
				.SingleInstance();

			builder.RegisterType<ProductValidator>()
				.As<AbstractValidator<Product>>();

			builder.RegisterType<ProductService>()
				.As<IProductService>();

			builder.RegisterType<ProductController>()
				.AsSelf();
		}
	}
}