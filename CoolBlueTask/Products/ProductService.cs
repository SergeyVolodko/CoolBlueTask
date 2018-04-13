using System.Collections.Generic;
using AutoMapper;
using CoolBlueTask.Products.Models;
using FluentValidation;

namespace CoolBlueTask.Products
{
	public interface IProductService
	{
		ProductReadDto CreateProduct(ProductWriteDto product);
		ProductReadDto UpdateProduct(string id, ProductWriteDto productToUpdate);

		IList<ProductReadDto> GetAll();
		IList<ProductReadDto> SearchByText(string searchText);
		ProductReadDto GetProductById(string id);
	}

	public class ProductService : IProductService
	{
		private readonly IMapper mapper;
		private readonly AbstractValidator<Product> validator;
		private readonly IProductRepository productRepository;

		public ProductService(
			IMapper mapper,
			AbstractValidator<Product> validator,
			IProductRepository productRepository)
		{
			this.mapper = mapper;
			this.validator = validator;
			this.productRepository = productRepository;
		}

		public ProductReadDto CreateProduct(
			ProductWriteDto productToCreate)
		{
			var product = mapper.Map<ProductWriteDto, Product>(productToCreate);

			var validationResult = validator.Validate(product);
			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}

			var createdProduct = productRepository.Save(product);

			var createdDto = mapper.Map<Product, ProductReadDto>(createdProduct);
			return createdDto;
		}

		public IList<ProductReadDto> GetAll()
		{
			var products = productRepository.LoadAll();

			var dtos = mapper.Map<IList<Product>, IList<ProductReadDto>>(products);

			return dtos;
		}

		public IList<ProductReadDto> SearchByText(string searchText)
		{
			var products = productRepository.LoadByNameOrDescription(searchText);

			var dtos = mapper.Map<IList<Product>, IList<ProductReadDto>>(products);

			return dtos;
		}

		public ProductReadDto GetProductById(string id)
		{
			if (!productRepository.Exists(id))
			{
				throw new EntityNotFoundException();
			}

			var product = productRepository.LoadById(id);

			return mapper.Map<Product, ProductReadDto>(product);
		}

		public ProductReadDto UpdateProduct(
			string id, 
			ProductWriteDto productToUpdate)
		{
			var product = mapper.Map<ProductWriteDto, Product>(productToUpdate);

			var validationResult = validator.Validate(product);
			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}

			var updatedProduct = productRepository.Update(id, product);

			var createdDto = mapper.Map<Product, ProductReadDto>(updatedProduct);
			return createdDto;
		}
	}
}