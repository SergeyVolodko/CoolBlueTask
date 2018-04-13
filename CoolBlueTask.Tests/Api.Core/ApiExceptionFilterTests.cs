using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using CoolBlueTask.Api.Core;
using CoolBlueTask.Tests.Infrastructure;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using NSubstitute;
using NLog;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CoolBlueTask.Tests
{
	public class ApiExceptionFilterTests
	{
		private readonly ApiExceptionFilterAttribute sut;
		private readonly ILogger logger;

		public ApiExceptionFilterTests()
		{
			logger = Substitute.For<ILogger>();
			sut = new ApiExceptionFilterAttribute(logger);
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

		[Theory]
		[MemberData(nameof(ErrorTypes))]
		public void all_errors_should_be_logged_with_correct_level(
			Exception exception,
			LogLevel logLevel)
		{
			// Arrange
			var httpMethod = "Put";
			var context = WebApi.MockContextWithException(exception, httpMethod);
			var endpoint = context.Request.RequestUri.AbsolutePath;

			// Act
			sut.Invoking(s => s.OnException(context))
				.ShouldThrow<HttpResponseException>();

			// Assert
			var logMessage =
				$"\r\nError type: {exception.GetType().Name}" +
				$"\r\nRequest: {httpMethod} | {endpoint}" +
				$"\r\nMessage: {exception.Message}" +
				$"\r\nStackTrace: {exception.StackTrace}" +
				$"\r\nInnerMessage: {exception.InnerException?.Message}";

			if (logLevel == LogLevel.Warn)
			{
				logger.Received(1).Warn(exception, logMessage);
			}
			else if (logLevel == LogLevel.Fatal)
			{
				logger.Received(1).Fatal(exception, logMessage);
			}
			else
			{
				logger.Received(1).Error(exception, logMessage);
			}
		}

		public static IEnumerable<object[]> ErrorTypesAndCodes()
		{
			yield return new object[] { new ValidationException("error"), HttpStatusCode.BadRequest };
			yield return new object[] { new UnauthorizedAccessException(), HttpStatusCode.Unauthorized };
			yield return new object[] { new NullReferenceException(), HttpStatusCode.InternalServerError };
			yield return new object[] { new Exception(), HttpStatusCode.InternalServerError };
			yield return new object[] { new EntityNotFoundException(), HttpStatusCode.NotFound };
		}

		public static IEnumerable<object[]> ErrorTypes()
		{
			yield return new object[] { new ValidationException("error"), LogLevel.Warn };
			yield return new object[] { new EntityNotFoundException(), LogLevel.Warn };
			yield return new object[] { new NullReferenceException(), LogLevel.Error };
			yield return new object[] { new DataBaseException(), LogLevel.Error };
			yield return new object[] { new OutOfMemoryException("fatal"), LogLevel.Fatal };
			yield return new object[] { new StackOverflowException("fatal"), LogLevel.Fatal };
		}

		[Theory]
		[AutoData]
		public void validation_error_should_contain_all_validation_messages(
			List<ValidationFailure> failures)
		{
			// Arrange
			var exception = new ValidationException(failures);
			var context = WebApi.MockContextWithException(exception);
			var errorList = exception.Errors.Select(error =>
				new InvalidField
				{
					PropertyName = error.PropertyName,
					ErrorCode = error.ErrorCode,
					ErrorMessage = error.ErrorMessage
				}).ToList();

			var expectedJson = JsonConvert.SerializeObject(errorList, Formatting.Indented);

			// Act // Assert
			sut.Invoking(s => s.OnException(context))
				.ShouldThrow<HttpResponseException>()
				.And
				.Response.Content
				.ReadAsStringAsync().GetAwaiter().GetResult()
				.Should()
				.Be(expectedJson);
		}
	}
}
