using Autofac;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.SalesCombinations.Validators;
using FluentValidation;

namespace CoolBlueTask.SalesCombinations
{
	public class SalesCombinationsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SalesCombinationRepository>()
				.As<ISalesCombinationRepository>()
				.SingleInstance();

			builder.RegisterType<SalesCombinationWriteDtoValidator>()
				.As<AbstractValidator<SalesCombinationWriteDto>>();

			builder.RegisterType<SalesCombinationBuilder>()
				.As<ISalesCombinationBuilder>();

			builder.RegisterType<SalesCombinationService>()
				.As<ISalesCombinationService>();

			builder.RegisterType<SalesCombinationController>()
				.AsSelf();
		}
	}
}