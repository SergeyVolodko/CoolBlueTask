using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductService
    {
        ProductResult AddProduct(ProductToAdd product);
        IList<Product> GetAllProducts();
        IList<Product> SearchProducts(string searchText);
    }

    public class ProductService: IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductResult AddProduct(ProductToAdd product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return ProductResult.NameIsEmpty;
            }

            try
            {
                productRepository.Save((Product)product);
            }
            catch (Exception)
            {
                return ProductResult.Failed;
            }
            

            return ProductResult.Ok;
        }

        public IList<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public IList<Product> SearchProducts(string searchText)
        {
            throw new NotImplementedException();
        }
    }
    
}