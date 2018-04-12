using System.Collections.Generic;
using System.Web.Http;
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

		[HttpGet]
		[Route("products/{productId}/sales_combinations")]
		public IList<SalesCombinationReadDto> GetProductSalesCombinations(
			string productId)
		{
			return service.GetProductSalesCombinations(productId);
		}
	}
}