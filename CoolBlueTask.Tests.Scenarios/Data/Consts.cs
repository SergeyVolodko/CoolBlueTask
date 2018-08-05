using System;
using System.IO;
using System.Reflection;

namespace CoolBlueTask.Tests.Scenarios.Data
{
	public static class Consts
	{
		public static string BaseAddress => "http://localhost:11111";

		public static string JeffsToken => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJteS10ZXN0LWFwaS5ldS5hdXRoMC5jb20iLCJhdWQiOiJRM1hxQTJhWEpqWllJcVo4NlBoc3phdXdJSHpUWjFURSIsInN1YiI6IkplZmYiLCJpYXQiOjE1MjYyNTYwMDAsImV4cCI6MjE0NTgzMDQwMH0.Xsc5beXpQrgdrkV9f_jYTMV83ouxFtl4oTIRocndebg";

		public static string ProductsJsonSamplesFolder => Path.Combine(TestDataFolder, "Products");

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
