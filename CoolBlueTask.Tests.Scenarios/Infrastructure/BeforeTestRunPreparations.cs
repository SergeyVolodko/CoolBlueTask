using System;
using System.Net.Http;
using CoolBlueTask.Tests.Scenarios.Data;
using Microsoft.Owin.Hosting;
using Owin;
using TechTalk.SpecFlow;

namespace CoolBlueTask.Tests.Scenarios.Infrastructure
{
	[Binding]
	public class BeforeTestRunPreparations
	{
		[BeforeTestRun]
		public static void InitializeRun()
		{
			var startup = new Startup { apiConfiguration = new ScenariosApiConfiguration() };
			Action<IAppBuilder> action = startup.Configuration;
			var baseAddress = Consts.BaseAddress;
			var server = WebApp.Start(baseAddress, action);

			var client = new HttpClient();

			Locator.server = server;
			Locator.HttpHelper = new HttpHelper(client, baseAddress);
		}
	}

	public static class Locator
	{
		public static IDisposable server;
		public static HttpHelper HttpHelper;
	}
}
