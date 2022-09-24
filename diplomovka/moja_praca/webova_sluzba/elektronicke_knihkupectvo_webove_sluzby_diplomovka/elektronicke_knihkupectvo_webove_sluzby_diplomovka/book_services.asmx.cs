using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
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
        public XmlDocument LoadDocument(string docu) {

            XmlDocument doc = new XmlDocument();
            doc.Load(docu);
            return doc;
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
                // nastavenie UTF-8 sady pre http response 
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
            }
            else
                Context.Response.Write("nenasiel sa zaznam pre zadane id ");


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

