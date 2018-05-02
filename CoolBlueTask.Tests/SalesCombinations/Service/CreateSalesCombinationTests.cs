using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
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
		public void calls_builder_build_new_combination(
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto)
		{
			// Arrange
			MockBuilder(builder);

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			Received.InOrder(() =>
			{
				builder.CreateForProduct(combinationDto.MainProductId);
				builder.WithRelatedProducts(combinationDto.RelatedProducts);
				builder.Build();
			});
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_combination_repo_save_built_combination(
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination)
		{
			// Arrange
			MockBuilder(builder);
			builder.Build().Returns(newCombination);

			// Act
			sut.CreateSalesCombination(combinationDto);

			// Assert
			repo.Received(1).Save(newCombination);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_created_combination_to_readdto(
			[Frozen] ISalesCombinationRepository repo,
			[Frozen] ISalesCombinationBuilder builder,
			[Frozen] IMapper mapper,
			SalesCombinationService sut,
			SalesCombinationWriteDto combinationDto,
			SalesCombination newCombination,
			SalesCombination createdCombination)
		{
			// Arrange
			MockBuilder(builder);
			builder.Build().Returns(newCombination);
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
			MockBuilder(builder);
			builder.Build().Returns(newCombination);
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
			builder.CreateForProduct(Arg.Any<string>())
				.Returns(builder);
			builder.WithRelatedProducts(Arg.Any<IList<string>>())
				.Returns(builder);
		}
	}
}
