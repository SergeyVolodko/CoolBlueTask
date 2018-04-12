using System.Collections.Generic;
using CoolBlueTask.Products.Models;
using Newtonsoft.Json;

namespace CoolBlueTask.SalesCombinations.Models
{
	public class SalesCombinationReadDto
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("main_product")]
		public ProductReadDto MainProduct { get; set; }

		[JsonProperty("related_products")]
		public IList<ProductReadDto> RelatedProducts { get; set; }
	}
}