using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;
using Simple.Data;

namespace CoolBlueTask.Products
{
	public interface IProductRepository
	{
		Product Save(Product product);
		IList<Product> LoadAll();
		IList<Product> LoadByNameOrDescription(string searchText);
	}

	public class ProductRepository : IProductRepository
	{
		private readonly string connectionString;

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

		public Product Save(Product product)
		{
			try
			{
				product.Id = Guid.NewGuid().ToString();
				OpenDB().Product.Insert(product);

				return product;
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}

		public IList<Product> LoadAll()
		{
			try
			{
				return (List<Product>)OpenDB().Product.All();
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}

		public IList<Product> LoadByNameOrDescription(string searchText)
		{
			try
			{
				var db = OpenDB();

				var expr1 = db.Product.Name.Like("%" + searchText + "%");
				var expr2 = db.Product.Description.Like("%" + searchText + "%");

				return (List<Product>)db.Product
					.FindAll(expr1 || expr2);
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}
	}

}