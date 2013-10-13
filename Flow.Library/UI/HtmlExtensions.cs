using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Flow.Library.UI
{
    public static class HtmlExtensions
    {
        public static string Prettify(this string content)
        {
            // adapted from http://stackoverflow.com/questions/1123718/format-xml-string-to-print-friendly-xml-string
            try
            {
                var mStream = new MemoryStream();
                var writer = new XmlTextWriter(mStream, Encoding.Unicode) {Formatting = Formatting.Indented};
                var document   = new XmlDocument();

                // Load the XmlDocument with the XML.
                document.LoadXml(content);
                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Rewind the MemoryStream in order to read its contents.
                mStream.Position = 0;

                var sReader = new StreamReader(mStream);
                return sReader.ReadToEnd();
            } 
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred in FormatHtml. Message: " + ex.Message);
                Console.WriteLine(ex);
                return content;
            }
        }
    }
}
