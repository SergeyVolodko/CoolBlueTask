using System;
using System.Collections.Generic;
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
        private string connectionString;

        public ProductRepository(string connectionString = null)
        {
            this.connectionString = connectionString;

            // Mocking with in-memory DB;
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
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
            var db = OpenDB();

            var expr1 = db.Product.Name.Like("%"+ searchText+ "%");
            var expr2 = db.Product.Description.Like("%" + searchText + "%");

            return (List<Product>)db.Product
                .FindAll(expr1 || expr2);
        }
    }
   
}