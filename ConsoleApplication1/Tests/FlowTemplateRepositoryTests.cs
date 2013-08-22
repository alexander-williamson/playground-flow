//using System.Linq;
//using Flow.Library;
//using Flow.Library.Data.Models;
//using Flow.Library.Data.Repositories;
//using Flow.Library.Steps;
//using Xunit;
//using FakeItEasy;

//namespace Flow.Console.Tests
//{
//    public class FlowTemplateRepositoryTests
//    {
//        [Fact]
//        public void repository_returns_full_template()
//        {
//            // assemble
//            var fake = A.Fake<IFlowTemplateRepository>();
//            A.CallTo(() => fake.GetFlowTemplate(A<int>._)).Returns(new FlowTemplateDataModel { Id = 100, Name = "Example Flow Template" });
//            A.CallTo(() => fake.GetFlowTemplateStepsForTemplate(A<int>._)).Returns(new[] { 
//                new FlowTemplateStepDataModel { FlowTemplateId = 101, Id = 200, Name = "Example Step 1", Type = "Collect Data Step 1" },
//                new FlowTemplateStepDataModel { FlowTemplateId = 102, Id = 201, Name = "Example Step 2", Type = "Collect Data Step 2" }
//                                                                                       });
//            A.CallTo(() => fake.GetFlowTemplateStepRulesForStep(A<int>._)).Returns(new[] {
//                                                                                           new FlowTemplateStepRuleDataModel { Id = 300, Source = "Example Source 1" }, 
//                                                                                           new FlowTemplateStepRuleDataModel { Id = 301, Source = "Example Source 2" }
//                                                                                       });
//            var factory = new FlowTemplateFactory(fake);

//            // act
//            var instance = factory.GetTemplate(1);

//            // assert
//            Assert.Equal(100, instance.Id);
//            Assert.Equal("Example Flow Template", instance.Name);
//            Assert.Equal(2, instance.Steps.Count());

//            var step1 = (DataCollectionStep) instance.Steps.ToList()[0];
//            Assert.Equal(200, step1.Id);
//            Assert.Equal("Example Step 1", step1.Name);
//            Assert.Equal("Collect Data Step 1", step1.Type);

//            Assert.Equal(2, step1.Rules.Count());
//            Assert.Equal(300, step1.Rules.First().Id);
//            Assert.Equal(200, step1.Rules.First().FlowTemplateStepId);
//            Assert.Equal("Example Source 1", step1.Rules.First().Source);
//            Assert.Equal(301, step1.Rules.Last().Id);
//            Assert.Equal(200, step1.Rules.Last().FlowTemplateStepId);
//            Assert.Equal("Example Source 2", step1.Rules.Last().Source);

//            var step2 = instance.Steps.ToList()[1];
//            Assert.Equal(201, step2.Id);
//            Assert.Equal("Example Step 2", step2.Name);
//            Assert.Equal("Collect Data Step 2", step2.Type);

//            Assert.Equal(2, step2.Rules.Count());
//            Assert.Equal(300, step2.Rules.First().Id);
//            Assert.Equal(201, step2.Rules.First().FlowTemplateStepId);
//            Assert.Equal("Example Source 1", step2.Rules.First().Source);
//            Assert.Equal(301, step2.Rules.Last().Id);
//            Assert.Equal(201, step2.Rules.Last().FlowTemplateStepId);
//            Assert.Equal("Example Source 2", step2.Rules.Last().Source);


//        }
//    }
//}
