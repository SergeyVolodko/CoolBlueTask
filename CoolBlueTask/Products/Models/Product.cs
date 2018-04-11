using System.Runtime.Serialization;

namespace CoolBlueTask.Products.Models
{
	[DataContract]
	public class Product
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		[DataMember(Name = "price")]
		public decimal Price { get; set; }
	}
}