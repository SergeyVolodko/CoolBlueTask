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
	public class SearchByTextTests
	{
		[Theory]
		[AutoNSubstituteData]
		public void calls_repository_load_by_name_or_description(
			[Frozen] IProductRepository productRepo,
			ProductService sut,
			string searchText)
		{
			// Act
			sut.SearchByText(searchText);

			// Assert
			productRepo.Received()
				.LoadByNameOrDescription(searchText);
		}

		[Theory]
		[AutoNSubstituteData]
		public void calls_mapper_map_found_products(
			[Frozen] IProductRepository productRepo,
			[Frozen] IMapper mapper,
			ProductService sut,
			string searchText,
			IList<Product> products)
		{
			// Arrange
			productRepo.LoadByNameOrDescription(searchText)
				.Returns(products);

			// Act
			sut.SearchByText(searchText);

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
			string searchText,
			IList<Product> products,
			IList<ProductReadDto> expected)
		{
			// Arrange
			productRepo.LoadByNameOrDescription(searchText)
				.Returns(products);

			mapper.Map<IList<Product>, IList<ProductReadDto>>(products)
				.Returns(expected);

			// Act
			var actual = sut.SearchByText(searchText);

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}
