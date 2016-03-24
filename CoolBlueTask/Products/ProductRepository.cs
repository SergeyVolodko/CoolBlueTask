using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductRepository
    {
        void Save(Product product);
        IList<Product> LoadAll();
        IList<Product> LoadByName(string searchText);
    }

    public class ProductRepository: IProductRepository
    {
        private List<Product> storage;

        //private string connectionString;
        public ProductRepository(/*string connectionString*/)
        {
            //this.connectionString = connectionString;
            storage = new List<Product>();
        }

        public void Save(Product product)
        {
            storage.Add(product);
        }

        public IList<Product> LoadAll()
        {
            return storage;
        }

        public IList<Product> LoadByName(string searchText)
        {
            throw new NotImplementedException();
        }
    }

   
}