using Newtonsoft.Json;

namespace CoolBlueTask.Products.Models
{
	public class ProductReadDto
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("price")]
		public decimal Price { get; set; }
	}
}