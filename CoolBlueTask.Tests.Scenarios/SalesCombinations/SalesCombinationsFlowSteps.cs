using System.Collections.Generic;
using System.IO;
using System.Net;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Scenarios.Data;
using CoolBlueTask.Tests.Scenarios.Infrastructure;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoolBlueTask.Tests.Scenarios.SalesCombinations
{
	[Binding]
	public class SalesCombinationsFlowSteps
	{
		private readonly string productsUrl = "/products";
		private readonly string salesCombinationsUrl = "/sales_combinations";
		private readonly string adminToken = Consts.JeffsToken;

		private readonly Dictionary<string, string> products = new Dictionary<string, string>();
		private readonly List<SalesCombinationReadDto> expectedCombinations = new List<SalesCombinationReadDto>();
		private TypedResponse<List<SalesCombinationReadDto>> salesCombinationsResponse;

		[Given(@"Jeff has related products in his store")]
		public void GivenJeffHasRelatedProductsInHisStore(
			Table productsTable)
		{
			var jsonPath = Path.Combine(Consts.SalesCombinationsJsonSamplesFolder, "existing_product_request.json");
			foreach (var row in productsTable.Rows)
			{
				var json = File.ReadAllText(jsonPath);

				var productName = row["product"];
				var toCreateJson = json.Replace("product-name", productName);
				var createdResponse = Locator.HttpHelper.PostJson(productsUrl, toCreateJson, adminToken);

				createdResponse.StatusCode
					.Should().Be(HttpStatusCode.Created);
				var productId = ScenariosHelper.GetIdFromResponse(createdResponse);

				products[productName] = productId;
			}
		}

		[When(@"He defines combinations of these products")]
		public void WhenHeDefinesCombinationsOfTheseProducts(
			Table productCombinationsTable)
		{
			foreach (var row in productCombinationsTable.Rows)
			{
				var mainProduct = products[row["Main product"]];
				var relatedProduct1 = products[row["Related product 1"]];
				string relatedProduct2;
				products.TryGetValue(row["Related product 2"], out relatedProduct2);

				var combination = new SalesCombinationWriteDto
				{
					MainProductId = mainProduct,
					RelatedProducts = new List<string> { relatedProduct1 }
				};
				if (!string.IsNullOrWhiteSpace(relatedProduct2))
				{
					combination.RelatedProducts.Add(relatedProduct2);
				}

				var createdResponse = Locator.HttpHelper
					.PostObject<SalesCombinationReadDto>(salesCombinationsUrl, combination, adminToken);

				createdResponse.StatusCode
					.Should().Be(HttpStatusCode.Created);

				expectedCombinations.Add(createdResponse.Data);
			}
		}

		[When(@"customer observes '(.*)' product")]
		public void WhenCustomerObservesProduct(
			string productName)
		{
			var productId = products[productName];
			var suggestionsUrl = $"{productsUrl}/{productId}/sales_combinations";

			salesCombinationsResponse = Locator.HttpHelper
				.Get<List<SalesCombinationReadDto>>(suggestionsUrl, token: null);
		}

		[Then(@"customer sees defined by Jeff products suggestions for '(.*)'")]
		public void ThenCustomerSeesFollowingProductsSuggestedTo(
			string mainProduct)
		{
			salesCombinationsResponse.StatusCode
				.Should().Be(HttpStatusCode.OK);

			var actual = salesCombinationsResponse.Data;

			actual.Should().BeEquivalentTo(expectedCombinations);
		}
	}
}
