using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace skusam_xml
{
    /// <summary>
    /// Summary description for jsonOutput
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class jsonOutput : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearJson() {
            XmlDocument xDocument = new XmlDocument();
            xDocument.Load("D:\\git_repositories\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\skusam_xml\\skusam_xml\\books.xml");
            xDocument.RemoveChild(xDocument.FirstChild);
            var builder = new StringBuilder();
            JsonSerializer.Create().Serialize(new CustomJsonWriter(new StringWriter(builder)), xDocument);
            var serialized = builder.ToString();
            Context.Response.Write(serialized);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void  HelloWorld()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\skusam_xml\\skusam_xml\\books.xml");
            doc.RemoveChild(doc.FirstChild);
            string json = JsonConvert.SerializeXmlNode(doc, Formatting.Indented);
            var details = JObject.Parse(json);

            Console.WriteLine(json);
            Console.WriteLine(details);

            XmlNodeList nodeList = doc.SelectNodes("bookstore/book");
            int countRootNodes = nodeList.Count;
            // Console.WriteLine(nodeList.Item(0).ChildNodes.Count);
            int j = 1;
            foreach (XmlNode node in nodeList)

            {
                Console.WriteLine("kniha cislo :  " + j);
                j++;
                int countChild = node.ChildNodes.Count;
                for (int i = 0; i < countChild; i++)
                {

                    Console.WriteLine(node.ChildNodes.Item(i).Name + ": " + node.ChildNodes.Item(i).InnerText);


                }

            }
            Context.Response.Write(JsonConvert.SerializeXmlNode(doc, Formatting.Indented));
            //return 0;
        }
    }
}
    

