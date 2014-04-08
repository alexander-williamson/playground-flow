using System.Web.Http;

namespace Flow.Web.Controllers.Api
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "hello world";
        }

    }
}
