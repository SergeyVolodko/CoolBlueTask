using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CoolBlueTask.Tests.Products
{
	public class ProductValidatorTests
	{
		private readonly ProductValidator sut;

		public ProductValidatorTests()
		{
			sut = new ProductValidator();
		}

		[Fact]
		public void null_product_fails()
		{
			// Act
			var actual = sut.Validate((Product)null);

			// Assert
			actual.IsValid.Should().BeFalse();
		}

		[Fact]
		public void empty_name_fails()
		{
			// Act
			// Assert
			sut.ShouldHaveValidationErrorFor(p => p.Name, (string)null);
		}

		[Theory]
		[InlineData(-0.1)]
		[InlineData(-42)]
		public void negative_price_fails(
			decimal price)
		{
			// Act
			// Assert
			sut.ShouldHaveValidationErrorFor(p => p.Price, price);
		}

		[Fact]
		public void happy_path()
		{
			// Arrange
			var product = new Product
			{
				Name = "Not Empty",
				Price = 0.0m
			};

			// Act
			var actual = sut.Validate(product);

			// Assert
			actual.IsValid.Should().BeTrue();
		}
	}
}
