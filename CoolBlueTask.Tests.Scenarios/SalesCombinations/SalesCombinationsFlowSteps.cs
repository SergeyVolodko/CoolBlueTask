using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using ApprovalTests;
using ApprovalTests.Reporters;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Scenarios.Data;
using CoolBlueTask.Tests.Scenarios.Infrastructure;
using FluentAssertions;
using Ploeh.AutoFixture;
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
		private TypedResponse<SalesCombinationReadDto> validationResponse;
		private string expectedValidationJson;

		[Given(@"Jeff has related products in his store")]
		public void GivenJeffHasRelatedProductsInHisStore(
			Table productsTable)
		{
			var jsonPath = Path.Combine(Consts.SalesCombinationsJsonSamplesFolder, "existing_product_request.json");
			foreach (var row in productsTable.Rows)
			{
				var json = File.ReadAllText(jsonPath);

				var productName = row["product"];

				var productId = createProduct(json, productName);

				products[productName] = productId;
			}
		}

		private string createProduct(string json, string productName)
		{
			var toCreateJson = json.Replace("product-name", productName);
			var createdResponse = Locator.HttpHelper.PostJson(productsUrl, toCreateJson, adminToken);

			createdResponse.StatusCode
				.Should().Be(HttpStatusCode.Created);
			var productId = ScenariosHelper.GetIdFromResponse(createdResponse);
			return productId;
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

		/// <summary>
		/// Validations
		/// </summary>
		[Given(@"Jeff has '(.*)' and '(.*)' products in his store")]
		public void GivenJeffHasRelatedProductsInHisStore(
			string productName1,
			string productName2)
		{
			var jsonPath = Path.Combine(Consts.SalesCombinationsJsonSamplesFolder, "existing_product_request.json");
			var json = File.ReadAllText(jsonPath);

			products[productName1] = createProduct(json, productName1);
			products[productName2] = createProduct(json, productName2);
		}

		[When(@"he tries to create a sale combination by entering (.*)")]
		public void WhenHeTriesToCreateASaleCombinationByEntering(
			string invalidInput)
		{
			var combination = new SalesCombinationWriteDto { RelatedProducts = new List<string>() };

			switch (invalidInput)
			{
				case "Empty input":
				{
					combination = null;
					expectedValidationJson = "create_invalid_combination_empty_input_response.json";
					break;
				}
				case "Main product and related products are missed":
				{
					combination.MainProductId = null;
					expectedValidationJson = "create_invalid_combination_corrupted_input_response.json";
					break;
				}
				case "Not existing main product":
				{
					combination.MainProductId = "non-existing";
					combination.RelatedProducts.Add(products["Paper"]);
					expectedValidationJson = "create_invalid_combination_not_existing_main_response.json";
					break;
				}
				case "One of the related products does not exist":
				{
					combination.MainProductId = products["Pen"];
					combination.RelatedProducts.Add(products["Paper"]);
					combination.RelatedProducts.Add("non-existing");
					expectedValidationJson = "create_invalid_combination_not_existing_related_response.json";
					break;
				}
				case "Too many related products":
				{
					var maxRelatedProducts = 5;
					var fixture = new Fixture();
					fixture.RepeatCount = maxRelatedProducts + 1;

					combination.MainProductId = products["Pen"];
					combination.RelatedProducts = fixture.Create<List<string>>();
					expectedValidationJson = "create_invalid_combination_too_many_related_response.json";
					break;
				}
			}

			validationResponse = Locator.HttpHelper
				.PostObject<SalesCombinationReadDto>(salesCombinationsUrl, combination, adminToken);
		}


		[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		[Then(@"Jeff should see corresponding errors")]
		public void ThenJeffShouldSeeCorrespondingErrors()
		{
			validationResponse.StatusCode
				.Should().Be(HttpStatusCode.BadRequest);

			var jsonPath = Path.Combine(Consts.SalesCombinationsJsonSamplesFolder, expectedValidationJson);
			var expected = File.ReadAllText(jsonPath);

			var actual = validationResponse.ContentAsFormattedJson;

			var writer = new TwoJsonsApprovalFileWriter(expected, actual, nameOfExpected: expectedValidationJson);
			Approvals.Verify(writer);
		}

	}
}
