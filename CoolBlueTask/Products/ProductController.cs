using System.Collections.Generic;
using System.Web.Http;
using CoolBlueTask.Products.Models;

namespace CoolBlueTask.Products
{
    [RoutePrefix("product")]
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
        public int AddProduct(ProductToAdd product)
        {
            var result = service.AddProduct(product);
            return (int)result;
        }

        [HttpGet]
        [Route("")]
        public IList<Product> GetProducts()
        {
            return service.GetAllProducts();
        }

        [HttpGet]
        [Route("{searchText}")]
        public IList<Product> Search(string searchText)
        {
            return service.SearchProducts(searchText);
        }
    }
}