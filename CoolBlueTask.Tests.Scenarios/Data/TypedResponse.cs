using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace CoolBlueTask.Tests.Scenarios.Data
{
	public class TypedResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Content { get; set; }

		public string ContentAsFormattedJson {
			get
			{
				return JToken.Parse(Content).ToString(Formatting.Indented);
			}
		}

		public TypedResponse(HttpResponseMessage response)
		{
			StatusCode = response.StatusCode;
			Content = response.Content.ReadAsStringAsync().Result;
		}
	}

	public class TypedResponse<T> : TypedResponse
	{
		public T Data { get; set; }

		public TypedResponse(HttpResponseMessage response) : base(response)
		{
			var serializationSettings = new JsonSerializerSettings
			{
				DateFormatHandling = DateFormatHandling.IsoDateFormat,
				DateParseHandling = DateParseHandling.DateTime,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				Formatting = Formatting.Indented,
				ContractResolver = new DefaultContractResolver {
					NamingStrategy = new SnakeCaseNamingStrategy
					{
						OverrideSpecifiedNames = false,
						ProcessDictionaryKeys = false
					}
				}
			};

			if (response.IsSuccessStatusCode)
			{
				Data = JsonConvert
					.DeserializeObject<T>(base.Content, serializationSettings);
			}
		}
	}
}
