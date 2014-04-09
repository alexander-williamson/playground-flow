using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Flow.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NullResponseIs404Attribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if ((actionExecutedContext.Response != null) && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                object contentValue;
                actionExecutedContext.Response.TryGetContentValue(out contentValue);
                if (contentValue == null)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Object not found");
                }
            }
        }

    }
}