using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductRepository
    {
        void Save(Product product);
    }

    public class ProductRepository: IProductRepository
    {
        private string connectionString;
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Save(Product product)
        {
            

        }

        public async Task<IList<Product>> LoadAll()
        {
            return null;
        }
    }

   
}