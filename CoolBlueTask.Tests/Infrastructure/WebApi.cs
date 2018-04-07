using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using NSubstitute;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;

namespace CoolBlueTask.Tests.Infrastructure
{
	public static class WebApi
	{
		public static RouteInfo RouteRequest(HttpConfiguration config, HttpRequestMessage request)
		{
			// create context
			var controllerContext = new HttpControllerContext(config, Substitute.For<IHttpRouteData>(), request);

			// get route data
			var routeData = config.Routes.GetRouteData(request);
			RemoveOptionalRoutingParameters(routeData.Values);

			request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
			controllerContext.RouteData = routeData;

			// get controller type
			var controllerDescriptor = new DefaultHttpControllerSelector(config).SelectController(request);
			controllerContext.ControllerDescriptor = controllerDescriptor;

			// get action name
			var actionMapping = new ApiControllerActionSelector().SelectAction(controllerContext);

			return new RouteInfo
			{
				Controller = controllerDescriptor.ControllerType,
				Action = actionMapping.ActionName
			};
		}

		private static void RemoveOptionalRoutingParameters(IDictionary<string, object> routeValues)
		{
			var optionalParams = routeValues
				.Where(x => x.Value == RouteParameter.Optional)
				.Select(x => x.Key)
				.ToList();

			foreach (var key in optionalParams)
			{
				routeValues.Remove(key);
			}
		}

		public static HttpActionExecutedContext MockContextWithException(Exception exception)
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"http://localhost/test")
			};
			var actionContext = new HttpActionContext
			{
				ControllerContext = new HttpControllerContext
				{
					Request = request
				}
			};
			return new HttpActionExecutedContext(actionContext, exception);
		}
	}

	public class RouteInfo
	{
		public Type Controller { get; set; }

		public string Action { get; set; }
	}
}
