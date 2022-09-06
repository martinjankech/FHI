using System;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ParsingXml
{
    class Program
    {
        
        static void Main(string[] args)
        {/*
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[2].ChildNodes[0].ChildNodes)
                Console.WriteLine(xmlNode.Attributes["currency"].Value + ": " + xmlNode.Attributes["rate"].Value);
            Console.ReadKey();
            */
            /*
            XmlDocument Doc = new XmlDocument();
            
            Doc.Load("D:\\git_repositories\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\SkusamXML\\SkusamXML\\books.xml");
           XmlNodeList node = Doc.SelectNodes("/bookstore/book/*");
            foreach (XmlNode nod in node)
            { Console.WriteLine( nod.Name + nod.InnerText); }
           */

            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\git_repositories\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\SkusamXML\\SkusamXML\\products.xml");

            textBox1.Text += "Products:" + Environment.NewLine;

            XmlNodeList productNodeList = doc.SelectNodes("/Format/Product");
            foreach (XmlNode productNode in productNodeList)
            {
                textBox1.Text += "".PadRight(4) + productNode.FirstChild.Name + " : " + productNode.FirstChild.InnerText + Environment.NewLine;
            }

            textBox1.Text += Environment.NewLine + "Properties:" + Environment.NewLine;
            XmlNodeList PropertyNodeList = doc.SelectNodes("/Format/Property");
            foreach (XmlNode propertyNode in PropertyNodeList)
            {
                textBox1.Text += "".PadRight(4) + propertyNode.Name + " : " + Environment.NewLine;
                foreach (XmlNode propertyChild in propertyNode.ChildNodes)
                {
                    textBox1.Text += "".PadRight(8) + propertyChild.Name + " : " + propertyChild.InnerText + Environment.NewLine;
                }
            }


        }
    }
}