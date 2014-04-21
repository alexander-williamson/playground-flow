using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using FakeItEasy;
using Flow.Library.Data;
using Flow.Library.Steps;
using Flow.Library.Validation;
using Flow.Web.Controllers.Api;
using Flow.Web.Dto;
using Xunit;
using FlowTemplate = Flow.Library.Core.FlowTemplate;
using FlowTemplateStep = Flow.Library.Core.FlowTemplateStep;
using RouteParameter = System.Web.Http.RouteParameter;

namespace Flow.Web.Tests.Controller.Api
{
    public class FlowTemplateControllerTests
    {
        private readonly FlowTemplatesController _sut;
        private readonly IFlowTemplateService _flowTemplateService;

        public FlowTemplateControllerTests()
        {
            Library.Configuration.AutoMapperConfig.Configure();
            AutoMapperConfig.Configure();
            _flowTemplateService = A.Fake<IFlowTemplateService>();
            _sut = new FlowTemplatesController(_flowTemplateService);
        }

        [Fact]
        public void Post_should_add_template()
        {
            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            controller.Post(new FlowTemplateDto { Id = 1, Name = "Test 1" });

            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Post_success_should_return_new_id()
        {
            // Assemble
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).Returns(3);
            
            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
            
            // Act
            var postResult = controller.Post(new FlowTemplateDto { Id = 1, Name = "Test 1" });
            var readAsAsyncTask = postResult.Content.ReadAsAsync<ExpandoObject>();
            Task.WaitAll(readAsAsyncTask);
            dynamic result = readAsAsyncTask.Result;

            // Assert
            Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public void Post_Should_save_supported_child_steps()
        {
            // Assemble
            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "StartStep"},
                    new FlowTemplateStepDto {StepTypeName = "StopStep"}
                }
            };

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            // Assert
            controller.Post(instance);

            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Post_Should_store_StartStep_type()
        {
            // assemble
            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "StartStep"},
                }
            };

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);


            // act
            controller.Post(instance);

            // assert
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Post_Should_store_StopStep_type()
        {
            // assemble
            FlowTemplate captured = null;
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._))
                .Invokes((FlowTemplate item) => captured = item);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "StopStep"},
                }
            };

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);


            // act
            controller.Post(instance);

            // assert
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.IsType<StopStep>(captured.Steps[0]);
        }

        [Fact]
        public void Post_Should_store_CollectDataStep_type()
        {
            // assemble
            FlowTemplate captured = null;
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._))
                .Invokes((FlowTemplate item) => captured = item);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "CollectDataStep"},
                }
            };

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            // act
            controller.Post(instance);

            // assert
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.IsType<CollectDataStep>(captured.Steps[0]);
        }

        [Fact]
        public void Post_should_store_StoreDataStep_type()
        {
            // assemble
            FlowTemplate captured = null;
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._))
                .Invokes((FlowTemplate item) => captured = item);

            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "StoreDataStep"},
                }
            };

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            // act
            controller.Post(instance);

            // assert
            A.CallTo(() => _flowTemplateService.Add(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.IsType<StoreDataStep>(captured.Steps[0]);
        }

        [Fact]
        public void Post_should_not_store_unsupported_Step()
        {
            // assemble
            var instance = new FlowTemplateDto
            {
                Name = "Example Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {StepTypeName = "NonExistantStep"},
                }
            };

            // assert
            Assert.Throws<NotSupportedException>(() => _sut.Post(instance));
        }

        [Fact]
        public void Should_return_flow_template_by_id()
        {
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(2)).Returns(new FlowTemplate {Id = 2, Name = "Template 2"});

            var result = _sut.Get(2);

            Assert.Equal(2, result.Id);
            Assert.Equal("Template 2", result.Name);
        }

        [Fact]
        public void Should_return_flow_steps_when_getting_single_flow()
        {
            AutoMapperConfig.Configure();
            Library.Configuration.AutoMapperConfig.Configure();
            var steps = new List<IStep>
            {
                // TODO fix inheritance here
                new StartStep { Id = 1, Name = "Start Step 1"},
                new CollectDataStep { Id = 2, Name = "Collect Data 1"},
                new StopStep { Id = 3, Name = "Stop Step 3"}
            };

            A.CallTo(() => _flowTemplateService.GetFlowTemplateSteps(1)).Returns(steps);
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(1)).Returns(new FlowTemplate { Id = 1, Name = "Template 1", Steps = steps});

            var result = _sut.Get(1);
            Assert.Equal(3, result.Steps.Count);
        }

        [Fact]
        public void Put_template_should_update_template()
        {
            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
            
            // Act
            controller.Put(new FlowTemplateDto
            {
                Id = 2,
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });

            // Assert
            A.CallTo(() => _flowTemplateService.Update(A<FlowTemplate>._)).MustHaveHappened();
        }

        [Fact]
        public void Put_new_template_step_should_create_template_step()
        {
            // Assemble
            A.CallTo(() => _flowTemplateService.GetFlowTemplateSteps(A<int>._)).Returns(null);
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(A<int>._)).Returns(new FlowTemplate { Id = 1 });
            
            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            // Act
            controller.Put(new FlowTemplateDto
            {
                Id = 2,
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });
            A.CallTo(() => _flowTemplateService.Update(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Put_existing_template_step_should_update_template_step()
        {

            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            
            controller.Put(new FlowTemplateDto
            {
                Id = 1,
                Name = "Example Template Step",
                Steps = new List<FlowTemplateStepDto>
                {
                    new FlowTemplateStepDto {Id = 10, Name = "Updated Step", StepTypeName = "StartStep" }
                }
            });

            A.CallTo(() => _flowTemplateService.Update(A<FlowTemplate>._)).MustHaveHappened();
        }

        [Fact]
        public void Put_should_return_validation_messages_if_validation_exception_thrown()
        {
            // Assemble
            A.CallTo(() => _flowTemplateService.Update(A<FlowTemplate>._)).Throws(new ValidationException("Validation Message"));

            // Act
            var controller = new FlowTemplatesController(_flowTemplateService);
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/FlowTemplates");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "FlowTemplates" } });
            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            // act
            var response = controller.Put(new FlowTemplateDto {Id = 1, Name = "NonExistant"});

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var content = (ObjectContent) response.Content;
            var value = content.Value;
            Assert.Equal("Message", ((HttpError)value).First().Key);
            Assert.Contains("Validation", ((HttpError)value).First().Value.ToString());
        }

        [Fact]
        public void Delete_should_return_not_found_if_not_exists()
        {
            // Assemble
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(A<int>._)).Returns(null);

            // Act
            var controller = new FlowTemplatesController(_flowTemplateService);
            
            // Assert
            Assert.Throws<HttpResponseException>(() => controller.Delete(100));
        }

        [Fact]
        public void Delete_should_request_item_to_be_deleted_from_repository()
        {
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(A<int>._)).Returns(new FlowTemplate {Id = 1, Name = "Example"});
            A.CallTo(() => _flowTemplateService.GetFlowTemplateSteps(A<int>._)).Returns(new List<FlowTemplateStep>
            {
                new FlowTemplateStep {Id = 10, FlowTemplateId = 1},
                new FlowTemplateStep {Id = 20, FlowTemplateId = 1},
                new FlowTemplateStep {Id = 30, FlowTemplateId = 1}
            });

            // Act
            var controller = new FlowTemplatesController(_flowTemplateService);
            controller.Delete(1);

            // Assert
            A.CallTo(() => _flowTemplateService.Delete(A<FlowTemplate>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Successful_delete_should_return_200()
        {
            A.CallTo(() => _flowTemplateService.GetFlowTemplate(A<int>._)).Returns(new FlowTemplate { Id = 1, Name = "Example" });

            // Act
            var controller = new FlowTemplatesController(_flowTemplateService);
            var result = controller.Delete(1);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
