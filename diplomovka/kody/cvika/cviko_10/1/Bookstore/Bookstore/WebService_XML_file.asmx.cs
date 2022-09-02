using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Diagnostics;
namespace Bookstore
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/", Description = "Webova sluzba analyzuje XML       subor" + " ulozeny na web serveri.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService_XML_file : System.Web.Services.WebService
    {
        //nazov skumaneho .xml suboru (lokalny subor) s cestou k nemu
        private string FileName = " D:\\git_repositories\\FHI\\diplomovka\\kody\\cvika\\cviko_10\\1\\Bookstore\\Bookstore\\books.xml";
     
           // "D:\\CVICENIA\\LS_11\\PSaDSDIII\\XPath\\priklady\\books\\books.xml";
        private XmlDocument doc;


        //konstruktor triedy 'Work_with_XML_file'
        public WebService_XML_file()
        {
            //vytvorenie objektu 'doc' triedy 'XmlDocument'
            doc = new XmlDocument();

            //nacitanie .xml suboru do stream-u (datoveho prudu), cez ktory web sluzba pracuje s .xml suborom
            doc.Load(FileName);
        }


        [WebMethod(Description = "Vrati POCET elementov 'book' so zadanou hodnotou ich detskeho elementu         'year'")]
        public int GetElementsCount(string ElementValue)
        {
            //testovanie korektnosti vstupu od pouzivatela
            if (ElementValue != "2003" && ElementValue != "2005" && ElementValue != "1998")
            {
                MessageBox.Show("Vlozili ste nekorektny vstup '" + ElementValue + "' do metody 'GetElementsCount'   webovej sluzby!!!" + "\n\nKorektne vstupy su:\n2003\n2005\n1998", "Nekorektny vstup do metody 'GetElementsCount'", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 

            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[year=" + ElementValue + "]/title");

            return nodeList.Count;
        }



        [WebMethod(Description = "Vrati ZOZNAM hodnot elementov 'title' rodicovskych elementov 'book', ktore    maju v detskom elemente 'year' zadanu hodnotu")]
        public string GetTitleElementswithYear(string ElementValue)
        {
            //testovanie korektnosti vstupu od pouzivatela
            if (ElementValue != "2003" && ElementValue != "2005" && ElementValue != "1998")
            {
                MessageBox.Show("Vlozili ste nekorektny vstup '" + ElementValue + "' do metody   'GetTitleElementswithYear' webovej sluzby!!!" + "\n\nKorektne vstupy su:\n2003\n2005\n1998",   "Nekorektny vstup do metody 'GetTitleElementswithYear'", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "chybny vstup";
            }

            string x = "";
            string return_x = "";

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore

            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[year=" + ElementValue + "]/title");

            int nodeListCount = nodeList.Count;
            int i = 0;

            while (i < nodeListCount)
            {
                XmlNode title = nodeList.Item(i);

                //jednotlive hodnoty title elementov su zobrazene aj s prislusnymi znackami z .xml suboru 
                return_x += title.OuterXml;
                x += title.InnerText + "\n";
                i++;
            }

            MessageBox.Show("Tituly knih s rokom vydania " + ElementValue + "\n\n" + x, "Vystup metody  'GetTitleElementswithYear'", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return return_x;
        }


        [WebMethod(Description = "Vrati ZOZNAM hodnot elementov 'title' a 'author' rodicovskych elementov 'book'  (tiez ich pocet), ktore maju v atribute 'category' zadanu hodnotu")]
        public string GetTitleAuthorElementswithCategory(string AttributValue)
        {
            //testovanie korektnosti vstupu od pouzivatela
            if (AttributValue != "COOKING" && AttributValue != "WEB" && AttributValue != "CHILDREN" &&
                                              AttributValue != "KLASIKA")
            {
               MessageBox.Show("Vlozili ste nekorektny vstup '" + AttributValue + "' do metody  'GetTitleAuthorElementswithCategory' webovej sluzby!!!" + "\n\nKorektne vstupy su:\nCOOKING\nWEB\nCHILDREN\nKLASIKA", "Nekorektny vstup do metody 'GetTitleAuthorElementswithCategory'", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "chybny vstup";
            }

            string x = "";
            string return_x = "";

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 
            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[@category='" + AttributValue + "']/title |   / bookstore / book[@category = '" + AttributValue + "']/author");
            XmlNodeList nodeList_title = root.SelectNodes("/bookstore/book[@category='" + AttributValue + "']/title");

            int nodeList_titleCount = nodeList_title.Count;

            int nodeListCount = nodeList.Count;
            int i = 0;

            while (i < nodeListCount)
            {
                XmlNode title = nodeList.Item(i);
                return_x += title.OuterXml;
                x += title.InnerText + "\n";
                i++;
            }

            MessageBox.Show("Pocet knih s hodnotou atributu 'category'='" + AttributValue + ": " +
             nodeList_titleCount + "\n\nTituly knih s hodnotou atributu 'category'='" + AttributValue + "'   s ich autormi\n\n" + x, "Vystup metody 'GetTitleAuthorElementswithCategory'",   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return return_x;
        }
       
        [WebMethod(Description = "v XML dokumente books.xml zistí a vráti zoznam hodnôt elementov title a author rodičovských elementov book (tiež ich počet), ktoré majú v atribútoch lang elementov title používateľom zadanú hodnotu (môže byť zadaná hodnota \"en\" alebo \"sk\"),")]
        public string GetTitleAuthorElementswithLang(string lang ) {
            if (lang != "sk" && lang != "en" )
            {
                MessageBox.Show("Vlozili ste nekorektny vstup '" + lang + "' do metody  'GetTitleAuthorElementswithLang' webovej sluzby!!!" + "\n\nKorektne vstupy su:\nsk\nen\n", "Nekorektny vstup do metody 'GetTitleAuthorElementswithLang'", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "chybny vstup";
            }
            string x ="";
            string return_x = "";
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 
            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book/title[@lang='" +lang+ "'] | /bookstore/book[title[@lang='" +lang+ "']]/author");
            XmlNodeList nodeList_title = root.SelectNodes("/bookstore/book/title[@lang='" + lang + "'] ");
           

            int nodeList_titleCount = nodeList.Count;
            int nodeListCount = nodeList.Count;
            int i = 0;
            while (i < nodeListCount)
            {
                XmlNode title = nodeList.Item(i);
               return_x += title.OuterXml;
                x += title.InnerText + "\n";
                i++;
            }

            return return_x; }

    }
}
