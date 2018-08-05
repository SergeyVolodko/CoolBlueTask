using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CoolBlueTask.Tests.Scenarios.Data;
using Flurl;
using Newtonsoft.Json;

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

			return new TypedResponse(response).ContentAsFormattedJson;
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

		public TypedResponse<T> Get<T>(
			string endpoint,
			string token) where T : new()
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var response = client.GetAsync(url).Result;

			return new TypedResponse<T>(response);
		}

		public TypedResponse<T> GetWithQueryParams<T>(
			string endpoint,
			IDictionary<string, string> queryParams,
			string token) where T : new()
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var urlWithQuery = url.SetQueryParams(queryParams);

			var response = client.GetAsync(urlWithQuery).Result;

			return new TypedResponse<T>(response);
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

		public TypedResponse<TResponse> PostObject<TResponse>(
			string endpoint,
			object dto,
			string token) where TResponse : new()
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var json = JsonConvert.SerializeObject(dto);

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = client.PostAsync(url, content).Result;

			return new TypedResponse<TResponse>(response);
		}

		public TypedResponse PutJson(
			string endpoint,
			string json,
			string token)
		{
			resetClientHeaders(token);

			var url = $"{baseAddress}{endpoint}";

			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = client.PutAsync(url, content).Result;

			return new TypedResponse(response);
		}
	}
}
