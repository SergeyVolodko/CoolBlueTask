using System.Collections.Generic;
using System.Web.Http;
using CoolBlueTask.Api.Core;
using CoolBlueTask.SalesCombinations.Models;

namespace CoolBlueTask.SalesCombinations
{
	public class SalesCombinationController : ApiController
	{
		private readonly ISalesCombinationService service;

		public SalesCombinationController(
			ISalesCombinationService service)
		{
			this.service = service;
		}

		[HttpPost]
		[Auth0Authorization]
		[Route("sales_combinations")]
		public SalesCombinationReadDto CreateCombination(
			[FromBody]SalesCombinationWriteDto combination)
		{
			return service.CreateSalesCombination(combination);
		}

		[HttpGet]
		[Route("products/{productId}/sales_combinations")]
		public IList<SalesCombinationReadDto> GetProductSalesCombinations(
			string productId)
		{
			return service.GetCombinationsForProduct(productId);
		}
	}
}