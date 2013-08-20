using System.Collections.Generic;
using System.Dynamic;
using Flow.Library;
using Flow.Library.Validation;
using Flow.Library.Validation.Rules;
using Xunit;

namespace Flow.Console.Tests
{
    public class ValidationEngineTests
    {
        [Fact]
        public void string_is_validated()
        {
            // assemble
            dynamic instance = new { PropertyOne = "Hello world" };
            var rules = new List<ValidationRule>();
            rules.Add(new ValidationRule { Key = "PropertyOne", Validator = new StringRequired() });

            // act
            var validator = new ValidationEngine(rules, instance);

            // assert
            Assert.True(validator.IsValid);
        }

        [Fact]
        public void empty_string_is_not_allowed()
        {
            // assemble
            dynamic instance = new { PropertyOne = "" };
            var rules = new List<ValidationRule>();
            rules.Add(new ValidationRule { Key = "PropertyOne", Validator = new StringRequired() });

            // act
            var validator = new ValidationEngine(rules, instance);

            // assert
            Assert.False(validator.IsValid);
        }

        [Fact]
        public void string_cannot_exceed_ten_chars()
        {
            // assemble
            var rules = new List<ValidationRule>();
            rules.Add(new ValidationRule { Key = "PropertyOne", Validator = new StringRequired() });
            rules.Add(new ValidationRule { Key = "PropertyOne", Validator = new StringMaxLength(10) });

            // act
            dynamic instance = new ExpandoObject();
            instance.PropertyOne = "1234567890";
            var validator = new ValidationEngine(rules, instance);
            Assert.True(validator.IsValid);

            instance.PropertyOne = "12345678901";
            Assert.False(validator.IsValid);
        }

        [Fact]
        public void broken_rule_collection_contains_lists_of_broken_rules()
        {
            // assemble
            dynamic instance = new ExpandoObject();
            instance.FirstProperty = "aaaaabbbbbc";
            instance.SecondProperty = 1234567890;

            var rules = new List<ValidationRule>
                            {
                                new ValidationRule {Key = "FirstProperty", Validator = new StringRequired()},
                                new ValidationRule {Key = "FirstProperty", Validator = new StringMaxLength(10)},
                                new ValidationRule {Key = "SecondProperty", Validator = new MaxValue(100)}
                            };

            // act
            var validator = new ValidationEngine(rules, instance);
            Assert.False(validator.IsValid);
            Assert.Equal(2, validator.BrokenRules.Count);

            instance.FirstProperty = "aaaaabbbbb";
            Assert.False(validator.IsValid);
            Assert.Equal(1, validator.BrokenRules.Count);

            instance.SecondProperty = 100;
            Assert.True(validator.IsValid);
            Assert.Equal(0, validator.BrokenRules.Count);
        }

        [Fact]
        public void javascript_validator_returns_false()
        {
            // assemble
            var instance = new {PropertyOne = "hello world"};
            var rules = new List<ValidationRule>();
            rules.Add(new ValidationRule {Key = "PropertyOne", Validator = new JavascriptRule {Source = "result = false; "}});
            var validator = new ValidationEngine(rules, instance);

            // act
            var result = validator.IsValid;

            // assert
            Assert.False(result);
            Assert.Equal(1, validator.BrokenRules.Count);
        }

        [Fact]
        public void javascript_validator_returns_true()
        {
            // assemble
            var instance = new { PropertyOne = "hello world" };
            var rules = new List<ValidationRule>();
            rules.Add(new ValidationRule { Key = "PropertyOne", Validator = new JavascriptRule { Source = "result = true; " } });
            var validator = new ValidationEngine(rules, instance);

            // act
            var result = validator.IsValid;

            // assert
            Assert.True(result);
            Assert.Equal(0, validator.BrokenRules.Count);
        }
    }
}
