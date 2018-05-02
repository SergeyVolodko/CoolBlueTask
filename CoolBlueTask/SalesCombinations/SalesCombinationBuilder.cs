using System;
using System.Collections.Generic;
using CoolBlueTask.SalesCombinations.Models;

namespace CoolBlueTask.SalesCombinations
{
	public interface ISalesCombinationBuilder
	{
		ISalesCombinationBuilder CreateForProduct(string mainProductId);
		ISalesCombinationBuilder WithRelatedProducts(IList<string> relatedProductsIds);

		SalesCombination Build();
	}

	public class SalesCombinationBuilder : ISalesCombinationBuilder
	{
		public ISalesCombinationBuilder CreateForProduct(string mainProductId)
		{
			throw new NotImplementedException();
		}

		public ISalesCombinationBuilder WithRelatedProducts(IList<string> relatedProductsIds)
		{
			throw new NotImplementedException();
		}


		public SalesCombination Build()
		{
			throw new NotImplementedException();
		}
	}
}