using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using FakeItEasy;
using Flow.Library.Core;
using Flow.Library.Data.Abstract;
using Flow.Library.Validation;
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
            Library.Configuration.AutoMapperConfig.Configure();
            AutoMapperConfig.Configure();
        }

        [Fact]
        public void Get_FlowTemplateSteps_should_return_steps_matching_parent()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(
                new List<FlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 1, Name = "Correct Step 1", FlowTemplateId = 1, StepTypeId = 1},
                    new FlowTemplateStep {Id = 2, Name = "Incorrect Step", FlowTemplateId = 2, StepTypeId = 1},
                    new FlowTemplateStep {Id = 3, Name = "Correct Step 3", FlowTemplateId = 1, StepTypeId = 2}
                });
            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Get);

            var response = sut.Get(1);
            var task = response.Content.ReadAsAsync<List<FlowTemplateStepDto>>();
            task.Wait();
            var result = task.Result;

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void Should_return_404_if_step_does_not_exist()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get(A<int>._)).Returns(null);
            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Get);

            var response = sut.Get(2);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(null, response.Content);
        }

        [Fact]
        public void Should_return_step_matching_parent_and_id()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(
                new List<FlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 1, Name = "Correct Step 1", FlowTemplateId = 1, StepTypeId = 1},
                    new FlowTemplateStep {Id = 2, Name = "Incorrect Step", FlowTemplateId = 2, StepTypeId = 1},
                    new FlowTemplateStep {Id = 3, Name = "Correct Step 3", FlowTemplateId = 1, StepTypeId = 2}
                });

            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps/2", HttpMethod.Get);

            var response = sut.Get(1, 3);
            var task = response.Content.ReadAsAsync<FlowTemplateDto>();
            task.Wait();
            var result = task.Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public void Should_return_404_if_step_does_not_exist_on_parent()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(
                new List<FlowTemplateStep>
                {
                    new FlowTemplateStep {Id = 1, Name = "Correct Step 1", FlowTemplateId = 1, StepTypeId = 1},
                    new FlowTemplateStep {Id = 2, Name = "Incorrect Step", FlowTemplateId = 2, StepTypeId = 1},
                    new FlowTemplateStep {Id = 3, Name = "Correct Step 3", FlowTemplateId = 1, StepTypeId = 2}
                }); var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Get);

            var response = sut.Get(3, 3);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(null, response.Content);
        }

        [Fact]
        public void Should_return_404_if_step_does_not_exist_when_trying_to_delete()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get(A<int>._)).Returns(null);
            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Delete);

            var response = sut.Delete(3, 3);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(null, response.Content);
        }

        [Fact]
        public void Should_return_200_if_step_is_deleted()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get())
                .Returns(new List<IFlowTemplateStep> {
                    new FlowTemplateStep {Id = 3, FlowTemplateId = 3} });
            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Delete);

            var response = sut.Delete(3, 3);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(null, response.Content);
        }

         [Fact]
        public void FlowTemplateStep_Post_should_return_bad_request_if_service_throws_validation_error()
        {
            // Assemble
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Throws(new ValidationException("Example Validation Message"));

            var sut = new FlowTemplateStepsController(unitOfWork);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Post);

            // Act
             var instance = new FlowTemplateStepDto {StepTypeName = "StartStep"};
            var response = sut.Post(3, instance);

            var task = response.Content.ReadAsAsync<ExpandoObject>();
            task.Wait();
            dynamic result = task.Result;

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Example Validation Message", result.BrokenRules[0]);
            Assert.Equal(1, result.BrokenRules.Count);
        }   
        
        [Fact]
        public void FlowTemplateService_Post_should_add_StartStep_values()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(new List<IFlowTemplateStep>
            {
                new FlowTemplateStep { Id = 10 }
            });
            var sut = new FlowTemplateStepsController(unitOfWork);
            IFlowTemplateStep captured = null;
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Post);

            // Act
            var instance = new FlowTemplateStepDto {Name = "New StartStep", StepTypeName = "StartStep"};
            var response = sut.Post(3, instance);

            var task = response.Content.ReadAsAsync<ExpandoObject>();
            task.Wait();
            dynamic result = task.Result;

            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(11, result.Id);
            Assert.Equal(captured.Id, result.Id);
            Assert.Equal(3, captured.FlowTemplateId);
            Assert.Equal(1, captured.StepTypeId);
            Assert.Equal("New StartStep", captured.Name);
        }

        [Fact]
        public void FlowTemplateService_Post_should_add_StopStep_values()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(new List<IFlowTemplateStep>
            {
                new FlowTemplateStep { Id = 10 }
            });
            var sut = new FlowTemplateStepsController(unitOfWork);
            IFlowTemplateStep captured = null;
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Post);

            // Act
            var instance = new FlowTemplateStepDto { Name = "New StopStep", StepTypeName = "StopStep" };
            var response = sut.Post(3, instance);

            var task = response.Content.ReadAsAsync<ExpandoObject>();
            task.Wait();
            dynamic result = task.Result;

            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(11, result.Id);
            Assert.Equal(3, captured.FlowTemplateId);
            Assert.Equal(2, captured.StepTypeId);
            Assert.Equal("New StopStep", captured.Name);
        }

        [Fact]
        public void FlowTemplateService_Post_should_add_CollectDataStep_values()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(new List<IFlowTemplateStep>
            {
                new FlowTemplateStep { Id = 10 }
            });
            var sut = new FlowTemplateStepsController(unitOfWork);
            IFlowTemplateStep captured = null;
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Post);

            // Act
            var instance = new FlowTemplateStepDto { Name = "New CollectDataStep", StepTypeName = "CollectDataStep" };
            var response = sut.Post(3, instance);

            var task = response.Content.ReadAsAsync<ExpandoObject>();
            task.Wait();
            dynamic result = task.Result;

            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(11, result.Id);
            Assert.Equal(3, captured.FlowTemplateId);
            Assert.Equal(3, captured.StepTypeId);
            Assert.Equal("New CollectDataStep", captured.Name);
        }

        [Fact]
        public void FlowTemplateService_Post_should_add_StoreDataStep_values()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).Returns(new List<IFlowTemplateStep>
            {
                new FlowTemplateStep { Id = 10 }
            });
            var sut = new FlowTemplateStepsController(unitOfWork);
            IFlowTemplateStep captured = null;
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._))
                .Invokes((IFlowTemplateStep item) => captured = item);
            SetupController(sut, "http://example.com/api/FlowTemplates/1/Steps", HttpMethod.Post);

            // Act
            var instance = new FlowTemplateStepDto { Name = "New StoreDataStep", StepTypeName = "StoreDataStep" };
            var response = sut.Post(3, instance);

            var task = response.Content.ReadAsAsync<ExpandoObject>();
            task.Wait();
            dynamic result = task.Result;

            A.CallTo(() => unitOfWork.FlowTemplateSteps.Get()).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.FlowTemplateSteps.Add(A<IFlowTemplateStep>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => unitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(11, result.Id);
            Assert.Equal(3, captured.FlowTemplateId);
            Assert.Equal(4, captured.StepTypeId);
            Assert.Equal("New StoreDataStep", captured.Name);
        }

    }
}
