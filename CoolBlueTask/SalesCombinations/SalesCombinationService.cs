using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBlueTask.SalesCombinations
{
    public interface ISalesCombinationService
    {
        IList<SalesCombination> GetByProduct(string productId);
    }


    public class SalesCombinationService : ISalesCombinationService
    {
        public IList<SalesCombination> GetByProduct(string productId)
        {
            throw new NotImplementedException();
        }
    }
}