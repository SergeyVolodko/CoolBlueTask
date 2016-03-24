using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolBlueTask.SalesCombinations
{
    public interface ISaleasCombinationRepository
    {
        IList<SalesCombination> LoadByProduct(Guid productId);
    }


    public class SaleasCombinationRepository: ISaleasCombinationRepository
    {
        private IList<SalesCombination> storage;

        public SaleasCombinationRepository()
        {
            storage = new List<SalesCombination>();

            var id1 = Guid.Empty;
            var id2 = Guid.NewGuid();
            var sale = new SalesCombination
            {
                Products = new List<Guid> {id1, id2}
            };

            Save(sale);
        }

        public IList<SalesCombination> LoadByProduct(Guid productId)
        {
            return storage.Where(c => c.Products.Contains(productId)).ToList();
        }

        public void Save(SalesCombination salesCombination)
        {
            salesCombination.Id = Guid.NewGuid();

            storage.Add(salesCombination);
        }
    }
}