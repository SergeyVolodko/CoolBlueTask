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
	public class CreateProductTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_dto_to_product(
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			Product product)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			// Act
			sut.CreateProduct(dto);

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
			Product product)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			// Act
			sut.CreateProduct(dto);

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
			Product product,
			List<ValidationFailure> errors)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult(errors));

			// Act // Assert
			sut.Invoking(s => s.CreateProduct(dto))
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
			Product product,
			List<ValidationFailure> errors)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());
			
			// Act
			sut.CreateProduct(dto);

			// assert
			productRepo.Received().Save(product);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_created_product_to_dto(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			Product product,
			Product createdProduct)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			productRepo.Save(product).Returns(createdProduct);

			// Act
			sut.CreateProduct(dto);

			// Assert
			mapper.Received()
				.Map<Product, ProductReadDto>(createdProduct);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			[Frozen] AbstractValidator<Product> validator,
			ProductService sut,
			ProductWriteDto dto,
			Product product,
			Product createdProduct,
			ProductReadDto expected)
		{
			// Arrange
			mapper.Map<ProductWriteDto, Product>(dto)
				.Returns(product);

			validator.Validate(product)
				.Returns(new ValidationResult());

			productRepo.Save(product)
				.Returns(createdProduct);

			mapper.Map<Product, ProductReadDto>(createdProduct)
				.Returns(expected);

			// Act
			var actual = sut.CreateProduct(dto);

			// Assert
			actual.Should().Be(expected);
		}
	}
}
