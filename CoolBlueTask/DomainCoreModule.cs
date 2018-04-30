using Autofac;
using AutoMapper;
using NLog;

namespace CoolBlueTask
{
	public class DomainCoreModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance(new ApiConfiguration()).As<IApiConfiguration>()
				.SingleInstance();

			// Dto-Entity Mapper
			// Scan this assembly and add all mapping profiles to the configuration:
			// Executes constructors of each class that populates from AutoMapper.Profile class
			var mapperInstance = new MapperConfiguration(cfg => cfg.AddProfiles(this.GetType().Assembly)).CreateMapper();
			mapperInstance.ConfigurationProvider.AssertConfigurationIsValid();
			builder.RegisterInstance(mapperInstance).As<IMapper>();

			// Logger
			var logger = ApiLoggerFactory.CreateFileLogger();
			builder.RegisterInstance(logger).As<ILogger>();
		}
	}
}