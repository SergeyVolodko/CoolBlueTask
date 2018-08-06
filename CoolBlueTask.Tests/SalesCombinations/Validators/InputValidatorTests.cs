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
			string expectedErrorCode)
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
			actual.Should().Contain(e => e.PropertyName == expectedPropertyName &&
										 e.ErrorCode == expectedErrorCode);
		}

		public static IEnumerable<object[]> FailingInputValidationCases()
		{
			var fixture = new Fixture();
			var someMainProduct = fixture.Create<string>();
			var someRelatedProducts = fixture.Create<List<string>>();
			yield return new object[]
			{
				null, someRelatedProducts, "MainProduct", "notempty_error"
			};
			yield return new object[]
			{
				"", someRelatedProducts, "MainProduct", "notempty_error"
			};
			yield return new object[]
			{
				" ", someRelatedProducts, "MainProduct", "notempty_error"
			};
			yield return new object[]
			{
				someMainProduct, null , "RelatedProducts", "notnull_error"
			};
			yield return new object[]
			{
				someMainProduct, new List<string>() , "RelatedProducts", "notempty_error"
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
