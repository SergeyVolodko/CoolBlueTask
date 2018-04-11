using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace CoolBlueTask
{
	public static class ApiLoggerFactory
	{
		public static ILogger CreateFileLogger()
		{
			var loggingConfig = new LoggingConfiguration();

			var fileTarget =
				new FileTarget
				{
					FileName = "${basedir}/${level}.log",
					Layout = new SimpleLayout("#### \r\n${longdate:universalTime=true} ${message}")
				};
			loggingConfig.AddTarget("file", fileTarget);
			var rule = new LoggingRule("*", LogLevel.Trace, fileTarget);
			loggingConfig.LoggingRules.Add(rule);

			LogManager.Configuration = loggingConfig;

			return LogManager.GetCurrentClassLogger();
		}
	}
}