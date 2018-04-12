using System;
using System.Collections.Generic;
using CoolBlueTask.SalesCombinations.Models;

namespace CoolBlueTask.SalesCombinations
{
    public interface ISalesCombinationService
    {
        IList<SalesCombinationReadDto> GetProductSalesCombinations(string productId);
	    SalesCombinationReadDto CreateSalesCombination(
			SalesCombinationWriteDto combination);
    }


    public class SalesCombinationService : ISalesCombinationService
    {
        private readonly ISaleasCombinationRepository salesRepository;

        public SalesCombinationService(
            ISaleasCombinationRepository salesRepository)
        {
            this.salesRepository = salesRepository;
        }

        public IList<SalesCombinationReadDto> GetProductSalesCombinations(
            string productId)
        {
	        return null;
	        //Guid id;
	        //if (!Guid.TryParse(productId, out id))
	        //{
	        //    return null;
	        //}

	        //return salesRepository
	        //    .LoadByProduct(id);
        }

	    public SalesCombinationReadDto CreateSalesCombination(SalesCombinationWriteDto combination)
	    {
		    throw new NotImplementedException();
	    }
    }
}