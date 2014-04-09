using System.Web.Http;
using Flow.Web.Controllers;
using Flow.Web.Controllers.Api;

namespace Flow.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "DefaultApi", 
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "FlowTemplateSteps",
                "api/FlowTemplates/{parent}/Steps/{id}",
                new
                {
                    controller = "FlowTemplateSteps",
                    id = RouteParameter.Optional
                });
        }
    }
}
