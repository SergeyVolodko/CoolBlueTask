using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CoolBlueTask.Products.Models;
using Simple.Data;

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
        

        private string connectionString;
        public ProductRepository(string connectionString = null)
        {
            this.connectionString = connectionString;
        }

        private dynamic OpenDB()
        {
            return Database.OpenFile(connectionString);
        }
        
        public void Save(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            OpenDB().Product.Insert(product);
        }

        public IList<Product> LoadAll()
        {
            return (List<Product>)OpenDB().Product.All();
        }

        public IList<Product> LoadByNameOrDescription(string searchText)
        {
            return storage.Where(
                    p => p.Name.Contains(searchText)
                         || p.Description.Contains(searchText)).ToList();
        }
    }
   
}