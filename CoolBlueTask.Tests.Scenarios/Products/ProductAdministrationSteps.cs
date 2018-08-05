using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using ApprovalTests;
using ApprovalTests.Reporters;
using CoolBlueTask.Tests.Scenarios.Data;
using CoolBlueTask.Tests.Scenarios.Infrastructure;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CoolBlueTask.Tests.Scenarios.Products
{
	[Binding]
	public class ProductAdministrationSteps
	{
		private readonly string url = "/products";
		private readonly string token = Consts.JeffsToken;

		private string productId;
		private string urlWithId;

		private TypedResponse response;
		private string expectedJson;

		/// <summary>
		/// Create product
		/// </summary>
		[Given(@"Jeff has no products in his shop")]
		public void GivenJeffHasNoProductsInHisShop()
		{
		}

		[When(@"he adds a new product '(.*)' to the store")]
		public void WhenHeAddsANewProductToTheStore(
			string productName)
		{
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, "new_product_request.json");
			var json = File.ReadAllText(jsonPath);

			var toCreateJson = json.Replace("product-name", productName);
			var createdResponse = Locator.HttpHelper.PostJson(url, toCreateJson, token);

			createdResponse.StatusCode
				.Should().Be(HttpStatusCode.Created);

			productId = ScenariosHelper.GetIdFromResponse(createdResponse);
			urlWithId = $"{url}/{productId}";
		}

		[Then(@"he sees the new product in products list")]
		public void ThenHeSeesTheNewProductInProductsList()
		{
			var listOfProducts = Locator.HttpHelper.GetJson(url, token);

			listOfProducts.Should().Contain(productId);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		[Then(@"product card has all provided details of '(.*)'")]
		public void ThenProductCardHasAllProvidedDetails(
			string productName)
		{
			var actual = Locator.HttpHelper.GetJson(urlWithId, token);
			var expectedFileName = "new_product_response.json";
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, expectedFileName);

			var expected = File.ReadAllText(jsonPath)
				.Replace("\"product-id\"", $"\"{productId}\"")
				.Replace("product-name", productName);

			var writer = new TwoJsonsApprovalFileWriter(expected, actual, nameOfExpected: expectedFileName);
			Approvals.Verify(writer);
		}

		/// <summary>
		/// Edit product
		/// </summary>
		[Given(@"Jeff has product '(.*)' in his shop")]
		public void GivenJeffHasAProductInHisShop(
			string productName)
		{
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, "existing_product_request.json");
			var json = File.ReadAllText(jsonPath);

			var toCreateJson = json.Replace("product-name", productName);
			var createdResponse = Locator.HttpHelper.PostJson(url, toCreateJson, token);

			createdResponse.StatusCode
				.Should().Be(HttpStatusCode.Created);

			productId = ScenariosHelper.GetIdFromResponse(createdResponse);
			urlWithId = $"{url}/{productId}";
		}

		[When(@"he changes all its details")]
		public void WhenHeChangesAllItsDetails()
		{
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, "update_product_request.json");
			var toUpdateJson = File.ReadAllText(jsonPath);
			
			response = Locator.HttpHelper.PutJson(urlWithId, toUpdateJson, token);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		[Then(@"he sees the updated product card")]
		public void ThenHeSeesTheUpdatedProductCard()
		{
			response.StatusCode
				.Should().Be(HttpStatusCode.OK);

			var actual = response.ContentAsFormattedJson;

			var expectedFileName = "update_product_response.json";
			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, expectedFileName);

			var expected = File.ReadAllText(jsonPath)
				.Replace("\"product-id\"", $"\"{productId}\"");

			var writer = new TwoJsonsApprovalFileWriter(expected, actual, nameOfExpected: expectedFileName);
			Approvals.Verify(writer);
		}

		/// <summary>
		/// Add invalid product
		/// </summary>
		[When(@"Jeff tries to add a new product with (.*)")]
		public void WhenJeffTriesToAddANewProductWith(
			string invalidInput)
		{
			var jsonFile = string.Empty;
			switch (invalidInput)
			{
				case "Empty input":
					jsonFile = "product_empty_input_request.json";
					expectedJson = "product_empty_input_response.json";
					break;

				case "Invalid values for all fields":
					jsonFile = "product_all_fields_invalid_request.json";
					expectedJson = "product_all_fields_invalid_response.json";
					break;
			}

			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, jsonFile);
			var toUpdateJson = File.ReadAllText(jsonPath);

			response = Locator.HttpHelper.PostJson(url, toUpdateJson, token);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		[UseReporter(typeof(DiffReporter))]
		[Then(@"he should see the list of respective errors")]
		public void ThenHeShouldSeeTheListOfRespectiveErrors()
		{
			response.StatusCode
				.Should().Be(HttpStatusCode.BadRequest);

			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, expectedJson);
			var expected = File.ReadAllText(jsonPath);

			var actual = response.ContentAsFormattedJson;

			var writer = new TwoJsonsApprovalFileWriter(expected, actual, nameOfExpected: expectedJson);
			Approvals.Verify(writer);
		}

		/// <summary>
		/// Update product with invalid input
		/// </summary>
		[When(@"Jeff tries to update this product with (.*)")]
		public void WhenJeffTriesToUpdateThisProductWith(
			string invalidInput)
		{
			var jsonFile = string.Empty;
			switch (invalidInput)
			{
				case "Empty input":
					jsonFile = "product_empty_input_request.json";
					expectedJson = "product_empty_input_response.json";
					break;

				case "Invalid values for all fields":
					jsonFile = "product_all_fields_invalid_request.json";
					expectedJson = "product_all_fields_invalid_response.json";
					break;
			}

			var jsonPath = Path.Combine(Consts.ProductsJsonSamplesFolder, jsonFile);
			var toUpdateJson = File.ReadAllText(jsonPath);

			response = Locator.HttpHelper.PutJson(urlWithId, toUpdateJson, token);
		}
	}
}
