using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CoolBlueTask.Products;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Owin;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class AdHocTests
	{
		//[Theory]
		//[AutoNSubstituteData]
		//public void try_simple_data(
		//    Product product)
		//{
		//    // arrange
		//    var repo = new ProductRepository(
		//        @"Data Source='C:\temp\db\coolBlue.sqlite'"
		//        //"Data Source=:memory:;Version=3;"
		//        );

		//    // act // assert
		//    repo.Save(product);
		//    //var all = await repo.LoadAll();
		//    //all.ShouldBeEquivalentTo(new List<Product> {product});
		//}


		//private Action<IAppBuilder> action;
		//[Theory]
		//[AutoNSubstituteData]
		//public void test_name()
		//{
		//	// Arrange
		//	var baseAddress = "http://localhost:11111";
		//	var startup = new Startup { apiConfiguration = new TestApiConfiguration() };
			
		//	//startup.Configuration(Substitute.For<IAppBuilder>());
		//	action = startup.Configuration;

		//	var client = new HttpClient();

		//	var url = $"{baseAddress}/version";
		//	// Act
		//	// Assert
		//	using (var server = WebApp.Start(baseAddress, action))
		//	{
		//		var actual = client.GetAsync(url)
		//			.ConfigureAwait(false)
		//			.GetAwaiter().GetResult();

		//		actual.StatusCode
		//			.Should().Be(HttpStatusCode.OK);
		//	}
		//}

	}
}
