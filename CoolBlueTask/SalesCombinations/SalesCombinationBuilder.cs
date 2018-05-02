using System;
using System.Collections.Generic;
using CoolBlueTask.Products;
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

			combination = new SalesCombination();
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

		public ISalesCombinationBuilder WithRelatedProducts(IList<string> relatedProductsIds)
		{
			throw new NotImplementedException();
		}

		public SalesCombination Build()
		{
			return combination;
		}
	}
}