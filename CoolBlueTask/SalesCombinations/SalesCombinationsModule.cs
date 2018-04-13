using Autofac;

namespace CoolBlueTask.SalesCombinations
{
	public class SalesCombinationsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			builder.RegisterType<SalesCombinationRepository>()
				.As<ISalesCombinationRepository>()
				.WithParameter("connectionString", @"C:\temp\db.sqlite")
				.SingleInstance();

			//builder.RegisterType<ProductValidator>()
			//	.As<AbstractValidator<Product>>();

			builder.RegisterType<SalesCombinationService>()
				.As<ISalesCombinationService>();

			builder.RegisterType<SalesCombinationController>()
				.AsSelf();
		}
	}
}