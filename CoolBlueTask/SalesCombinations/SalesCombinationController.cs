using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
		public HttpResponseMessage CreateCombination(
			[FromBody]SalesCombinationWriteDto combination)
		{
			var createdCombination =  service.CreateSalesCombination(combination);

			return Request.CreateResponse(HttpStatusCode.Created, createdCombination);
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