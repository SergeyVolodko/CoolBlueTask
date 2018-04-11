using System;
using System.Collections.Generic;
using Simple.Data;

namespace CoolBlueTask.SalesCombinations
{
	public interface ISaleasCombinationRepository
	{
		IList<SalesCombination> LoadByProduct(Guid productId);
	}

	public class SaleasCombinationRepository : ISaleasCombinationRepository
	{
		private readonly string connectionString;

		public SaleasCombinationRepository(string connectionString = null)
		{
			this.connectionString = connectionString;
		}

		private dynamic OpenDB()
		{
			return Database.OpenFile(connectionString);
		}

		public IList<SalesCombination> LoadByProduct(Guid productId)
		{
			try
			{
				var db = OpenDB();
				var expr = db.SalesCombination.Products.Like("%" + productId + "%");
				return (List<SalesCombination>)db.SalesCombination.FindAll(expr);
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}

		public void Save(SalesCombination salesCombination)
		{
			try
			{
				salesCombination.Id = Guid.NewGuid().ToString();

				OpenDB().SalesCombination.Insert(salesCombination);
			}
			catch (Exception)
			{
				throw new DataBaseException();
			}
		}
	}
}