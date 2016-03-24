using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CoolBlueTask.Products.Models;
using CoolBlueTask.SalesCombinations;

namespace CoolBlueTask.Products
{
    [RoutePrefix("product")]
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
        public int AddProduct(ProductToAdd product)
        {
            var result = productService.AddProduct(product);
            return (int)result;
        }

        [HttpGet]
        [Route("")]
        public IList<Product> GetProducts()
        {
            return productService.GetAllProducts();
        }

        [HttpGet]
        [Route("{searchText}")]
        public IList<Product> Search(string searchText)
        {
            return productService.SearchProducts(searchText);
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