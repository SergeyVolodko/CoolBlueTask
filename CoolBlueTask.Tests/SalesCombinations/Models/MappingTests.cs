using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Models
{
	public class MappingTests
	{
		private readonly SalesCombinationMappingProfile sut;
		private readonly IMapper mapper;

		public MappingTests()
		{
			sut = new SalesCombinationMappingProfile();
			mapper = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile(sut);
					cfg.AddProfile<ProductMappingProfile>();
				})
				.CreateMapper();
		}

		[Theory]
		[AutoData]
		public void combination_to_read_dto(SalesCombination combination)
		{
			// Arrange
			var expected = new SalesCombinationReadDto
			{
				Id = combination.Id,
				MainProduct = mapper.Map<ProductReadDto>(combination.MainProduct),
				RelatedProducts = mapper.Map<IList<ProductReadDto>>(combination.RelatedProducts)
			};

			// Act
			var actual = mapper.Map<SalesCombination, SalesCombinationReadDto>(combination);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}
