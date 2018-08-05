using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Api.Core;
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
		[Auth0Authorization]
		[Route("")]
		public HttpResponseMessage CreateProduct(
			[FromBody]ProductWriteDto product)
		{
			var createdProduct =  service.CreateProduct(product);

			return Request.CreateResponse(HttpStatusCode.Created, createdProduct);
		}

		[HttpPut]
		[Auth0Authorization]
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