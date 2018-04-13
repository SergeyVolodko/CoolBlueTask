using AutoMapper;
using CoolBlueTask.Products;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.Products.Service
{
	public class GetProductByIdTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_repository_exist(
			[Frozen] IProductRepository productRepo,
			ProductService sut,
			string id)
		{
			// Arrange
			productRepo.Exists(id).Returns(true);

			// Act
			sut.GetProductById(id);

			// Assert
			productRepo.Received().Exists(id);
		}

		[Theory]
		[AutoNSubstituteData]
		public void if_product_not_exist_throw(
			[Frozen] IProductRepository productRepo,
			ProductService sut,
			string id)
		{
			// Act
			// Assert
			sut.Invoking(s => s.GetProductById(id))
				.ShouldThrow<EntityNotFoundException>();
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_repository_load_by_id(
			[Frozen] IProductRepository productRepo,
			ProductService sut,
			string id)
		{
			// Arrange
			productRepo.Exists(id).Returns(true);

			// Act
			sut.GetProductById(id);

			// Assert
			productRepo.Received().LoadById(id);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_loaded_product(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			ProductService sut,
			string id,
			Product product)
		{
			// Arrange
			productRepo.Exists(id).Returns(true);
			productRepo
				.LoadById(id).Returns(product);

			// Act
			sut.GetProductById(id);

			// Assert
			mapper.Received()
				.Map<Product, ProductReadDto>(product);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			ProductService sut,
			string id,
			Product product,
			ProductReadDto expected)
		{
			// Arrange
			productRepo.Exists(id).Returns(true);
			productRepo
				.LoadById(id).Returns(product);

			mapper.Map<Product, ProductReadDto>(product)
				.Returns(expected);

			// Act
			var actual = sut.GetProductById(id);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}
