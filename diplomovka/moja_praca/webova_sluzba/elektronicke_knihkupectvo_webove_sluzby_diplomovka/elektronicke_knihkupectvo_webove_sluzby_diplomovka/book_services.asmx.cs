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
    { private String fileBookInfo = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_moje.xml ";
      private String fileBookTransactionInfo = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_transakcie_moje.xml ";
        public String fileOutputSingleSearch = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\output.xml";
        public XmlDocument LoadDocument(string docu) {
            XmlDocument doc = new XmlDocument();
            doc.Load(docu);
            return doc;
        }
        //public string DecodeFromUtf8(string utf8_String)
        //{
        //    byte[] bytes = Encoding.Default.GetBytes(utf8_String);
        //    string utf8 = Encoding.UTF8.GetString(bytes);
        //    return utf8;
        //}

        //zatial nedokoncena
        //public void SavetoRootSingleSearchedBook(string Path, XmlNodeList data)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(Path);
        //    XmlNode root = doc.DocumentElement;
        //    var items = doc.GetElementsByTagName("testsuite");

        //    for (int i = 0; i < data.Count; i++)
        //    {// tu prenasame data medzi dvoma xml dokumentami preto musime pouzit metodu importNode
        //        XmlNode newdata = doc.ImportNode(data.Item(i), true);
        //        root.AppendChild(newdata);
        //    }

        //    doc.Save(Path);

        //}
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd-HH-mm-ss");
        }
        // zapise data o vsetkych hladaniach do jedneho suboru aj s casovou peciatkov 
        public void WriteToTheFileWithTimeStamp(string path,XmlNodeList data)
        {
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            string x_to_file = "";
            int nodeListCount = data.Count;
            int i = 0;
            sw.Write("\n"+"<output>" + "\n\n");

            while (i < nodeListCount)
            {
                XmlNode book = data.Item(i);
                //jednotlive hodnoty book elementov su zobrazene aj s prislusnymi znackami z .xml suboru
                x_to_file = book.OuterXml;
                sw.Write("\t" + x_to_file + "\n");
                i++;
            }
            sw.Write("<timestamp>" + "\n\n");
            String timeStamp = GetTimestamp(DateTime.Now);
            sw.Write("\t" + timeStamp + "\n");
            sw.Write("\n</timestamp>");
            sw.Write("\n</output>");
            sw.Close();
        }


        [WebMethod]
        public void SinglebookDataById(string  id)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            XmlNodeList singleBookById = doc.SelectNodes("Bookstore/books/book[id=" + id + "]");
            // vzdy nam vracia 1 hodnotu a preto ak sa item(0) rovna null tak vrati status code 404 s popisom 
            if (singleBookById.Item(0) == null)
            {
                Context.Response.StatusCode = 404;
                Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
                Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");

            }
            else
            {
                //SavetoRoot(fileOutputSingleSearch, singleBookById);
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, singleBookById);
                // nastavenie UTF-8 sady pre http response 
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                // do responsu nam zapise serializovane xml na json 
                Context.Response.Write(JsonConvert.SerializeXmlNode(singleBookById.Item(0), Formatting.Indented));
                
            }
            
        }
        
        [WebMethod]
        public void SinglebookDataByName(string name)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            // prekovertujeme name na lowercase kedze to iste spravim aj na frontende a tym padom aj ked pouzivatel zada nazov v inom case tak vrati dobry vysledok 
            XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[translate(nazov,'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ','abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ') = \"" +name+"\"]");
            if (nodeListBook.Item(0) == null)
            {
              Context.Response.StatusCode = 404;
               Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
             Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");

            }
             else
             {
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                //nastavenie UTF-8 sady pre http response 
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
            if (nodeListBook.Item(0) == null)
            {
                Context.Response.StatusCode = 404;
                Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
                Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");

            }
            else
            {
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                //nastavenie UTF-8 sady pre http response 
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));

            }
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
        [WebMethod]
        public void GetListBooksBySelectedKategory(string category,string input ) {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileBookInfo);
            // input musi byt string!
            XmlNodeList Selectedbooks = doc.SelectNodes("Bookstore/books/book["+category+"=\""+input+"\"]");
         
            int count = Selectedbooks.Count;
            
            for (int i = 0; i < count; i++)

            {
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(Selectedbooks.Item(i),Formatting.Indented));

            }

        }
        
        
    }
}

