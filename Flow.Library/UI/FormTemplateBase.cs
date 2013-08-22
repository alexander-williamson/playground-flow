using System.Collections.Generic;
using System.Text;

namespace Flow.Library.UI
{
    public interface IFormTemplate
    {
        int Id { get; set; }
        string Name { get; set; }
        string TemplateHtml { get; set; }
    }

    public class FormTemplateBase : IFormTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateHtml { get; set; }
        public FormTemplateBase()
        {
            Id = 0;
            Name = string.Empty;
            TemplateHtml = GetDefaultTemplateHtml();
        }
        private static string GetDefaultTemplateHtml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<!doctype html>");
            sb.AppendLine("<html>");
            sb.AppendLine("	<head>");
            sb.AppendLine("		<script src=\"https://ajax.googleapis.com/ajax/libs/angularjs/1.0.7/angular.min.js\"></script>");
            sb.AppendLine("	</head>");
            sb.AppendLine("	<body ng-app>");
            sb.AppendLine("		<form action=\"Submit\" method=\"POST\">");
            sb.AppendLine("			<label>Name:</label>");
            sb.AppendLine("            <input type=\"text\" ng-model=\"yourName\" placeholder=\"Enter a name here\">");
            sb.AppendLine("			   <hr>");
            sb.AppendLine("			<h1>You will submit yourname={{yourName}}!</h1>");
            sb.AppendLine("		</div>");
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
            Html = template.TemplateHtml;
        }
    }

}