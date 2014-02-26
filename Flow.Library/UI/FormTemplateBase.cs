using System.Collections.Generic;
using System.Text;
using Flow.Library.Validation;
using Newtonsoft.Json;

namespace Flow.Library.UI
{
    public class FormTemplateBase : IFormTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Head { get; set; }
        public string Html { get; set; }
        public List<ValidationRule> BrokenRules { get; set; } 

        public FormTemplateBase(List<ValidationRule> brokenRules)
        {
            BrokenRules = brokenRules ?? new List<ValidationRule>(); ;
            Id = 0;
            Name = string.Empty;
            Head = GetHead();
            Body = GetBody();
            Html = GetHtml(Body, Head);
            Html = Html.Prettify();
        }

        private string GetHead()
        {
            var sb = new StringBuilder();
            sb.AppendLine("		<script src=\"https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js\"></script>");

            sb.AppendLine("<script type\"text/javascript\">");
            var json = JsonConvert.SerializeObject(BrokenRules);
            sb.AppendFormat("var brokenRules = {0};", json).AppendLine();
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        private string GetBody()
        {
            var sb = new StringBuilder();
            sb.AppendLine("		<form ng-app action=\"Submit\" method=\"POST\">");
            sb.AppendLine("			<label>Name:</label>");
            sb.AppendLine("            <input name=\"yourName\" type=\"text\" ng-model=\"yourName\" placeholder=\"Enter a name here\">");
            sb.AppendLine("			   <hr>");
            sb.AppendLine("			<h1>You will submit yourname={{yourName}}!</h1>");
            sb.AppendLine("		</form>");
            return sb.ToString();
        }

        private string GetHtml(string head, string body)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<!doctype html>");
            sb.AppendLine("<html>");
            sb.AppendLine("	<head>");
            sb.AppendLine(head);
            sb.AppendLine("	</head>");
            sb.AppendLine("	<body>");
            sb.AppendLine(body);
            sb.AppendLine("	</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

    }

    public class FormInstance
    {
        public int Id { get; set; }
        public string Html { get; set; }
        public bool IsBindable { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public FormInstance(IFormTemplate template)
        {
            Id = 0;
            Html = template.Body;
        }
    }

}