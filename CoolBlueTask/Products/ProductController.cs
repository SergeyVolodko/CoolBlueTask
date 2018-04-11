using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;

namespace CoolBlueTask.Products
{
	[RoutePrefix("products")]
	public class ProductController : ApiController
	{
		private readonly IProductService productService;
		private readonly ISalesCombinationService salesService;

		public ProductController(
			IProductService productService,
			ISalesCombinationService salesService)
		{
			this.productService = productService;
			this.salesService = salesService;
		}

		[HttpPost]
		[Route("")]
		public ProductReadDto CreateProduct(
			[FromBody]ProductWriteDto product)
		{
			return productService.CreateProduct(product);
		}

		[HttpGet]
		[Route("")]
		public IList<ProductReadDto> GetAllProducts()
		{
			return productService.GetAll();
		}

		[HttpGet]
		[Route("{searchText}")]
		public IList<ProductReadDto> SearchProducts(string searchText)
		{
			return productService.SearchByText(searchText);
		}

		[HttpGet]
		[Route("{productId}/salesCombinations")]
		public IList<SalesCombination> GetProductSalesCombinations(string productId)
		{
			var sales = salesService
				.GetByProduct(productId);

			if (sales == null)
			{
				throw new HttpResponseException(
					HttpStatusCode.BadRequest);
			}

			return sales;
		}
	}
}