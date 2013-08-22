using System;
using System.Reflection;
using Flow.Library.Validation.Rules;
using Newtonsoft.Json;

namespace Flow.Library.Validation
{
    public class JsonRuleFactory
    {
        public static IRule GetRuleInstance(string json)
        {
            dynamic element = JsonConvert.DeserializeObject(json);
            String ruleTypeName = null;
            Type ruleType = null;

            // try and get the type using the value from json
            try
            {
                ruleTypeName = element.Type;
                ruleType = Type.GetType(ruleTypeName);
            }
            catch
            {
                Console.WriteLine("Unable to read type");
            }

            if (ruleType == null)
            {
                // if couldn't find it, it's probably because the full name wasn't specified
                // give the rules namespace as a hint
                try
                {
                    ruleTypeName = "Flow.Library.Validation.Rules." + element.Type;
                    ruleType = Type.GetType(ruleTypeName);
                }
                catch
                {
                    Console.WriteLine("Unable to read type");
                }
            }

            if (string.IsNullOrWhiteSpace(ruleTypeName))
                ruleTypeName = typeof(DefaultRule).Name;

            if (ruleType == null)
                throw new InvalidCastException("Unable to find Rule Type: " + ruleTypeName);

            var ruleinstance = (IRule)Activator.CreateInstance(ruleType);

            foreach (var propertyElement in ruleinstance.GetType().GetProperties())
            {
                var retrievedValue = element[propertyElement.Name];
                if (retrievedValue != null)
                {
                    var value = Convert.ChangeType(retrievedValue.Value, propertyElement.PropertyType);
                    var property = ruleinstance.GetType().GetProperty(propertyElement.Name, BindingFlags.Public | BindingFlags.Instance);
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(ruleinstance, value, null);
                    }
                }
            }

            return ruleinstance;

        }

        public static T Cast<T>(object o)
        {
            return (T) o;
        }

    }
}
