using System;
using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
	public interface IProductService
	{
		ProductReadDto CreateProduct(ProductWriteDto product);
		IList<ProductReadDto> GetAll();
		IList<ProductReadDto> SearchByText(string searchText);
	}

	public class ProductService : IProductService
	{
		private readonly IMapper mapper;
		private readonly IProductRepository productRepository;

		public ProductService(
			IMapper mapper,
			IProductRepository productRepository)
		{
			this.mapper = mapper;
			this.productRepository = productRepository;
		}

		public ProductReadDto CreateProduct(ProductWriteDto product)
		{
			//if (string.IsNullOrWhiteSpace(product.Name))
			//{
			//    return ProductResult.NameIsEmpty;
			//}

			//var createdProduct = 
			//productRepository.Save((Product)product);

			return null;
		}

		public IList<ProductReadDto> GetAll()
		{
			var products = productRepository.LoadAll();

			var dto = mapper.Map<IList<Product>, IList<ProductReadDto>>(products);

			return dto;
		}

		public IList<ProductReadDto> SearchByText(string searchText)
		{
			return null;
			//return productRepository.LoadByNameOrDescription(searchText);
		}
	}

}