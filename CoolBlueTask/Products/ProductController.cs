using System.Collections.Generic;
using System.Web.Http;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
	[RoutePrefix("products")]
	public class ProductController : ApiController
	{
		private readonly IProductService service;

		public ProductController(
			IProductService service)
		{
			this.service = service;
		}

		[HttpPost]
		[Route("")]
		public ProductReadDto CreateProduct(
			[FromBody]ProductWriteDto product)
		{
			return service.CreateProduct(product);
		}

		[HttpPut]
		[Route("{id}")]
		public ProductReadDto UpdateProduct(
			string id,
			[FromBody]ProductWriteDto productToUpdate)
		{
			return service.UpdateProduct(id, productToUpdate);
		}

		[HttpGet]
		[Route("")]
		public IList<ProductReadDto> GetAllProducts()
		{
			return service.GetAll();
		}

		[HttpGet]
		[Route("{id}")]
		public ProductReadDto GetProduct(string id)
		{
			return service.GetProductById(id);
		}

		[HttpGet]
		[Route("search/{searchText}")]
		public IList<ProductReadDto> SearchProducts(string searchText)
		{
			return service.SearchByText(searchText);
		}
	}
}