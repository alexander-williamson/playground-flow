using System;
using System.Text;

namespace Flow.Library.Validation
{
    public class ValidationException : Exception
    {
        private readonly string[] _brokenRules;

        public ValidationException(string[] brokenRules)
        {
            _brokenRules = brokenRules;
        }

        public ValidationException(string brokenRule)
        {
            _brokenRules = new [] { brokenRule };
        }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("Validation Exception: ");
                sb.AppendFormat("{0} broken rules", _brokenRules.Length);
                for(var i = 0; i < _brokenRules.Length; i++)
                {
                    sb.AppendFormat("{0} : {1}", i, _brokenRules[i]);
                }
                sb.AppendFormat("");
                return sb.ToString();
            }
        }
    }
}