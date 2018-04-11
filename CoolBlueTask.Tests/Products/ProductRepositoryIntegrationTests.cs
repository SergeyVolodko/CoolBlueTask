using System.Collections.Generic;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Simple.Data;
using Xunit;

namespace CoolBlueTask.Tests.Products
{
	public class ProductRepositoryIntegrationTests
	{
		public ProductRepositoryIntegrationTests()
		{
			var adapter = new InMemoryAdapter();
			Database.UseMockAdapter(adapter);
		}

		[Theory]
		[AutoNSubstituteData]
		public void add_load_all_integration(
			ProductRepository sut,
			Product product1,
			Product product2)
		{
			// Arrange
			var expected = new List<Product>
							{ product1, product2 };

			// Act 
			sut.Save(product1);
			sut.Save(product2);
			var actual = sut.LoadAll();

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}

		[Theory]
		[AutoNSubstituteData]
		public void search_integration(
			ProductRepository sut,
			Product product1,
			Product product2,
			Product product3,
			string searchText,
			string someDescription)
		{
			// Arrange
			product1.Name = product1.Name + searchText;
			product3.Description = "test" + searchText + someDescription;
			var expected = new List<Product> { product1, product3 };

			// Act
			sut.Save(product1);
			sut.Save(product2);
			sut.Save(product3);
			var actual = sut.LoadByNameOrDescription(searchText);

			// Assert
			actual.Count
				.Should()
				.Be(2);

			actual.ShouldAllBeEquivalentTo(
				expected,
				options => options.Excluding(o => o.Id));
		}
	}
}
