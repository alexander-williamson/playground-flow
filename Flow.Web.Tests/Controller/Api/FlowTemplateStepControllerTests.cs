using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Mvc;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Web.Controllers.Api;
using Flow.Web.Dto;
using Xunit;

namespace Flow.Web.Tests.Controller.Api
{
    public class FlowTemplateStepControllerTests
    {

        public static void SetupController(FlowTemplateStepsController controller, string url, HttpMethod method)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(method, url);
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplateStep" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
        }

        public FlowTemplateStepControllerTests()
        {
            AutoMapperConfig.Configure();
        }

        [Fact]
        public async void Get_with_id_should_return_matching_templateStep()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get(2)).Returns(
            new FlowTemplateStep
            {
                Id = 2,
                Name = "Correct Step",
                FlowTemplateId = 1,
                StepTypeId = 1
            });
            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Step/2", HttpMethod.Get);

            var response = sut.Get(2);
            var result = await response.Content.ReadAsAsync<FlowTemplateStepDto>();

            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async void Should_return_404_if_step_does_not_exist()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get(A<int>._)).Returns(null);
            var sut = new FlowTemplateStepsController(unitOfWork);

            var response = sut.Get(2);
            dynamic result = await response.Content.ReadAsAsync<object>();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(null, result);
        }


    }
}
