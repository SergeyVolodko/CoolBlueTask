using System;
using System.Collections.Generic;
using System.Linq;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations.Models;
using Simple.Data;

namespace CoolBlueTask.SalesCombinations
{
	public interface ISalesCombinationRepository
	{
		SalesCombination Save(SalesCombination salesCombination);

		IList<SalesCombination> LoadByProduct(string productId);
	}

	public class SalesCombinationRepository : ISalesCombinationRepository
	{
		private readonly string connectionString;

		public SalesCombinationRepository(IApiConfiguration configuration)
		{
			this.connectionString = configuration.DbConnectionString;
		}

		private dynamic OpenDB()
		{
			return Database.OpenFile(connectionString);
		}

		public IList<SalesCombination> LoadByProduct(string productId)
		{
			try
			{
				var db = OpenDB();
				var expr = db.SalesCombination.MainProductId.Like("%" + productId + "%");
				var combinations = (List<SalesCombinationDbo>) db.SalesCombination.FindAll(expr);

				var result = new List<SalesCombination>();
				foreach (var combination in combinations)
				{
					var mainProduct = (Product)db.Product.FindById(combination.MainProductId);
					var relatedProductsIds = combination.RelatedProducts.Split('|').ToList();
					var relatedProducts = relatedProductsIds
						.Select(id => (Product)db.Product.FindById(id)).ToList();

					result.Add(new SalesCombination
					{
						Id = combination.Id,
						MainProduct = mainProduct,
						RelatedProducts = relatedProducts
					});
				}

				return result;
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}

		public SalesCombination Save(SalesCombination salesCombination)
		{
			try
			{
				salesCombination.Id = Guid.NewGuid().ToString();

				var dbo = new SalesCombinationDbo
				{
					Id = salesCombination.Id,
					MainProductId = salesCombination.MainProduct.Id,
					RelatedProducts = string.Join("|", 
						salesCombination.RelatedProducts.Select(p => p.Id))
				};

				OpenDB().SalesCombination.Insert(dbo);

				return salesCombination;
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}
	}
}