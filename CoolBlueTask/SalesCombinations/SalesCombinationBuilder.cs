using System.Collections.Generic;
using System.Linq;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;
using FluentValidation;
using FluentValidation.Results;

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
			if (!productRepository.Exists(mainProductId))
			{
				var failure = new ValidationFailure("MainProduct", "Main product does not exist.") {ErrorCode = "not_existing_main_product"};
				throw new ValidationException(new List<ValidationFailure> { failure });
			}

			var product = productRepository.LoadById(mainProductId);

			combination.MainProduct = product;

			return this;
		}

		public ISalesCombinationBuilder WithRelatedProducts(
			IList<string> relatedProductsIds)
		{
			if (relatedProductsIds.Any(i => !productRepository.Exists(i)))
			{
				var failure = new ValidationFailure("RelatedProducts", "A related product does not exist.") { ErrorCode = "not_existing_related_product" };
				throw new ValidationException(new List<ValidationFailure> { failure });
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