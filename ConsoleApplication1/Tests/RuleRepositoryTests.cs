using Flow.Library;
using Flow.Library.Validation.Rules;
using Xunit;

namespace Flow.Console.Tests
{
    public class RuleRepositoryTests
    {
        [Fact]
        public void type_is_found_and_loaded()
        {
            // assemble
            const string json = "{ Type: \"MaxValue\", Max: 20 }";

            // act
            var rule = RulesRepository.GetRuleInstance(json);

            // assert
            Assert.IsType<MaxValue>(rule);
            Assert.Equal(20, ((MaxValue)rule).Max);
        }

        [Fact]
        public void type_is_found_and_loaded_minvalue()
        {
            // assemble
            const string json = "{ Type: \"MinValue\", Min: 1 }";

            // act
            var rule = RulesRepository.GetRuleInstance(json);

            // assert
            Assert.IsType<MinValue>(rule);
            Assert.Equal(1, ((MinValue)rule).Min);
        }
    }
}
