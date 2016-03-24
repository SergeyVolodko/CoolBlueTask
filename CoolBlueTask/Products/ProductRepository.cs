using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public  void Save(Product product)
        {
            

        }

        public async Task<IList<Product>> LoadAll()
        {
            return null;
        }
    }
}