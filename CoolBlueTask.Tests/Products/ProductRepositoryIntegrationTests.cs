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
		private readonly ProductRepository sut;

		public ProductRepositoryIntegrationTests()
		{
			var adapter = new InMemoryAdapter();
			Database.UseMockAdapter(adapter);
			sut = new ProductRepository(new TestApiConfiguration());
		}

		[Theory]
		[AutoNSubstituteData]
		public void save_load_all_integration(
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

		[Theory]
		[AutoNSubstituteData]
		public void update_load_by_id_integration(
			Product product1,
			Product product2)
		{
			// Arrange
			var id1 = sut.Save(product1).Id;
			var expected = product2;
			expected.Id = id1;

			// Act
			sut.Update(id1, product2);
			var actual = sut.LoadById(id1);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}

		[Theory]
		[AutoNSubstituteData]
		public void save_exist_integration(
			Product newProduct)
		{
			// Arrange
			var productId = sut.Save(newProduct).Id;

			// Act
			// Assert
			sut.Exists(productId)
				.Should()
				.BeTrue();
		}
	}
}
