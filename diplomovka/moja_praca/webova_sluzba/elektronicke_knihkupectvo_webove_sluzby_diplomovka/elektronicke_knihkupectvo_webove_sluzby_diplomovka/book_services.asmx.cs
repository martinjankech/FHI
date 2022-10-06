using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace elektronicke_knihkupectvo_webove_sluzby_diplomovka
{
    /// <summary>
    /// Summary description for book_services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class book_services : System.Web.Services.WebService
    { private String fileBookInfo = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\book_moje.xml ";
      private String fileBookTransactionInfo = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\book_transakcie_moje.xml ";
        public String fileOutputSingleSearch = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\output.xml";
        public XmlDocument LoadDocument(string docu) {

            XmlDocument doc = new XmlDocument();
            doc.Load(docu);
            return doc;
        }
        public string DecodeFromUtf8(string utf8_String)
        {
            byte[] bytes = Encoding.Default.GetBytes(utf8_String);
            string utf8 = Encoding.UTF8.GetString(bytes);
            return utf8;
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        public void WriteToTheFileWithTimeStamp(string path,XmlNodeList data)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            string x_to_file = "";
            int nodeListCount = data.Count;
            int i = 0;
            sw.Write("<output>" + "\n\n");

            while (i < nodeListCount)
            {
                XmlNode title = data.Item(i);
                //jednotlive hodnoty title elementov su zobrazene aj s prislusnymi znackami z .xml suboru
                x_to_file = title.OuterXml;
                sw.Write("\t" + x_to_file + "\n");
                i++;
            }
            sw.Write("<timestamp>" + "\n\n");
            String timeStamp = GetTimestamp(DateTime.Now);
            sw.Write("\t" + timeStamp + "\n");
            sw.Write("\n</timespamp>");
            sw.Write("\n</output>");
            sw.Close();
        }


        [WebMethod]
        public void SinglebookDataById(string  id)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            XmlNodeList AllBook= doc.SelectNodes("Bookstore/books/book");
            int allBookCount = AllBook.Count;
            // id su radene od 1... preto tato podmienka je postacujuca
            if (Int32.Parse(id)<= allBookCount)
            {
                XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[id=" + id + "]");
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                // nastavenie UTF-8 sady pre http response 
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
                
            }
            else
                Context.Response.Write("nenasiel sa zaznam pre zadane id ");
        }
        
        [WebMethod]
        public void SinglebookDataByName(string name)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            
            
   
            //XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[nazov=\"" + name + "\"]");
            // prekovertujeme name na lowercase kedze to iste spravim aj na frontende a tym padom aj ked pouzivatel zada nazov v inom case tak vrati dobry vysledok 
            XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[translate(nazov,'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ','abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ') = \"" +name+"\"]");

            // string node=  nodeListBook.Item(0).ChildNodes[2].FirstChild.InnerXml;

            //Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
           // Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));

            if (nodeListBook.Item(0) == null)
            {
              Context.Response.StatusCode = 404;
               Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
             Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");

            }
             else
             {//nastavenie UTF-8 sady pre http response 
                 Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
              Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
             }
           
            
        }

        [WebMethod]
        public void SinglebookDataByIsbn(string isbn)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books/book");
            int allBookCount = AllBook.Count;

            XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[isbn=\"" + isbn + "\"]");
            //nastavenie UTF-8 sady pre http response 
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));


            //Context.Response.Write("nenasiel sa zaznam pre zadane id ");
        }
        [WebMethod]
        public void GetListAllBooks()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileBookInfo);
            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books");
            // nastavenie UTF-8 sady pre http response 
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(JsonConvert.SerializeXmlNode(AllBook.Item(0), Formatting.Indented));
        }

    }
}

