using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoolBlueTask.SalesCombinations.Models
{
	public class SalesCombinationWriteDto
	{
		[JsonProperty("main_product_id")]
		public string MainProductId { get; set; }

		[JsonProperty("related_products")]
		public IList<string> RelatedProducts { get; set; }
	}
}