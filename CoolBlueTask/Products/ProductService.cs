using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductService
    {
        ProductReadDto CreateProduct(ProductWriteDto product);
        IList<ProductReadDto> GetAll();
        IList<ProductReadDto> SearchByText(string searchText);
    }

    public class ProductService: IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
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
            productRepository.LoadAll();

	        return null;
        }

        public IList<ProductReadDto> SearchByText(string searchText)
        {
	        return null;
            //return productRepository.LoadByNameOrDescription(searchText);
        }
    }
    
}