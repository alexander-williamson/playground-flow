using System.Collections.Specialized;
using FakeItEasy;
using Flow.Web.Configuration;
using Xunit;

namespace Flow.Web.Tests.Configuration
{
    public class WebConfigurationTests
    {
        [Fact]
        public void Should_return_value_from_web_config()
        {
            // Assemble
            const string expected = @"Data Source=.\SQLEXPRESS;Initial Catalog=Example;Integrated Security=True";
            var fake = A.Fake<IConfigurationProvider>();
            A.CallTo(() => fake.ConnectionString("Flow")).Returns(expected);

            // Act
            var sut = new WebConfiguration(fake);
            var result = sut.ConnectionString;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Should_return_value_for_CollectCastleWindsorPerformanceCounters()
        {
            // Assemble
            const bool expected = true;
            var collection = new NameValueCollection {{"CollectCastleWindsorPerformanceCounters", "True"}};
            var fake = A.Fake<IConfigurationProvider>();
            A.CallTo(() => fake.AppSettings).Returns(collection);

            // Act
            var sut = new WebConfiguration(fake);
            var result = sut.CollectCastleWindsorPerformanceCounters;

            // Assert
            Assert.Equal(expected, result);
        }
    }

}