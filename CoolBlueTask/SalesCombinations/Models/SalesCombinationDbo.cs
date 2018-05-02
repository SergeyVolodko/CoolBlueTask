using System.Runtime.Serialization;

namespace CoolBlueTask.SalesCombinations.Models
{
	[DataContract]
	public class SalesCombinationDbo
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "main_product_id")]
		public string MainProductId { get; set; }

		[DataMember(Name = "related_products")]
		public string RelatedProducts { get; set; }
	}
}