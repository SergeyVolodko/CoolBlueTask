using System.Collections.Generic;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Builder
{
	public class WithMainProductTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void if_main_product_id_is_empty_throws(
			SalesCombinationBuilder sut)
		{
			// Act
			// Assert
			sut.Invoking(s => s.WithMainProduct(null))
				.ShouldThrow<ValidationException>();
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_product_repo_exists_for_provided_id(
			[Frozen] IProductRepository productRepo,
			SalesCombinationBuilder sut,
			string mainProductId)
		{
			// Arrange
			productRepo.Exists(mainProductId).Returns(true);

			// Act
			sut.WithMainProduct(mainProductId);

			// Assert
			productRepo.Received(1).Exists(mainProductId);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_provided_main_product_not_exist_throws(
			SalesCombinationBuilder sut,
			string mainProductId)
		{
			// Arrange
			var failures = new List<ValidationFailure>
			{
				new ValidationFailure("MainProduct", "Main product does not exist.")
				{
					ErrorCode = "not_existing_main_product"
				}
			};
			// Act
			// Assert
			sut.Invoking(s => s.WithMainProduct(mainProductId))
				.ShouldThrow<ValidationException>()
				.And
				.Errors.ShouldBeEquivalentTo(failures);
		}

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
