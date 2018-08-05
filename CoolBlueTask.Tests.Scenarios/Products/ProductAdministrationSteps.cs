using System;
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

		[Given(@"Jeff has a product in his shop")]
		public void GivenJeffHasAProductInHisShop()
		{
			ScenarioContext.Current.Pending();
		}

		[When(@"he updates all its details")]
		public void WhenHeUpdatesAllItsDetails()
		{
			ScenarioContext.Current.Pending();
		}

		[When(@"Jeff tries to add a new product with (.*)")]
		public void WhenJeffTriesToAddANewProductWith(
			string invalidInput)
		{
			ScenarioContext.Current.Pending();
		}

		[When(@"Jeff tries to update this product with (.*)")]
		public void WhenJeffTriesToUpdateThisProductWith(
			string invalidInput)
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"he sees the updated product card")]
		public void ThenHeSeesTheUpdatedProductCard()
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"he sees the list of respective errors")]
		public void ThenHeSeesTheListOfRespectiveErrors()
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"he sees the the list of respective errors")]
		public void ThenHeSeesTheTheListOfRespectiveErrors()
		{
			ScenarioContext.Current.Pending();
		}
	}
}
