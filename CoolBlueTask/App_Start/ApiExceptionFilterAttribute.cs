using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace CoolBlueTask
{
	public class ApiExceptionFilterAttribute :
		ExceptionFilterAttribute, IAutofacExceptionFilter
	{
		public override void OnException(
			HttpActionExecutedContext httpContext)
		{
			var exception = httpContext.Exception;
			//var exceptionName = exception.GetType().Name;

			var httpStatusCode = HttpStatusCode.InternalServerError;

			if (exception is UnauthorizedAccessException)
			{
				httpStatusCode = HttpStatusCode.Unauthorized;
			}

			throwExceptionWithDebugInfo(exception, httpStatusCode);
		}

		private void throwExceptionWithDebugInfo(
			Exception exception, 
			HttpStatusCode statusCode)
		{
			var ex = new HttpResponseMessage(statusCode)
				{ Content = new StringContent(exception.Message + exception.StackTrace) };
			throw new HttpResponseException(ex);
		}
	}
}