using System;
using System.Collections.Generic;

namespace CoolBlueTask.SalesCombinations
{
    public interface ISalesCombinationService
    {
        IList<SalesCombination> GetByProduct(string productId);
    }


    public class SalesCombinationService : ISalesCombinationService
    {
        private readonly ISaleasCombinationRepository salesRepository;

        public SalesCombinationService(
            ISaleasCombinationRepository salesRepository)
        {
            this.salesRepository = salesRepository;
        }

        public IList<SalesCombination> GetByProduct(
            string productId)
        {
            Guid id;
            if (!Guid.TryParse(productId, out id))
            {
                return null;
            }

            return salesRepository
                .LoadByProduct(id);
        }
    }
}