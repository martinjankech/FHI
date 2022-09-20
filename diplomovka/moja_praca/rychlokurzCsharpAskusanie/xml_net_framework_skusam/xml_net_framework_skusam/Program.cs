using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;


namespace xml_net_framework_skusam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\git_repositories\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\xml_net_framework_skusam\\xml_net_framework_skusam\\books.xml");
            string json = JsonConvert.SerializeXmlNode(doc, Formatting.Indented) ;
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
                for (int i = 0; i < countChild; i++) {
                    
                    Console.WriteLine(node.ChildNodes.Item(i).Name + ": " + node.ChildNodes.Item(i).InnerText);
                
                
                }

            }
        }
    }
}

