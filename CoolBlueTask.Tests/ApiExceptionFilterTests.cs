using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class ApiExceptionFilterTests
	{
		private ApiExceptionFilterAttribute sut;

		public ApiExceptionFilterTests()
		{
			sut = new ApiExceptionFilterAttribute();
		}

		[Theory]
		[MemberData(nameof(ErrorTypesAndCodes))]
		public void exceptions_are_handled(
			Exception exception,
			HttpStatusCode expectedCode)
		{
			// Arrange
			var context = WebApi.MockContextWithException(exception);

			// Act // Assert
			sut.Invoking(s => s.OnException(context))
				.ShouldThrow<HttpResponseException>()
				.And.Response
				.StatusCode.Should().Be(expectedCode);
		}

		public static IEnumerable<object[]> ErrorTypesAndCodes()
		{
			//yield return new object[] { new ValidationException("error"), HttpStatusCode.BadRequest };
			//yield return new object[] { new ValidationErrorException("error"), HttpStatusCode.BadRequest };
			yield return new object[] { new UnauthorizedAccessException(), HttpStatusCode.Unauthorized };
			yield return new object[] { new NullReferenceException(), HttpStatusCode.InternalServerError };
			yield return new object[] { new Exception(), HttpStatusCode.InternalServerError };
			//yield return new object[] { new EntityNotFoundException(new Entity()), HttpStatusCode.NotFound };
		}
	}
}
