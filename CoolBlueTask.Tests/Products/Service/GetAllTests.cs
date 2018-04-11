using System.Collections.Generic;
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
	public class GetAllTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_repository_load_all(
			[Frozen] IProductRepository productRepo,
			ProductService sut)
		{
			// Act
			sut.GetAll();

			// Assert
			productRepo.Received().LoadAll();
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_fetched_products(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			ProductService sut,
			IList<Product> products)
		{
			// Arrange
			productRepo
				.LoadAll().Returns(products);

			// Act
			sut.GetAll();

			// Assert
			mapper.Received()
				.Map<IList<Product>, IList<ProductReadDto>>(products);
		}

		[Theory]
		[AutoNSubstituteData]
		public void happy_path(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			ProductService sut,
			IList<Product> products,
			IList<ProductReadDto> expected)
		{
			// Arrange
			productRepo.LoadAll().Returns(products);

			mapper.Map<IList<Product>, IList<ProductReadDto>>(products)
				.Returns(expected);

			// Act
			var actual = sut.GetAll();

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}
