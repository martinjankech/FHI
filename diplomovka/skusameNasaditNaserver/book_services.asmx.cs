using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace knihy_jankech
{
    /// <summary>
    /// Summary description for book_services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class book_services : System.Web.Services.WebService
    {
        private String fileBookInfo = "D:\\git_repozitare\\FHI\\diplomovka\\skusameNasaditNaserver\\xml\\book_moje.xml ";
        private String fileBookTransactionInfo = "D:\\git_repozitare\\FHI\\diplomovka\\skusameNasaditNaserver\\xml\\book_transakcie_moje.xml ";
        public String fileOutputSingleSearch = "D:\\git_repozitare\\FHI\\diplomovka\\skusameNasaditNaserver\\xml\\output.xml";
        public String fileAmountFilterPath = "D:\\git_repozitare\\FHI\\diplomovka\\skusameNasaditNaserver\\xml\\Amoutsoutputs";


        private XmlDocument LoadDocument(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filePath);
                return doc;
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Error while loading the document";
                Context.Response.Write("Error while loading the document: " + ex.Message);
                return null;
            }
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
        public void WriteToTheFileWithTimeStamp(string path, XmlNodeList data)
        {
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            string x_to_file = "";
            int nodeListCount = data.Count;
            int i = 0;
            sw.Write("\n" + "<output>" + "\n\n");

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
        // ulozi IEnumerable objekt vysledok LINQ dopytu spolu s casovou peciatkov a hladanymi parametrami do noveho xml suboru 
        public static void SaveWebMethodResult(IEnumerable<object> result, string methodName, string[] parameters, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + methodName + "_";
            foreach (string param in parameters)
            {
                fileName += param.ToString() + "_";
            }
            fileName = fileName.TrimEnd('_') + ".xml";

            XElement root = new XElement("Root");
            foreach (var item in result)
            {
                XElement element = new XElement("Item");
                foreach (var prop in item.GetType().GetProperties())
                {
                    element.Add(new XElement(prop.Name, prop.GetValue(item)));
                }
                root.Add(element);
            }

            string fullPath = Path.Combine(path, fileName);
            root.Save(fullPath);
        }

        [WebMethod]
        public void SinglebookDataById(string id)
        {
            try
            {
                XmlDocument doc = LoadDocument(fileBookInfo);
                if (doc == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                    return;
                }
                if (string.IsNullOrEmpty(id))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.StatusDescription = "Nezadali ste hodnotu id";
                    Context.Response.Write("Nezadali ste ID");
                    return;
                }

                XmlNodeList singleBookById = doc.SelectNodes("Bookstore/books/book[id=" + id + "]");
                if (singleBookById.Item(0) == null)
                {
                    Context.Response.StatusCode = 404;
                    Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
                    Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");
                }
                else
                {
                    WriteToTheFileWithTimeStamp(fileOutputSingleSearch, singleBookById);
                    Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                    Context.Response.Write(JsonConvert.SerializeXmlNode(singleBookById.Item(0), Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Interná chyba servera";
                Context.Response.Write("Vyskytla sa interná chyba servera: " + ex.Message);
            }
        }

        [WebMethod]
        public void SinglebookDataByName(string name)
        {
            try
            {
                XmlDocument doc = LoadDocument(fileBookInfo);
                if (doc == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                    return;
                }
                if (string.IsNullOrEmpty(name))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.StatusDescription = "Nezadali ste hodnotu mena knihy";
                    Context.Response.Write("Nezadali ste ID");
                    return;
                }
                name = name.ToLower();
                XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[translate(nazov,'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ','abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ') = \"" + name + "\"]");

                if (nodeListBook == null || nodeListBook.Count == 0)
                {
                    throw new Exception("Record not found for the given name");
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Chyba pri spracovavani poziadavky";
                }

                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Chyba pri spracovavani poziadavky";
                Context.Response.Write("Error: " + ex.Message);
            }
        }

        [WebMethod]
        public void SinglebookDataByIsbn(string isbn)

        {
            XmlDocument doc = LoadDocument(fileBookInfo);
            if (doc == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                return;
            }
            if (string.IsNullOrEmpty(isbn))
            {
                Context.Response.StatusCode = 400;
                Context.Response.StatusDescription = "Nezadali ste hodnotu mena isbn";
                Context.Response.Write("Nezadali ste ID");
                return;
            }
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

        }
        [WebMethod]
        public void GetListAllBooks()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileBookInfo);
            if (doc == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                return;
            }
            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books");
            // nastavenie UTF-8 sady pre http response 
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(JsonConvert.SerializeXmlNode(AllBook.Item(0), Formatting.Indented));
        }
        [WebMethod]
        public void GetListBooksBySelectedKategory(string category, string input)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileBookInfo);
            if (doc == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                return;
            }
            // input musi byt string!
            XmlNodeList Selectedbooks = doc.SelectNodes("Bookstore/books/book[" + category + "=\"" + input + "\"]");

            int count = Selectedbooks.Count;

            for (int i = 0; i < count; i++)

            {
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(Selectedbooks.Item(i), Formatting.Indented));

            }

        }

        [WebMethod]
        public void SortedBookDataByDateCategory(string selectedAtribute, string selectedValueAtribute, string startDate, string endDate, string sortField, string sortOrder)
        {
            string[] parameters = { selectedAtribute, selectedValueAtribute, startDate, endDate, sortField, sortOrder, };
            //var parameters = new {
            //    selectedAtribute = selectedAtribute,
            //    selectedValueAtribute = selectedAtribute,
            //    startDate = startDate,
            //    endDate= endDate,
            //    sortField= sortField,
            //    sortOrder= sortOrder,
            //};
            // Skontroluje ci su vlozene vsetky parametre 
            if (string.IsNullOrEmpty(selectedAtribute) || string.IsNullOrEmpty(selectedValueAtribute) ||
                string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                return;
            }

            // skontroluje ci su hodnoty s sortorder "Ascending" alebo "Descending "
            if (!sortOrder.Equals("Ascending") && !sortOrder.Equals("Descending"))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Input Error: sortOrder must be either 'Ascending' or 'Descending'");
                return;
            }


            // skontroluje ci su dostal spravnu z vybranych hodnot podla ktorych ma zoradit udaje este doplnit 
            if (!sortField.Equals("nazov") && !sortField.Equals("pocet_stran") && !sortField.Equals("rok_vydania")
                && !sortField.Equals("predajna_cena") && !sortField.Equals("nakupna_cena") && !sortField.Equals("priemerne_hodnotenie"))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Input Error: zoradovat možete podla tých atribútov 'nazov' 'pocet_stran''rok_vydania','predajna_cena''nakupna_cena','priemerne_hodnotenie'");
                return;
            }
            // Try to parse the start and end dates
            DateTime start, end;
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }
            if (start > end)
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zaciatocny datum nemoze byt neskorej ako konecny datum");
                return;
            }
            // Load XML files containing book information and book transaction information
            var booksXml = XElement.Load(fileBookInfo);
            var warehouseXml = XElement.Load(fileBookTransactionInfo);
            // Convert the start and end dates to DateTime format
            DateTime startD = DateTime.Parse(startDate);
            DateTime endD = DateTime.Parse(endDate);
            // Use LINQ to join the information from the two XML files and filter the results based on selected category and selected value
            var result = from b in booksXml.Descendants("book")
                         join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                         from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - startD).Ticks)).Take(1)
                         let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                         from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - endD).Ticks)).Take(1)
                         where (string)b.Element(selectedAtribute) == selectedValueAtribute ||
                         (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor1") == selectedValueAtribute) ||
                         (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor2") == selectedValueAtribute)

                         select new
                         {
                             BookID = (string)b.Element("id"),
                             BookName = (string)b.Element("nazov"),
                             BookNumPages = (int)b.Element("pocet_stran"),
                             BookYear = (int)b.Element("rok_vydania"),
                             BookSellPrice = (float)b.Element("predajna_cena"),
                             BookBuyPrice = (float)b.Element("nakupna_cena"),
                             BookRating = (float)b.Element("priemerne_hodnotenie"),
                             StartAmount = startAmount,
                             EndAmount = (int)w2.Element("aktualne_mnozstvo_na_sklade"),
                             StartDate = start.ToString("yyyy-MM-dd"),
                             EndDate = end.ToString("yyyy-MM-dd")
                         };

            if (sortOrder == "Ascending")
            {
                switch (sortField)
                {
                    case "nazov":
                        result = result.OrderBy(r => r.BookName);
                        break;
                    case "pocet_stran":
                        result = result.OrderBy(r => r.BookNumPages);
                        break;
                    case "rok_vydania":
                        result = result.OrderBy(r => r.BookYear);
                        break;
                    case "predajna_cena":
                        result = result.OrderBy(r => r.BookSellPrice);
                        break;
                    case "nakupna_cena":
                        result = result.OrderBy(r => r.BookBuyPrice);
                        break;
                    case "priemerne_hodnotenie":
                        result = result.OrderBy(r => r.BookRating);
                        break;

                    default:
                        break;
                }
            }
            else if (sortOrder == "Descending")
            {
                switch (sortField)
                {
                    case "nazov":
                        result = result.OrderByDescending(r => r.BookName);
                        break;
                    case "pocet_stran":
                        result = result.OrderByDescending(r => r.BookNumPages);
                        break;
                    case "rok_vydania":
                        result = result.OrderByDescending(r => r.BookYear);
                        break;
                    case "predajna_cena":
                        result = result.OrderByDescending(r => r.BookSellPrice);
                        break;
                    case "nakupna_cena":
                        result = result.OrderByDescending(r => r.BookBuyPrice);
                        break;
                    case "priemerne_hodnotenie":
                        result = result.OrderByDescending(r => r.BookRating);
                        break;

                    default:
                        break;
                }
            }
            if (result.Count() == 0)
            {
                Context.Response.ContentType = "application/json";
                Context.Response.Write("Žiadny záznam nespĺňa zadané kritériá");

            };
            SaveWebMethodResult(result, "SaveWebMethodResult", parameters, fileAmountFilterPath);
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
        }


    }
};


