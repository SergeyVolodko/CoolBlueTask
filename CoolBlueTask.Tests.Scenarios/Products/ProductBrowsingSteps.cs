using System.Collections.Generic;
using System.IO;
using System.Net;
using CoolBlueTask.Products.Models;
using CoolBlueTask.Tests.Scenarios.Data;
using CoolBlueTask.Tests.Scenarios.Infrastructure;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoolBlueTask.Tests.Scenarios.Products
{
	[Binding]
	public class ProductBrowsingSteps
	{
		private readonly string url = "/products";
		private readonly string adminToken = Consts.JeffsToken;

		private TypedResponse<List<ProductReadDto>> response;

		[Given(@"Jeff has following products in his store")]
		public void GivenJeffHasFollowingProductsInHisStore(
			Table productsTable)
		{
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, "new_product_request.json");
			foreach (var row in productsTable.Rows)
			{
				var json = File.ReadAllText(jsonPath);

				var productName = row["product"];
				var toCreateJson = json.Replace("product-name", productName);
				var createdResponse = Locator.HttpHelper.PostJson(url, toCreateJson, adminToken);

				createdResponse.StatusCode
					.Should().Be(HttpStatusCode.Created);
			}
		}

		[When(@"I search for '(.*)'")]
		public void WhenISearchFor(string searchText)
		{
			var searchUrl = $"{url}/search/{searchText}";
			response = Locator.HttpHelper.Get<List<ProductReadDto>>(
				searchUrl, token: null);
		}

		[Then(@"I see following products")]
		public void ThenISeeFollowingS(Table foundProductsTable)
		{
			response.StatusCode
					.Should().Be(HttpStatusCode.OK);

			var expectedCount = foundProductsTable.RowCount;

			var actual = response.Data;

			actual.Count.Should().Be(expectedCount);

			foreach (var row in foundProductsTable.Rows)
			{
				var expectedName = row["product"];
				actual.Should().Contain(p => p.Name == expectedName);
			}
		}
	}
}
