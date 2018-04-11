using AutoMapper;
using CoolBlueTask.Products.Models;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.Products.Models
{
	public class MappingTests
	{

		private readonly ProductMappingProfile sut;
		private readonly IMapper mapper;

		public MappingTests()
		{
			sut = new ProductMappingProfile();
			mapper = new MapperConfiguration(cfg => cfg.AddProfile(sut)).CreateMapper();
		}

		[Theory]
		[AutoData]
		public void product_to_read_dto(Product product)
		{
			// Arrange
			var expected = new ProductReadDto
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price
			};

			// Act
			var actual = mapper.Map<Product, ProductReadDto>(product);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}

		[Theory]
		[AutoData]
		public void write_dto_to_product(ProductWriteDto dto)
		{
			// Arrange
			var expected = new Product
			{
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price
			};

			// Act
			var actual = mapper.Map<ProductWriteDto, Product>(dto);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}
