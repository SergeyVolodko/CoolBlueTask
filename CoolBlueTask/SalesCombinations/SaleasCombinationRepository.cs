using System;
using System.Collections.Generic;
using Simple.Data;

namespace CoolBlueTask.SalesCombinations
{
    public interface ISaleasCombinationRepository
    {
        IList<SalesCombination> LoadByProduct(Guid productId);
    }


    public class SaleasCombinationRepository: ISaleasCombinationRepository
    {
        private readonly string connectionString;

        public SaleasCombinationRepository(string connectionString = null)
        {
            this.connectionString = connectionString;
            //var id1 = Guid.Empty;
            //var id2 = Guid.NewGuid();
            //var sale = new SalesCombination
            //{
            //    Products = new List<Guid> {id1, id2}
            //};

            //Save(sale);
        }

        private dynamic OpenDB()
        {
            return Database.OpenFile(connectionString);
        }


        public IList<SalesCombination> LoadByProduct(Guid productId)
        {
            var db = OpenDB();
            var expr = db.SalesCombination.Products.Like("%" + productId + "%");
            return (List<SalesCombination>)db.SalesCombination.FindAll(expr);
        }

        public void Save(SalesCombination salesCombination)
        {
            salesCombination.Id = Guid.NewGuid().ToString();

            OpenDB().SalesCombination.Insert(salesCombination);
        }
    }
}