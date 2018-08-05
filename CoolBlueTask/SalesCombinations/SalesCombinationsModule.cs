using Autofac;

namespace CoolBlueTask.SalesCombinations
{
	public class SalesCombinationsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			builder.RegisterType<SalesCombinationRepository>()
				.As<ISalesCombinationRepository>()
				.SingleInstance();

			builder.RegisterType<SalesCombinationBuilder>()
				.As<ISalesCombinationBuilder>();

			builder.RegisterType<SalesCombinationService>()
				.As<ISalesCombinationService>();

			builder.RegisterType<SalesCombinationController>()
				.AsSelf();
		}
	}
}