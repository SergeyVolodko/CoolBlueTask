using System.Net.Http;
using System.Text;
using CoolBlueTask.Tests.Scenarios.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

			var json = response.Content.ReadAsStringAsync().Result;

			return formatJson(json);
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

		public TypedResponse PostJson(
			string endpoint,
			string json,
			string token)
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = client.PostAsync(url, content).Result;

			return new TypedResponse(response);
		}

		private string formatJson(string json)
		{
			return JValue.Parse(json).ToString(Formatting.Indented);
		}
	}
}
