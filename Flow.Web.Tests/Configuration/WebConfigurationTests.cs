using System.Configuration;
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

            // Act
            var sut = new WebConfiguration();
            var result = sut.ConnectionString;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
