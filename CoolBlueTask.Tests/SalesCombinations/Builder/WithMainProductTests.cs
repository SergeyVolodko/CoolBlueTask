using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Builder
{
	public class WithMainProductTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_product_repo_load_by_provided_id(
			[Frozen] IProductRepository productRepo,
			SalesCombinationBuilder sut,
			string mainProductId)
		{
			// Arrange
			productRepo.Exists(mainProductId).Returns(true);

			// Act
			sut.WithMainProduct(mainProductId);

			// Assert
			productRepo.Received(1).LoadById(mainProductId);
		}

		[Theory]
		[AutoNSubstituteData]
		public void on_build_assigns_loaded_product_as_combination_main_product(
			[Frozen] IProductRepository productRepo,
			SalesCombinationBuilder sut,
			string mainProductId,
			Product loadedProduct)
		{
			// Arrange
			productRepo.Exists(mainProductId).Returns(true);
			productRepo.LoadById(mainProductId).Returns(loadedProduct);

			// Act
			var actual = sut.WithMainProduct(mainProductId)
				.Build();

			// Assert
			actual.MainProduct
				.Should().Be(loadedProduct);
		}
	}
}
