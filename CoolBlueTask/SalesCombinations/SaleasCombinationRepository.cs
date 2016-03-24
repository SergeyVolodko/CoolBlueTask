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
        public IList<SalesCombination> LoadByProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}