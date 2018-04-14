using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac.Integration.WebApi;

namespace CoolBlueTask.Api.Core
{
	public class Auth0AuthorizationAttribute
		: AuthorizeAttribute, IAutofacAuthorizationFilter
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			//bool skipAuthorization = actionContext.ActionDescriptor
			//	.GetCustomAttributes<AllowAnonymousAttribute>().Any();
			//if (skipAuthorization)
			//{
			//	return;
			//}

			var token = actionContext?.Request?.Headers?.Authorization?.Parameter;

			if (token == null)
			{
				throw new UnauthorizedAccessException();
			}
			if (!IsAuthorized(actionContext))
			{
				throw new UnauthorizedAccessException(token);
			}
		}
	}
}