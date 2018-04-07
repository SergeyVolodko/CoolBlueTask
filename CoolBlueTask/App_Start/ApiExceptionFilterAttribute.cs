using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace CoolBlueTask
{
	public class ApiExceptionFilterAttribute :
		ExceptionFilterAttribute, IAutofacExceptionFilter
	{
	}
}