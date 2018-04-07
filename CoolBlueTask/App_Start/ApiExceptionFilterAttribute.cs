using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace CoolBlueTask.App_Start
{
	public class ApiExceptionFilterAttribute :
		ExceptionFilterAttribute, IAutofacExceptionFilter
	{
	}
}