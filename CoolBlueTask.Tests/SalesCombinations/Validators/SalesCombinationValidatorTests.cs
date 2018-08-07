using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.SalesCombinations.Validators;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Validators
{
	public class SalesCombinationValidatorTests
	{
		private readonly SalesCombinationValidator sut;

		public SalesCombinationValidatorTests()
		{
			sut = new SalesCombinationValidator();
		}

		[Fact]
		public void null_combination_fails()
		{
			// Act
			var actual = sut.Validate((SalesCombination)null);

			// Assert
			actual.IsValid.Should().BeFalse();
		}

		[Theory]
		[AutoData]
		public void missed_main_product_fails(
			SalesCombination combination)
		{
			// Arrange
			combination.MainProduct = null;

			// Act
			var actual = sut.Validate(combination).Errors;

			// Assert
			actual.Should().Contain(e => 
				e.PropertyName == "MainProduct" &&
				e.ErrorCode == "not_existing_main_product" &&
				e.ErrorMessage == "Main product does not exist.");
		}

		[Theory]
		[AutoData]
		public void missed_a_related_product_fails(
			SalesCombination combination)
		{
			// Arrange
			combination.RelatedProducts[0] = null;

			// Act
			var actual = sut.Validate(combination).Errors;

			// Assert
			actual.Should().Contain(e =>
				e.PropertyName == "RelatedProducts" &&
				e.ErrorCode == "not_existing_related_product" &&
				e.ErrorMessage == "A related product does not exist.");
		}

		[Theory]
		[AutoData]
		public void happy_path(SalesCombination combination)
		{
			// Act
			var actual = sut.Validate(combination);

			// Assert
			actual.IsValid
				.Should().BeTrue();
		}
	}
}
