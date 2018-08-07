using System.Collections.Generic;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;

namespace CoolBlueTask.SalesCombinations
{
	public interface ISalesCombinationBuilder
	{
		ISalesCombinationBuilder WithMainProduct(string mainProductId);
		ISalesCombinationBuilder WithRelatedProducts(IList<string> relatedProductsIds);

		SalesCombination Build();
	}

	public class SalesCombinationBuilder : ISalesCombinationBuilder
	{
		private readonly IProductRepository productRepository;

		private readonly SalesCombination combination;

		public SalesCombinationBuilder(
			IProductRepository productRepository)
		{
			this.productRepository = productRepository;

			combination = new SalesCombination { RelatedProducts = new List<Product>()};
		}

		public ISalesCombinationBuilder WithMainProduct(
			string mainProductId)
		{
			var product = productRepository.LoadById(mainProductId);

			combination.MainProduct = product;

			return this;
		}

		public ISalesCombinationBuilder WithRelatedProducts(
			IList<string> relatedProductsIds)
		{
			foreach (var productsId in relatedProductsIds)
			{
				var product = productRepository.LoadById(productsId);

				combination.RelatedProducts.Add(product);
			}

			return this;
		}

		public SalesCombination Build()
		{
			return combination;
		}
	}
}