using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.Experimental;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Service
{
	public class CreateSalesCombinationTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_input_validator_validate_input(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			combinationValidator.Validate(Arg.Any<SalesCombination>())
				.Returns(new ValidationResult());
			MockBuilder(builder);

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			inputValidator.Received(1).Validate(combinationDto);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_input_is_invalid_throws(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			List<ValidationFailure> errors)
		{
			// Arrange
			inputValidator.Validate(combinationDto)
				.Returns(new ValidationResult(errors));

			// Act // Assert
			sut.Invoking(s => s.CreateSalesCombination(combinationDto))
				.ShouldThrow<ValidationException>()
				.Which.Errors
				.ShouldAllBeEquivalentTo(errors);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_builder_build_new_combination(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			combinationValidator.Validate(Arg.Any<SalesCombination>())
				.Returns(new ValidationResult());
			MockBuilder(builder);

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			Received.InOrder(() =>
			{
				builder.WithMainProduct(combinationDto.MainProductId);
				builder.WithRelatedProducts(combinationDto.RelatedProducts);
				builder.Build();
			});
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_combination_validator_validate_built_combination(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			MockBuilder(builder);
			builder.Build().Returns(newCombination);

			combinationValidator.Validate(newCombination)
				.Returns(new ValidationResult());

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			repo.Received(1).Save(newCombination);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_built_combination_is_invalid_throws(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination,
			List<ValidationFailure> errors)
		{
			// Arrange
			inputValidator.Validate(combinationDto)
				.Returns(new ValidationResult());
			MockBuilder(builder);
			builder.Build().Returns(newCombination);

			combinationValidator.Validate(newCombination)
				.Returns(new ValidationResult(errors));

			// Act // Assert
			sut.Invoking(s => s.CreateSalesCombination(combinationDto))
				.ShouldThrow<ValidationException>()
				.Which.Errors
				.ShouldAllBeEquivalentTo(errors);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_combination_repo_save_built_combination(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			MockBuilder(builder);
			builder.Build().Returns(newCombination);

			combinationValidator.Validate(newCombination).Returns(new ValidationResult());

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			repo.Received(1).Save(newCombination);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_created_combination_to_readdto(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			[Frozen] IMapper mapper,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination,
			SalesCombination createdCombination)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			MockBuilder(builder);
			builder.Build().Returns(newCombination);
			combinationValidator.Validate(newCombination).Returns(new ValidationResult());
			repo.Save(newCombination).Returns(createdCombination);

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			mapper.Received(1)
				.Map<SalesCombination, SalesCombinationReadDto>(createdCombination);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] AbstractValidator<SalesCombinationWriteDto> inputValidator,
			[Frozen] AbstractValidator<SalesCombination> combinationValidator,
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			[Frozen] IMapper mapper,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination,
			SalesCombination createdCombination,
			SalesCombinationReadDto expected)
		{
			// Arrange
			inputValidator.Validate(combinationDto).Returns(new ValidationResult());
			MockBuilder(builder);
			builder.Build().Returns(newCombination);
			combinationValidator.Validate(newCombination).Returns(new ValidationResult());
			repo.Save(newCombination).Returns(createdCombination);
			mapper.Map<SalesCombination, SalesCombinationReadDto>(createdCombination)
				.Returns(expected);

			// Act
			var actual = sut.CreateSalesCombination(combinationDto);

			// Assert
			actual.Should().Be(expected);
		}


		private void MockBuilder(ISalesCombinationBuilder builder)
		{
			builder.WithMainProduct(Arg.Any<string>())
				.Returns(builder);
			builder.WithRelatedProducts(Arg.Any<IList<string>>())
				.Returns(builder);
		}
	}
}
