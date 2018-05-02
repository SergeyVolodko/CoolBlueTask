using System;
using System.IO;
using System.Reflection;

namespace CoolBlueTask.Tests.Infrastructure
{
	public static class Consts
	{
		public static string ValidToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJteS10ZXN0LWFwaS5ldS5hdXRoMC5jb20iLCJhdWQiOiJRM1hxQTJhWEpqWllJcVo4NlBoc3phdXdJSHpUWjFURSIsInN1YiI6IlNvbWUgVXNlciIsImlhdCI6MTUyNjI1NjAwMCwiZXhwIjoyMTQ1ODMwNDAwfQ.OBAyGVLdBcHU5er6guZlIny5EdcJo0o8lwDLsmgS29E";

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
