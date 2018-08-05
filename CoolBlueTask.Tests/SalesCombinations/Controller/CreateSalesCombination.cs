using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolBlueTask.Api.Core;
using CoolBlueTask.SalesCombinations;
using CoolBlueTask.SalesCombinations.Models;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests.SalesCombinations.Controller
{
	public class CreateSalesCombinationSalesCombination
	{
		[Fact]
		public void routing_create_combination()
		{
			// Arrange
			var uri = @"http://localhost:4242/sales_combinations";
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			var config = new HttpConfiguration();

			// Act
			WebApiConfig.Register(config);
			var route = WebApi.RouteRequest(config, request);

			// Asserts
			route.Controller.Should().Be<SalesCombinationController>();
			route.Action.Should().Be("CreateCombination");
		}

		[Fact]
		public void has_authorization_attribute()
		{
			// Arrange
			var method = typeof(SalesCombinationController)
				.Methods()
				.FirstOrDefault(m => m.Name == "CreateCombination");

			// Act
			var attributes = method.GetCustomAttributes(false);

			// Assert
			attributes.Should()
				.Contain(a => a is Auth0AuthorizationAttribute);
		}

		[Theory]
		[ControllerAutoData]
		public void calls_service_create_combination(
			[Frozen] ISalesCombinationService service,
			SalesCombinationController sut,
			SalesCombinationWriteDto combination)
		{
			// Act
			sut.CreateCombination(combination);

			// Assert
			service.Received().CreateSalesCombination(combination);
		}

		[Theory]
		[ControllerAutoData]
		public void returns_created_combination(
			[Frozen] ISalesCombinationService service,
			SalesCombinationController sut,
			SalesCombinationWriteDto combination,
			SalesCombinationReadDto createdSalesCombination)
		{
			// Arrange
			service
				.CreateSalesCombination(combination)
				.Returns(createdSalesCombination);

			// Act
			var response = sut.CreateCombination(combination);

			// Asserts
			response.StatusCode.Should().Be(HttpStatusCode.Created);

			var content = response.Content.ReadAsStringAsync()
				.GetAwaiter().GetResult();

			JsonConvert.DeserializeObject<SalesCombinationReadDto>(content)
				.ShouldBeEquivalentTo(createdSalesCombination);
		}
	}
}
