﻿using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductService
    {
        ProductResult CreateProduct(ProductWriteDto product);
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

        public ProductResult CreateProduct(ProductWriteDto product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return ProductResult.NameIsEmpty;
            }

            try
            {
                productRepository.Save((Product)product);
            }
            catch (Exception ex)
            {
                return ProductResult.Failed;
            }

            return ProductResult.Ok;
        }

        public IList<Product> GetAllProducts()
        {
            return productRepository.LoadAll();
        }

        public IList<Product> SearchProducts(string searchText)
        {
            return productRepository.LoadByNameOrDescription(searchText);
        }
    }
    
}