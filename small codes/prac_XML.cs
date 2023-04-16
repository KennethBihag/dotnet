using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ConsoleApp1
{
    internal class XML_prac
    {
        public static void Main(string [] args)
        {
            string xmlStr = File.ReadAllText("Test.xml");
            NameTable nt = new NameTable();
            XmlNamespaceManager nsm = new XmlNamespaceManager(nt);
            nsm.AddNamespace("native", "Native.com");
            XmlParserContext context;
            //String subset = "<!ENTITY h 'hardcover'>";
            context = new XmlParserContext(nt, nsm, "RootElem", null, null, null, null, null, XmlSpace.None);
            XmlTextReader reader = new XmlTextReader(xmlStr, XmlNodeType.Element, context);
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(reader);
            Console.WriteLine(xdoc.OuterXml);
        }
    }
}
