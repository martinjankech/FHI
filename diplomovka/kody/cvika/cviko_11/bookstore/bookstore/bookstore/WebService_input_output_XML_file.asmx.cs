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
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace bookstore
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/", Description = "Webova sluzba analyzuje XML    subor" + " ulozeny na web serveri.")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_input_output_XML_file : System.Web.Services.WebService
    {
        //nazov skumaneho .xml suboru;
        //subor moze byt lokalny subor (nas pripad), alebo subor niekde na inom serveri na internete (vtedy treba 
        //zadat jeho HTTP URL (Web adresu))
        private string FileName = "D:\\git_repositories\\FHI\\diplomovka\\kody\\cvika\\cviko_11\\bookstore\\bookstore\\bookstore\\books.xml";
        private XmlDocument doc;

        //_____________________________________________________________________________________
        //konstruktor triedy 'Work_with_input_output_XML_file'
        public WebService_input_output_XML_file()
        {
            //vytvorenie objektu 'doc' triedy 'XmlDocument'
            doc = new XmlDocument();

            


            //nacitanie .xml suboru do stream-u (datoveho prudu), cez ktory web sluzba pracuje s .xml suborom
            doc.Load(FileName);
        }

        //_____________________________________________________________________________________
        [WebMethod(Description = "Vrati POCET elementov 'book' so zadanou hodnotou ich detskeho elementu   'year'")]
        public int GetElementsCount(string ElementValue)
        {
            if (ElementValue != "2003" && ElementValue != "2005" && ElementValue != "1998")
            {
                //alternativa vytvorenia argumentov metody 'MessageBox.Show' 
                string message = "Vlozili ste nekorektny vstup '" + ElementValue + "' do metody 'GetElementsCount webovej sluzby !!!" + "\n\nKorektne vstupy su:\n2003\n2005\n1998";
                string caption = "Nekorektny vstup do metody 'GetElementsCount'";
                MessageBoxButtons buttons = MessageBoxButtons.OK;

                MessageBox.Show(message, caption, buttons, MessageBoxIcon.Exclamation,
                      MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return 0;
            }

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 

            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[year=" + ElementValue + "]/title");
           
            return nodeList.Count;
        }

        //_____________________________________________________________________________________
        [WebMethod(Description = "Vrati ZOZNAM hodnot elementov 'title' rodicovskych elementov 'book', ktore   maju v detskom elemente 'year' zadanu hodnotu")]
        public string GetTitleElementswithYear(string ElementValue)
        {
            if (ElementValue != "2003" && ElementValue != "2005" && ElementValue != "1998")
            {
                MessageBox.Show("Vlozili ste nekorektny vstup '" + ElementValue + "' do metody  'GetTitleElementswithYear' webovej sluzby!!!" + "\n\nKorektne vstupy su:\n2003\n2005\n1998", "Nekorektny vstup do metody 'GetTitleElementswithYear'", MessageBoxButtons.OK, 

                       MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 
			           MessageBoxOptions.ServiceNotification);
                return "chybny vstup";
            }

            //@-quoting - escape sequences are not processed
            string path = @"D:\git_repositories\FHI\diplomovka\kody\cvika\cviko_11\bookstore\bookstore\bookstore\method_output.xml";

            //Druhy parameter konstruktora ‘StreamWriter’ urcuje ako budu data pridane do suboru. Ak subor existuje 
            //a tento parameter ma logicku hodnotu ‘false’, potom je subor prepisany novymi datami. Ak subor existuje 
            //a tento parameter ma logicku hodnotu ‘true’, potom su nove data pridane do suboru. Inak je vytvoreny 
            //novy subor.
            StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);

            string x_to_file = "";
            string x = "";
            string return_x = "";

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 

            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[year=" + ElementValue + "]/title");

            int nodeListCount = nodeList.Count;
            int i = 0;

            sw.Write("<output>" + "\n\n");
            while (i < nodeListCount)
            {
                XmlNode title = nodeList.Item(i);
                //jednotlive hodnoty title elementov su zobrazene aj s prislusnymi znackami z .xml suboru
                return_x += title.OuterXml;
                x_to_file = title.OuterXml;
                sw.Write("\t" + x_to_file + "\n");
                x += title.InnerText + "\n";
                i++;
            }
            sw.Write("\n</output>");
            MessageBox.Show("Tituly knih s rokom vydania " + ElementValue + "\n\n" + x, "Vystup metod 'GetTitleElementswithYear'", MessageBoxButtons.OK, 

                   MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.ServiceNotification);

            //prikaz je nutny, aby bol obsah stream-u "splachnuty" do diskoveho suboru a zaroven je stream 
            //korektne zatvoreny, cim su uvolnene vsetky zdroje, ktore pouzival
            sw.Close();
            return return_x;
        }

        //_____________________________________________________________________________________
        [WebMethod(Description = "Vrati ZOZNAM hodnot elementov 'title' a 'author' rodicovskych elementov 'book' (tiez ich pocet), ktore maju v atribute 'category' zadanu hodnotu")]
        public string GetTitleAuthorElementswithCategory(string AttributValue)
        {
            if (AttributValue != "COOKING" && AttributValue != "WEB" && AttributValue != "CHILDREN" &&
                                           AttributValue != "KLASIKA")
            {
                MessageBox.Show("Vlozili ste nekorektny vstup '" + AttributValue + "' do metody  'GetTitleAuthorElementswithCategory' webovej sluzby!!!" + "\n\nKorektne vstupy   su:\nCOOKING\nWEB\nCHILDREN\nKLASIKA", "Nekorektny vstup do metody    'GetTitleAuthorElementswithCategory'", MessageBoxButtons.OK, 

                       MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 
			           MessageBoxOptions.ServiceNotification);
                return "chybny vstup";
            }

            string path = @"D:\git_repositories\FHI\diplomovka\kody\cvika\cviko_11\bookstore\bookstore\bookstore\method_output.xml";

            StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);

            string x_to_file = "";

            string x = "";
            string return_x = "";

            //vytvorenie objektu 'nodeList' triedy 'XmlNodeList'
            XmlNodeList nodeList;

            XmlElement root = doc.DocumentElement; //ziskanie korenoveho elementu v nacitanom .xml subore 

            //v argumente instancnej metody 'SelectNodes' je pouzity regularny XPath vyraz
            nodeList = root.SelectNodes("/bookstore/book[@category='" + AttributValue + "']/title | / bookstore / book[@category = '" + AttributValue + "'] / author");
            XmlNodeList nodeList_title = root.SelectNodes("/bookstore/book[@category='" + AttributValue + "']/title");

            int nodeList_titleCount = nodeList_title.Count;

            int nodeListCount = nodeList.Count;
            int i = 0;

            sw.Write("<output>" + "\n\n");
            while (i < nodeListCount)
            {
                XmlNode title = nodeList.Item(i);
                //jednotlive hodnoty title elementov su zobrazene aj s prislusnymi znackami z .xml suboru 
                return_x += title.OuterXml;
                x_to_file = title.OuterXml;
                sw.Write("\t" + x_to_file + "\n");
                x += title.InnerText + "\n";
                i++;
            }
            sw.Write("\n</output>");
            MessageBox.Show("Pocet knih s hodnotou atributu 'category'='" + AttributValue + ": " + nodeList_titleCount + "\n\nTituly knih s hodnotou atributu 'category'='" + AttributValue  + "' s ich autormi\n\n" + x, "Vystup metody 'GetTitleAuthorElementswithCategory'",MessageBoxButtons.OK, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            sw.Close();
            return return_x;
        }

    }
}
