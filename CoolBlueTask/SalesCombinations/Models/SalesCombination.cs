using System.Collections.Generic;
using System.Runtime.Serialization;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.SalesCombinations.Models
{
	[DataContract]
	public class SalesCombination
	{
		public string Id { get; set; }

		public Product MainProduct { get; set; }

		public IList<Product> RelatedProducts { get; set; }
	}
}