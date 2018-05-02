using System.Collections.Generic;
using System.Linq;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;
using FluentValidation;

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
			if (string.IsNullOrWhiteSpace(mainProductId) ||
				!productRepository.Exists(mainProductId))
			{
				throw new ValidationException("Main product is missing.");
			}

			var product = productRepository.LoadById(mainProductId);

			combination.MainProduct = product;

			return this;
		}

		public ISalesCombinationBuilder WithRelatedProducts(
			IList<string> relatedProductsIds)
		{
			if (relatedProductsIds.Count == 0)
			{
				throw new ValidationException("Related products can't be empty.");
			}
			if (relatedProductsIds.Any(string.IsNullOrWhiteSpace))
			{
				throw new ValidationException("A related product id is missing.");
			}
			if (relatedProductsIds.Any(i => !productRepository.Exists(i)))
			{
				throw new ValidationException("A related product does not exist");
			}

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