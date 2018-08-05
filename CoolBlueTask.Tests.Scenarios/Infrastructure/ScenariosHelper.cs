using CoolBlueTask.Tests.Scenarios.Data;
using Newtonsoft.Json;

namespace CoolBlueTask.Tests.Scenarios.Infrastructure
{
	public static class ScenariosHelper
	{
		public static string GetIdFromResponse(TypedResponse response)
		{
			return ((dynamic)JsonConvert.DeserializeObject(response.Content)).id;
		}
	}
}
