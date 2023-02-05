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
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
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
        private String fileBookInfo = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\book_moje.xml ";
        private String fileBookTransactionInfo = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\book_transakcie_moje.xml ";
        public String fileOutputSingleSearch = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\output.xml";
        public String fileAmountFilterPath = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\Amoutsoutputs";


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
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-dd-HH-mm-ss");
        }
        public void WriteToTheFileWithTimeStamp(string path, XmlNodeList data)
        {
            // Create a new XML document
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            XmlElement output;
            XmlElement timestamp;

            // Check if the file exists
            bool fileExists = System.IO.File.Exists(path);

            // If the file does not exist, create a new root element
            if (!fileExists)
            {
                root = doc.CreateElement("root");
                doc.AppendChild(root);
            }
            // If the file exists, load the document and get the root element
            else
            {
                doc.Load(path);
                root = doc.DocumentElement;
            }

            // Loop through each node in the data
            foreach (XmlNode node in data)
            {
                // Import the node into the document
                XmlNode importedNode = doc.ImportNode(node, true);
                // Create an "output" element
                output = doc.CreateElement("output");
                // Create a "timestamp" element
                timestamp = doc.CreateElement("timestamp");
                // Set the inner text of the timestamp element to the current date and time
                timestamp.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Append the imported node and the timestamp element to the output element
                output.AppendChild(importedNode);
                output.AppendChild(timestamp);
                // Append the output element to the root element
                root.AppendChild(output);
            }

            // Save the document to the specified file path
            doc.Save(path);
        }

        //This code writes an XML document to a file with a timestamp.If the file already exists, the code will load the file and add new elements to it, otherwise it will create a new XML document with a root element.The code loops through each node in the data, imports it into the document, creates an "output" element, creates a "timestamp" element with the current date and time, appends the imported node and the timestamp to the output element, and finally appends the

        // ulozi IEnumerable alebo iny objekt vysledok LINQ dopytu spolu s casovou peciatkov a hladanymi parametrami do noveho xml suboru 
        //dynamic je kľúčové slovo C#, ktoré umožňuje deklarovať premennú ako dynamický typ. Typ premennej sa rieši počas behu namiesto kompilácie. To znamená, že typ premennej sa môže dynamicky meniť na základe hodnoty, ku ktorej je priradená.
        //Použitie dynamic v tejto metode je výhodné, pretože  umožňuje odovzdať akýkoľvek typ objektu metóde SaveWebMethodResult bez toho, aby sa muselo zadava5 presný typ.Vďaka tomu je metóda flexibilnejšia a všeobecnejšia, pretože dokáže spracovať objekty rôznych typov a uložiť ich do súboru XML.
        // Keďže typ výsledku je dynamický, kód môže určiť typ objektu za behu a skontrolovať, či ide o IEnumerable<object>. Ak áno, kód prejde každú položku a pridá ju do súboru XML. Ak nie, kód ho pridá ako jednu položku.
        //Použitím dynamických sa môžete vyhnúť nutnosti písať viacero metód na spracovanie rôznych typov objektov a namiesto toho napísať jednu metódu, ktorá zvládne všetky typy.
        public static void SaveWebMethodResult(dynamic result, string methodName, string[] parameters, string path)
        {
            // Check if the specified path exists, and create it if not
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generate the file name based on the current time and method name
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + methodName + "_";
            foreach (string param in parameters)
            {
                fileName += param.ToString() + "_";
            }
            fileName = fileName.TrimEnd('_') + ".xml";

            // Create the root element of the XML file
            XElement root = new XElement("Root");

            // Check if the result is an IEnumerable<object>
            if (result is IEnumerable<object>)
            {
                // If it is, loop through each item and add it to the XML file
                foreach (var item in (IEnumerable<object>)result)
                {
                    XElement element = new XElement("Item");
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        element.Add(new XElement(prop.Name, prop.GetValue(item)));
                    }
                    root.Add(element);
                }
            }
            else
            {
                // If it's not an IEnumerable<object>, add it as a single item to the XML file
                XElement element = new XElement("Item");
                foreach (var prop in result.GetType().GetProperties())
                {
                    element.Add(new XElement(prop.Name, prop.GetValue(result)));
                }
                root.Add(element);
            }

            // Save the XML file to the specified path
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
        public void SortedBookAmoutsByDateAndCategory(string selectedAtribute, string selectedValueAtribute, string startDate, string endDate, string sortField, string sortOrder)
        {
            string[] parameters = { selectedAtribute, selectedValueAtribute, startDate, endDate, sortField, sortOrder, };
            if (selectedAtribute != "vsetky")
            {
                // Skontroluje ci su vlozene vsetky parametre 
                if (string.IsNullOrEmpty(selectedAtribute) || string.IsNullOrEmpty(selectedValueAtribute) ||
                    string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                    return;
                }
            }
            else
            {

                if (string.IsNullOrEmpty(selectedAtribute) ||
                       string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(sortField) || string.IsNullOrEmpty(sortOrder))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                    return;
                }


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
                Context.Response.Write("Začiatočný dátum nemože byť neskorej ako konečný dátum");
                return;
            }
            // Load XML files containing book information and book transaction information
            var booksXml = XElement.Load(fileBookInfo);
            var warehouseXml = XElement.Load(fileBookTransactionInfo);
            // Convert the start and end dates to DateTime format
            DateTime startD = DateTime.Parse(startDate);
            DateTime endD = DateTime.Parse(endDate);
            // Use LINQ to join the information from the two XML files and filter the results based on selected category and selected value
            if (selectedAtribute != "vsetky")
            {
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
                double TotalStartAmount = result.Sum(x => x.StartAmount);
                double TotalEndAmount = result.Sum(x => x.EndAmount);

                if (result.Count() == 0)
                {
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write("Žiadny záznam nespĺňa zadané kritériá");

                };
                SaveWebMethodResult(result, "SaveWebMethodResult", parameters, fileAmountFilterPath);
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else
            {

                var result = from b in booksXml.Descendants("book")
                             join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - startD).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - endD).Ticks)).Take(1)




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
        [WebMethod]
        public string GetTotalSellOfBooks(string bookXmlFile, string transactionXmlFile, string startDate, string endDate, string category, string sortBy, string sortOrder)
        {
            XDocument bookData = XDocument.Load(bookXmlFile);
            XDocument transactionData = XDocument.Load(transactionXmlFile);

            var result = from b in bookData.Root.Elements("book")
                         join t in transactionData.Root.Elements("transaction") on (int)b.Element("id") equals (int)t.Element("book_id")
                         where t.Element("type").Value == "sell" &&
                               DateTime.Parse(t.Element("date").Value) >= DateTime.Parse(startDate) &&
                               DateTime.Parse(t.Element("date").Value) <= DateTime.Parse(endDate) &&
                               b.Element("category").Value == category
                         select new
                         {
                             BookId = (int)b.Element("id"),
                             BookName = b.Element("name").Value,
                             SellDate = DateTime.Parse(t.Element("date").Value),
                             SellAmount = (double)t.Element("amount")
                         };

            switch (sortBy)
            {
                case "id":
                    result = sortOrder == "ascending" ? result.OrderBy(x => x.BookId) : result.OrderByDescending(x => x.BookId);
                    break;
                case "name":
                    result = sortOrder == "ascending" ? result.OrderBy(x => x.BookName) : result.OrderByDescending(x => x.BookName);
                    break;
                case "date":
                    result = sortOrder == "ascending" ? result.OrderBy(x => x.SellDate) : result.OrderByDescending(x => x.SellDate);
                    break;
                case "amount":
                    result = sortOrder == "ascending" ? result.OrderBy(x => x.SellAmount) : result.OrderByDescending(x => x.SellAmount);
                    break;
            }

            double totalSell = result.Sum(x => x.SellAmount);
            return totalSell.ToString();
        }
        [WebMethod]
        public void AgregatedStatiscticsAmount(string selectedAtribute, string selectedValueAtribute, string startDate, string endDate)
        {
            string[] parameters = { selectedAtribute, selectedValueAtribute, startDate, endDate, };
            if (selectedAtribute != "vsetky")
            {
                // Skontroluje ci su vlozene vsetky parametre 
                if (string.IsNullOrEmpty(selectedAtribute) || string.IsNullOrEmpty(selectedValueAtribute) ||
                    string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) )
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                    return;
                }
            }
            else
            {

                if (string.IsNullOrEmpty(selectedAtribute) ||
                       string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate)  )
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                    return;
                }


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
                Context.Response.Write("Začiatočný dátum nemože byť neskorej ako konečný dátum");
                return;
            }
            // Load XML files containing book information and book transaction information
            var booksXml = XElement.Load(fileBookInfo);
            var warehouseXml = XElement.Load(fileBookTransactionInfo);
            // Convert the start and end dates to DateTime format
            DateTime startD = DateTime.Parse(startDate);
            DateTime endD = DateTime.Parse(endDate);
            // Use LINQ to join the information from the two XML files and filter the results based on selected category and selected value
            if (selectedAtribute != "vsetky")
            {
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
                                 StartAmount = startAmount,
                                 EndAmount = (int)w2.Element("aktualne_mnozstvo_na_sklade"),
                                 StartDate = start.ToString("yyyy-MM-dd"),
                                 EndDate = end.ToString("yyyy-MM-dd")
                             };


                double TotalStartAmount = result.Sum(x => x.StartAmount);
                double TotalEndAmount = result.Sum(x => x.EndAmount);
                double MaxStartAmount = result.Max(x => x.StartAmount);
                double MinStartAmont = result.Min(x => x.StartAmount);
                double MaxEndAmount = result.Max(x => x.EndAmount);
                double MinEndAmont = result.Min(x => x.EndAmount);
                double AvgStartAmont = result.Average(x => x.StartAmount);
                double AvgEndAmont = result.Average(x => x.EndAmount);
                var serializedResult = new
                {
                    TotalStartAmount,
                    TotalEndAmount,
                    MaxStartAmount,
                    MinStartAmont,
                    MaxEndAmount,
                    MinEndAmont,
                    AvgStartAmont,
                    AvgEndAmont,
                };

                if (result.Count() == 0)
                {
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write("Žiadny záznam nespĺňa zadané kritériá");

                };
                SaveWebMethodResult(serializedResult, "SaveWebMethodResult", parameters, fileAmountFilterPath);
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(serializedResult, Formatting.Indented));
            }
            else
            {

                var result = from b in booksXml.Descendants("book")
                             join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - startD).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - endD).Ticks)).Take(1)
                             select new
                             {
                                 StartAmount = startAmount,
                                 EndAmount = (int)w2.Element("aktualne_mnozstvo_na_sklade"),
                                 StartDate = start.ToString("yyyy-MM-dd"),
                                 EndDate = end.ToString("yyyy-MM-dd")
                             };
                double TotalStartAmount = result.Sum(x => x.StartAmount);
                double TotalEndAmount = result.Sum(x => x.EndAmount);
                double MaxStartAmount = result.Max(x => x.StartAmount);
                double MinStartAmont = result.Min(x => x.StartAmount);
                double MaxEndAmount = result.Max(x => x.EndAmount);
                double MinEndAmont = result.Min(x => x.EndAmount);
                double AvgStartAmont = result.Average(x => x.StartAmount);
                double AvgEndAmont = result.Average(x => x.EndAmount);
                var serializedResult = new
                {
                    TotalStartAmount,
                    TotalEndAmount,
                    MaxStartAmount,
                    MinStartAmont,
                    MaxEndAmount,
                    MinEndAmont,
                    AvgStartAmont,
                    AvgEndAmont,
                };
                if (result.Count() == 0)
                {
                    Context.Response.ContentType = "application/json";
                    Context.Response.Write("Žiadny záznam nespĺňa zadané kritériá");

                };
                SaveWebMethodResult(result, "SaveWebMethodResult", parameters, fileAmountFilterPath);
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(serializedResult, Formatting.Indented));
            }

        }
    };
}


