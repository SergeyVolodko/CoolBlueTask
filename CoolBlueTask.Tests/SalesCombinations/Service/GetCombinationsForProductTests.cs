using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Service
{
	public class GetCombinationsForProductTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_product_repository_exists(
			[Frozen] IProductRepository productRepo,
			SalesCombinationService sut,
			string productId)
		{
			// Arrange
			productRepo.Exists(productId).Returns(true);

			// Act
			sut.GetCombinationsForProduct(productId);

			// Assert
			productRepo.Received().Exists(productId);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_product_not_exist_throw(
			[Frozen] IProductRepository productRepo,
			SalesCombinationService sut,
			string productId)
		{
			// Act
			// Assert
			sut.Invoking(s => s.GetCombinationsForProduct(productId))
				.ShouldThrow<EntityNotFoundException>();
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_combination_repository_load_by_product(
			[Frozen] IProductRepository productRepo,
			[Frozen] ISalesCombinationRepository combinationRepo,
			SalesCombinationService sut,
			string productId)
		{
			// Arrange
			productRepo.Exists(productId).Returns(true);

			// Act
			sut.GetCombinationsForProduct(productId);

			// Assert
			combinationRepo.Received()
				.LoadByProduct(productId);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_fetched_combinations(
			[Frozen] IProductRepository productRepo,
			[Frozen] ISalesCombinationRepository combinationRepo,
			[Frozen] IMapper mapper,
			SalesCombinationService sut,
			string productId,
			IList<SalesCombination> combinations)
		{
			// Arrange
			productRepo.Exists(productId).Returns(true);
			combinationRepo
				.LoadByProduct(productId).Returns(combinations);

			// Act
			sut.GetCombinationsForProduct(productId);

			// Assert
			mapper.Received()
				.Map<IList<SalesCombination>, IList<SalesCombinationReadDto>>(combinations);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] IProductRepository productRepo,
			[Frozen] ISalesCombinationRepository combinationRepo,
			[Frozen] IMapper mapper,
			SalesCombinationService sut,
			string productId,
			IList<SalesCombination> combinations,
			IList<SalesCombinationReadDto> expected)
		{
			// Arrange
			productRepo.Exists(productId).Returns(true);
			combinationRepo
				.LoadByProduct(productId).Returns(combinations);

			mapper.Map<IList<SalesCombination>, IList<SalesCombinationReadDto>>(combinations)
				.Returns(expected);

			// Act
			var actual = sut.GetCombinationsForProduct(productId);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}