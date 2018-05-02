using System;
using System.Collections.Generic;
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

		public SalesCombinationRepository(string connectionString = null)
		{
			this.connectionString = connectionString;
		}

		private dynamic OpenDB()
		{
			return Database.OpenFile(connectionString);
		}

		public IList<SalesCombination> LoadByProduct(string productId)
		{
			//try
			//{
			//	var db = OpenDB();
			//	var expr = db.SalesCombination.Products.Like("%" + productId + "%");
			//	return (List<SalesCombination>)db.SalesCombination.FindAll(expr);
			//}
			//catch (Exception)
			//{
			//	throw new DataBaseException();
			//}
			return null;
		}

		public SalesCombination Save(SalesCombination salesCombination)
		{
			try
			{
				salesCombination.Id = Guid.NewGuid().ToString();

				OpenDB().SalesCombination.Insert(salesCombination);

				return salesCombination;
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}
	}
}