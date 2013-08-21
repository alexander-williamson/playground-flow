//using System.Collections.Generic;
//using ConsoleApplication1.Library;
//using Flow.Library;
//using Xunit;

//namespace ConsoleApplication1.Tests
//{
//    public class FormInstanceTests
//    {
//        [Fact]
//        public void form_arguments_is_initilised_empty()
//        {
//            // assemble
//            var instance = new FormProvider();

//            // assert
//            Assert.Equal(0, instance.Arguments.Count);
//            Assert.Equal(0, instance.Variables.Count);
//        }

//        [Fact]
//        public void form_instance_arguments_persists()
//        {
//            // assemble
//            var arguments = new Dictionary<string, object>
//                                {
//                                    {"FirstName", string.Empty},
//                                    {"LastName", string.Empty},
//                                    {"Telephone", string.Empty},
//                                    {"Memo", string.Empty}
//                                };
//            var instance = new FormProvider(arguments);

//            // act
//            instance.();

//            // assert
//            Assert.Equal(4, instance.Arguments.Count);
//            Assert.Equal(string.Empty, instance.Arguments["FirstName"]);
//            Assert.Equal(string.Empty, instance.Arguments["LastName"]);
//            Assert.Equal(string.Empty, instance.Arguments["Telephone"]);
//            Assert.Equal(string.Empty, instance.Arguments["Memo"]);

//            Assert.Equal(4, instance.Variables.Count);
//            Assert.Equal(string.Empty, instance.Variables["FirstName"]);
//            Assert.Equal(string.Empty, instance.Variables["LastName"]);
//            Assert.Equal(string.Empty, instance.Variables["Telephone"]);
//            Assert.Equal(string.Empty, instance.Variables["Memo"]);
//        }

//        [Fact]
//        public void form_instance_arguments_do_not_change()
//        {
//            // assemble
//            var arguments = new Dictionary<string, object>
//                                {
//                                    {"FirstName", string.Empty},
//                                    {"LastName", string.Empty},
//                                    {"Telephone", string.Empty},
//                                    {"Memo", string.Empty}
//                                };
//            var instance = new FormProvider(arguments);

//            // act
//            instance.Variables["FirstName"] = "AAA";
//            instance.Variables["LastName"] = "BBB";
//            instance.Variables["Telephone"] = "CCC";
//            instance.Variables["Memo"] = "DDD";

//            // assert
//            Assert.Equal(4, instance.Arguments.Count);
//            Assert.Equal(string.Empty, instance.Arguments["FirstName"]);
//            Assert.Equal(string.Empty, instance.Arguments["LastName"]);
//            Assert.Equal(string.Empty, instance.Arguments["Telephone"]);
//            Assert.Equal(string.Empty, instance.Arguments["Memo"]);

//            Assert.Equal(4, instance.Variables.Count);
//            Assert.Equal("AAA", instance.Variables["FirstName"]);
//            Assert.Equal("BBB", instance.Variables["LastName"]);
//            Assert.Equal("CCC", instance.Variables["Telephone"]);
//            Assert.Equal("DDD", instance.Variables["Memo"]);
//        }
//    }
//}