using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.Products.Service
{
	public class UpdateProductTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_dto_to_product(
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			string id,
			ProductWriteDto dto,
			Product product)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			// Act
			sut.UpdateProduct(id, dto);

			// Assert
			mapper.Received()
				.Map<ProductWriteDto, Product>(dto);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_validator_validate_mapped_product(
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			string id,
			Product product)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			// Act
			sut.UpdateProduct(id, dto);

			// Assert
			validator.Received().Validate(product);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_product_is_invalid_throws(
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			string id,
			Product product,
			List<ValidationFailure> errors)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult(errors));

			// Act // Assert
			sut.Invoking(s => s.UpdateProduct(id, dto))
				.ShouldThrow<ValidationException>()
				.Which.Errors
				.ShouldAllBeEquivalentTo(errors);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_repository_save_product(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			string id,
			Product product,
			List<ValidationFailure> errors)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());
			
			// Act
			sut.UpdateProduct(id, dto);

			// assert
			productRepo.Received().Update(id, product);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_updated_product_to_dto(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			string id,
			Product product,
			Product updatedProduct)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			productRepo.Update(id, product).Returns(updatedProduct);

			// Act
			sut.UpdateProduct(id, dto);

			// Assert
			mapper.Received()
				.Map<Product, ProductReadDto>(updatedProduct);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			string id,
			Product product,
			Product updatedProduct,
			ProductReadDto expected)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			productRepo.Update(id, product)
				.Returns(updatedProduct);

			mapper.Map<Product, ProductReadDto>(updatedProduct)
				.Returns(expected);

			// Act
			var actual = sut.UpdateProduct(id, dto);

			// Assert
			actual.Should().Be(expected);
		}
	}
}
