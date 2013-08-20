//using System.Collections.Generic;
//using ConsoleApplication1.Library;
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
//            Assert.Equal(0, instance.Values.Count);
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
//            instance.Process();

//            // assert
//            Assert.Equal(4, instance.Arguments.Count);
//            Assert.Equal(string.Empty, instance.Arguments["FirstName"]);
//            Assert.Equal(string.Empty, instance.Arguments["LastName"]);
//            Assert.Equal(string.Empty, instance.Arguments["Telephone"]);
//            Assert.Equal(string.Empty, instance.Arguments["Memo"]);

//            Assert.Equal(4, instance.Values.Count);
//            Assert.Equal(string.Empty, instance.Values["FirstName"]);
//            Assert.Equal(string.Empty, instance.Values["LastName"]);
//            Assert.Equal(string.Empty, instance.Values["Telephone"]);
//            Assert.Equal(string.Empty, instance.Values["Memo"]);
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
//            instance.Values["FirstName"] = "AAA";
//            instance.Values["LastName"] = "BBB";
//            instance.Values["Telephone"] = "CCC";
//            instance.Values["Memo"] = "DDD";
            
//            // assert
//            Assert.Equal(4, instance.Arguments.Count);
//            Assert.Equal(string.Empty, instance.Arguments["FirstName"]);
//            Assert.Equal(string.Empty, instance.Arguments["LastName"]);
//            Assert.Equal(string.Empty, instance.Arguments["Telephone"]);
//            Assert.Equal(string.Empty, instance.Arguments["Memo"]);

//            Assert.Equal(4, instance.Values.Count);
//            Assert.Equal("AAA", instance.Values["FirstName"]);
//            Assert.Equal("BBB", instance.Values["LastName"]);
//            Assert.Equal("CCC", instance.Values["Telephone"]);
//            Assert.Equal("DDD", instance.Values["Memo"]);
//        }
//    }
//}