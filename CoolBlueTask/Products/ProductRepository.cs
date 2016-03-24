using System;
using System.Collections.Generic;
using System.Linq;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    public interface IProductRepository
    {
        void Save(Product product);
        IList<Product> LoadAll();
        IList<Product> LoadByNameOrDescription(string searchText);
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
            product.Id = Guid.NewGuid();
            storage.Add(product);
        }

        public IList<Product> LoadAll()
        {
            return storage;
        }

        public IList<Product> LoadByNameOrDescription(string searchText)
        {
            return storage.Where(
                    p => p.Name.Contains(searchText)
                         || p.Description.Contains(searchText)).ToList();
        }
    }
   
}