using System.Collections.Generic;
using System.Web.Http;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
	[RoutePrefix("products")]
	public class ProductController : ApiController
	{
		private readonly IProductService productService;

		public ProductController(
			IProductService productService)
		{
			this.productService = productService;
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
	}
}