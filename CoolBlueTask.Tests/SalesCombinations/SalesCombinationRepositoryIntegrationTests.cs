using System.Collections.Generic;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Simple.Data;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations
{
	public class SalesCombinationRepositoryIntegrationTests
	{
		private readonly SalesCombinationRepository sut;
		private readonly ProductRepository productRepo;

		public SalesCombinationRepositoryIntegrationTests()
		{
			var config = new TestApiConfiguration();

			var adapter = new InMemoryAdapter();
			Database.UseMockAdapter(adapter);
			sut = new SalesCombinationRepository(config);
			productRepo = new ProductRepository(config);
		}

		[Theory]
		[AutoNSubstituteData]
		public void save_load_by_product_integration(
			Product product1,
			Product product2,
			Product product3)
		{
			// Arrange
			product1 = productRepo.Save(product1);
			product2 = productRepo.Save(product2);
			product3 = productRepo.Save(product3);

			var combination1 = new SalesCombination
			{
				MainProduct = product1,
				RelatedProducts = new List<Product> { product2, product3 }
			};
			var combination2 = new SalesCombination
			{
				MainProduct = product2,
				RelatedProducts = new List<Product> { product1, product3 }
			};
			var combination3 = new SalesCombination
			{
				MainProduct = product1,
				RelatedProducts = new List<Product> { product3 }
			};

			var expected = new List<SalesCombination> {combination1, combination3};

			// Act
			sut.Save(combination1);
			sut.Save(combination2);
			sut.Save(combination3);
			var actual = sut.LoadByProduct(product1.Id);

			// Assert
			actual.ShouldAllBeEquivalentTo(expected);
		}
	}
}
