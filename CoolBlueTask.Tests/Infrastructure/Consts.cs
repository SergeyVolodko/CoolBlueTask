using System;
using System.IO;
using System.Reflection;

namespace CoolBlueTask.Tests.Infrastructure
{
	public static class Consts
	{
		public static string TestDataFolder = Path.Combine(AssemblyDirectory, "test_files");

		private static string AssemblyDirectory
		{
			get
			{
				var codeBase = Assembly.GetAssembly(typeof(Consts)).CodeBase;
				var uri = new UriBuilder(codeBase);
				var path = Uri.UnescapeDataString(uri.Path);

				return Path.GetDirectoryName(path);
			}
		}
	}
}
