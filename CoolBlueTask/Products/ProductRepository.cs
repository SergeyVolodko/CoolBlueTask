using System;
using System.Collections.Generic;
using CoolBlueTask.Products.Models;
using Simple.Data;

namespace CoolBlueTask.Products
{
	public interface IProductRepository
	{
		Product Save(Product product);
		Product Update(string id, Product product);

		IList<Product> LoadAll();
		IList<Product> LoadByNameOrDescription(string searchText);
		bool Exists(string productId);
		Product LoadById(string id);
	}

	public class ProductRepository : IProductRepository
	{
		private readonly string connectionString;

		public ProductRepository(IApiConfiguration configuration)
		{
			this.connectionString = configuration.DbConnectionString;
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

		public Product Update(string id, Product product)
		{
			try
			{
				product.Id = id;
				OpenDB().Product.UpdateById(product);

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

		public bool Exists(string productId)
		{
			try
			{
				return (bool)OpenDB().Product.ExistsById(productId);
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}

		public Product LoadById(string id)
		{
			try
			{
				return (Product)OpenDB().Product.FindById(id);
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}
	}
}