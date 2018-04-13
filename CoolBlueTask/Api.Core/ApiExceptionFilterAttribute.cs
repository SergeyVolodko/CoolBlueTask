using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using NLog;
using System.Linq;
using System.Text;
using CoolBlueTask.Api.Core;
using FluentValidation;
using Newtonsoft.Json;

namespace CoolBlueTask
{
	public class ApiExceptionFilterAttribute :
		ExceptionFilterAttribute, IAutofacExceptionFilter
	{
		private readonly ILogger logger;

		public ApiExceptionFilterAttribute(
			ILogger logger)
		{
			this.logger = logger;
		}

		public override void OnException(
			HttpActionExecutedContext httpContext)
		{
			var exception = httpContext.Exception;
			var endpoint = httpContext.Request.RequestUri?.AbsolutePath;

			logException(exception, httpContext.Request.Method, endpoint);

			var httpStatusCode = HttpStatusCode.InternalServerError;

			if (exception is UnauthorizedAccessException)
			{
				httpStatusCode = HttpStatusCode.Unauthorized;
			}
			else
			if (exception is ValidationException)
			{
				throwValidationError((ValidationException)exception);
			}
			else if (exception is EntityNotFoundException)
			{
				throwExceptionWithDebugInfo(exception, HttpStatusCode.NotFound);
			}

			throwExceptionWithDebugInfo(exception, httpStatusCode);
		}

		private void logException(
			Exception exception,
			HttpMethod requestMethod,
			string endpoint)
		{
			var exceptionName = exception.GetType().Name;
			var fatalExceptions =
				new[]
				{
					nameof(OutOfMemoryException),
					nameof(StackOverflowException)
				};

			var warningExceptions =
				new[]
				{
					nameof(EntityNotFoundException),
					nameof(ValidationException)
				};

			var logMessage = ErrorFormatter.FormatMessage(
				exception,
				requestMethod,
				endpoint);

			if (warningExceptions.Contains(exceptionName))
			{
				logger.Warn(exception, logMessage);
			}
			else if (fatalExceptions.Contains(exceptionName))
			{
				logger.Fatal(exception, logMessage);
			}
			else
			{
				logger.Error(exception, logMessage);
			}
		}

		private void throwExceptionWithDebugInfo(
			Exception exception,
			HttpStatusCode statusCode)
		{
			var ex = new HttpResponseMessage(statusCode)
				{ Content = new StringContent(exception.Message + exception.StackTrace) };
			throw new HttpResponseException(ex);
		}

		private void throwValidationError(ValidationException ex)
		{
			var errorList = new List<InvalidField>();
			if (ex.Errors.Any())
			{
				errorList = ex.Errors
					.Select(e => new InvalidField(e)).ToList();
			}
			else
			{
				errorList.Add(new InvalidField { ErrorMessage = ex.Message });
			}

			var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				Content = new StringContent(JsonConvert.SerializeObject(
					errorList, Formatting.Indented))
			};
			throw new HttpResponseException(responseMessage);
		}
	}

	internal static class ErrorFormatter
	{
		public static string FormatMessage(
			Exception exception,
			HttpMethod httpMethod,
			string endpoint)
		{
			var sb = new StringBuilder();
			
			sb.Append($"\r\nError type: {exception.GetType().Name}");
			sb.Append($"\r\nRequest: {httpMethod} | {endpoint}");
			sb.Append($"\r\nMessage: {exception.Message}");
			sb.Append($"\r\nStackTrace: {exception.StackTrace}");
			sb.Append($"\r\nInnerMessage: {exception.InnerException?.Message}");

			return sb.ToString();
		}
	}
}