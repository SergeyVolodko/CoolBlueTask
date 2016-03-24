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
        public ProductResult AddProduct(ProductToAdd product)
        {
            throw new NotImplementedException();
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