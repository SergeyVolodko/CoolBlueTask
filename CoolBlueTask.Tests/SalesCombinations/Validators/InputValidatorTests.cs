using System.Collections.Generic;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.SalesCombinations.Validators;
using FluentAssertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Validators
{
	public class InputValidatorTests
	{
		private readonly SalesCombinationWriteDtoValidator sut;

		public InputValidatorTests()
		{
			sut = new SalesCombinationWriteDtoValidator();
		}

		[Fact]
		public void null_input_fails()
		{
			// Act
			var actual = sut.Validate((SalesCombinationWriteDto)null);

			// Assert
			actual.IsValid.Should().BeFalse();
		}

		[Theory]
		[MemberData(nameof(FailingInputValidationCases))]
		public void invalid_input_cases(
			string mainProductId,
			List<string> relatedProducts,
			string expectedPropertyName,
			string expectedErrorCode,
			string expectedMessage)
		{
			// Arrange
			var input = new SalesCombinationWriteDto
			{
				MainProductId = mainProductId,
				RelatedProducts = relatedProducts
			};

			// Act
			var actual = sut.Validate(input).Errors;

			// Assert
			actual.Should().Contain(e => 
				e.PropertyName == expectedPropertyName &&
				e.ErrorCode == expectedErrorCode &&
				e.ErrorMessage == expectedMessage);
		}

		public static IEnumerable<object[]> FailingInputValidationCases()
		{
			var fixture = new Fixture();
			var someMainProduct = fixture.Create<string>();
			var someRelatedProducts = fixture.Create<List<string>>();

			var maxRelatedProducts = 5;
			fixture.RepeatCount = maxRelatedProducts + 1;
			var tooManyRelatedProducts = fixture.Create<List<string>>();

			yield return new object[]
			{
				null, someRelatedProducts, "MainProduct", "notempty_error", "Main product is missing."
			};
			yield return new object[]
			{
				"", someRelatedProducts, "MainProduct", "notempty_error", "Main product is missing."
			};
			yield return new object[]
			{
				" ", someRelatedProducts, "MainProduct", "notempty_error", "Main product is missing."
			};
			yield return new object[]
			{
				someMainProduct, null, "RelatedProducts", "notempty_error", "Related products are missing."
			};
			yield return new object[]
			{
				someMainProduct, null, "RelatedProducts", "notempty_error", "Related products are missing."
			};
			yield return new object[]
			{
				someMainProduct, new List<string>(), "RelatedProducts", "notempty_error", "Related products are missing."
			};
			yield return new object[]
			{
				someMainProduct, tooManyRelatedProducts, "RelatedProducts", "too_many_related_products", "The number of related products should be less than 5."
			};
		}

		[Theory]
		[AutoData]
		public void happy_path(SalesCombinationWriteDto input)
		{
			// Act
			var actual = sut.Validate(input);

			// Assert
			actual.IsValid
				.Should().BeTrue();
		}
	}
}
