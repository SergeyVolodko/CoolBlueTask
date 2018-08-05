using System.Net.Http;
using CoolBlueTask.Tests.Scenarios.Data;

namespace CoolBlueTask.Tests.Scenarios.Infrastructure
{
	public class HttpHelper
	{
		private readonly HttpClient client;
		private readonly string baseAddress;
		private readonly object clientLock = new object();

		public HttpHelper(HttpClient client, string baseAddress)
		{
			this.client = client;
			this.baseAddress = baseAddress;
		}
		private void resetClientHeaders(string token)
		{
			lock (clientLock)
			{
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
				client.DefaultRequestHeaders.Add("Accept", "application/json");
			}
		}

		public string GetJson(
			string endpoint,
			string token)
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var response = client.GetAsync(url).Result;

			return response.Content.ReadAsStringAsync().Result;
		}

		public TypedResponse Get(
			string endpoint,
			string token)
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var response = client.GetAsync(url).Result;

			return new TypedResponse(response);
		}
	}
}
