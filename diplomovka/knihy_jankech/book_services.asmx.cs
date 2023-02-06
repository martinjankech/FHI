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
using static System.Net.Mime.MediaTypeNames;
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
            // Vytvorenie nového objektu XmlDocument
            XmlDocument doc = new XmlDocument();
            try
            {
                // Načítanie XML dokumentu zo zadaného súboru
                doc.Load(filePath);
                return doc;
            }
            catch (FileNotFoundException ex)
            {
                // Ak sa nevie nájsť zadaný súbor, nastaví sa status kód na 404 a vráti sa popisná chybová správa
                Context.Response.StatusCode = 404;
                Context.Response.StatusDescription = "Zadaný súbor sa nenašiel.";
                Context.Response.Write("Zadaný súbor sa nenašiel: " + ex.Message);
                return null;
            }
            catch (XmlException ex)
            {
                // Ak sa vyskytne chyba pri parsovaní XML dokumentu, nastaví sa status kód na 400 a vráti sa popisná chybová správa
                Context.Response.StatusCode = 400;
                Context.Response.StatusDescription = "Chyba pri parsovaní XML dokumentu.";
                Context.Response.Write("Chyba pri parsovaní XML dokumentu: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Ak sa vyskytne akákoľvek iná chyba pri načítaní dokumentu, nastaví sa status kód na 500 a vráti sa popisná chybová správa
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Chyba pri načítaní dokumentu.";
                Context.Response.Write("Chyba pri načítaní dokumentu: " + ex.Message);
                // Výnimka bude zaznamenaná pre budúce účely ladenia
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        //public string DecodeFromUtf8(string utf8_String)
        //{
        //    byte[] bytes = Encoding.Default.GetBytes(utf8_String);
        //    string utf8 = Encoding.UTF8.GetString(bytes);
        //    return utf8;
        //}
        public static string CreateTimestamp()
        {
            // Formátovať aktuálny dátum a čas pomocou vzoru "yyyy-MM-dd HH:mm:ss"
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        // Táto funckia zapíše dokument XML do súboru s časovou značkou.Ak súbor už existuje,
        // kód ho načíta a pridá doň nové prvky, inak vytvorí nový dokument XML s koreňovým prvkom.
        // Kód prechádza v cykle každý uzol v údajoch, importuje ho do dokumentu, vytvorí prvok "output" (výstup), vytvorí prvok "timestamp" (časová pečiatka) s aktuálnym dátumom a časom,
        // pripojí importovaný uzol a časovú pečiatku k výstupnému prvku a nakoniec pripojí výstupný prvok ku koreňovému prvku.Nakoniec sa dokument uloží do zadanej cesty k súboru.

        // metóda na zápis do súboru s časovým údajom
        // Funkcia na zápis do súboru s časovou pečiatkou
        public void WriteToTheFileWithTimeStamp(string path, XmlNodeList data)
        {
            try
            {
                // Vytvorenie objektu XmlDocument
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                XmlElement output;
                XmlElement timestamp;
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                // Kontrola, či súbor existuje
                bool fileExists = System.IO.File.Exists(path);

                // Ak súbor neexistuje, vytvorí sa nový súbor
                if (!fileExists)
                {
                    doc.AppendChild(xmlDeclaration);
                    root = doc.CreateElement("root");
                    doc.AppendChild(root);
                }
                else
                {
                    // Načítanie existujúceho súboru
                    doc.Load(path);
                    root = doc.DocumentElement;
                }

                // Prechod cez všetky uzly v XmlNodeList
                foreach (XmlNode node in data)
                {
                    // Importovanie uzla do XmlDocument
                    XmlNode importedNode = doc.ImportNode(node, true);
                    output = doc.CreateElement("output");
                    timestamp = doc.CreateElement("timestamp");
                    // Vytvorenie časovej pečiatky
                    timestamp.InnerText = CreateTimestamp();
                    // Pridanie importovaného uzla a časovej pečiatky k elementu output
                    output.AppendChild(importedNode);
                    output.AppendChild(timestamp);
                    // Pridanie elementu output k koreňovému elementu
                    root.AppendChild(output);
                }

                // Uloženie zmeneného súboru
                doc.Save(path);
            }
            catch (FileNotFoundException ex)
            {
                // Výpis chyby, ak súbor sa nenašiel
                Console.WriteLine("Súbor sa nenašiel: " + ex.Message);
            }
            catch (XmlException ex)
            {
                // Výpis chyby, ak nastala chyba v XML súbore
                Console.WriteLine("Chyba XML: " + ex.Message);
            }
            catch (IOException ex)
            {
                // Výpis chyby, ak nastala chyba pri vstupe/výstupe
                Console.WriteLine("Chyba I/O: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Výpis chyby, ak nemáte oprávnenie k prístupu k súboru
                Console.WriteLine("Nedovolený prístup: " + ex.Message);
            }
        }
        //This code writes an XML document to a file with a timestamp.If the file already exists, the code will load the file and add new elements to it, otherwise it will create a new XML document with a root element.The code loops through each node in the data, imports it into the document, creates an "output" element, creates a "timestamp" element with the current date and time, appends the imported node and the timestamp to the output element, and finally appends the

        // ulozi IEnumerable alebo iny objekt vysledok LINQ dopytu spolu s casovou peciatkov a hladanymi parametrami do noveho xml suboru 
        //dynamic je kľúčové slovo C#, ktoré umožňuje deklarovať premennú ako dynamický typ. Typ premennej sa rieši počas behu namiesto kompilácie. To znamená, že typ premennej sa môže dynamicky meniť na základe hodnoty, ku ktorej je priradená.
        //Použitie dynamic v tejto metode je výhodné, pretože  umožňuje odovzdať akýkoľvek typ objektu metóde SaveWebMethodResult bez toho, aby sa muselo zadava5 presný typ.Vďaka tomu je metóda flexibilnejšia a všeobecnejšia, pretože dokáže spracovať objekty rôznych typov a uložiť ich do súboru XML.
        // Keďže typ výsledku je dynamický, kód môže určiť typ objektu za behu a skontrolovať, či ide o IEnumerable<object>. Ak áno, kód prejde každú položku a pridá ju do súboru XML. Ak nie, kód ho pridá ako jednu položku.
        //Použitím dynamických sa môžete vyhnúť nutnosti písať viacero metód na spracovanie rôznych typov objektov a namiesto toho napísať jednu metódu, ktorá zvládne všetky typy.
        public static void SaveWebMethodResult(dynamic result, string methodName, string[] parameters, string path)
        {
            // Skontroluj, či špecifikovaná cesta existuje a vytvor ju, ak nie
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Vygeneruj názov súboru na základe aktuálneho času a názvu metódy
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + methodName + "_";
            foreach (string param in parameters)
            {
                fileName += param.ToString() + "_";
            }
            fileName = fileName.TrimEnd('_') + ".xml";

            // Vytvor koreňový element XML súboru
            XElement root = new XElement("Root");

            // Skontroluj, či výsledok je IEnumerable<object>
            if (result is IEnumerable<object>)
            {
                // Ak áno, prejdi cez každý prvok a pridaj ho do XML súboru
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
                // Ak nie je IEnumerable<object>, pridaj ho ako jediný prvok do XML súboru
                XElement element = new XElement("Item");
                foreach (var prop in result.GetType().GetProperties())
                {
                    element.Add(new XElement(prop.Name, prop.GetValue(result)));
                }
                root.Add(element);
            }

            // Ulož XML súbor na špecifikovanú cestu
            string fullPath = Path.Combine(path, fileName);
            root.Save(fullPath);
        }





        [WebMethod]
        public void SinglebookDataById(string id)
        {
            try
            {
                // Nacitanie dokumentu zo suboru
                XmlDocument doc = LoadDocument(fileBookInfo);
                if (doc == null)
                {
                    // Nastavenie status kodu na 500 a popisu chyby ako "Dokument nemohol byt nacitany" v pripade, ze dokument sa neda nacitat
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                    return;
                }
                if (string.IsNullOrEmpty(id))
                {
                    // Nastavenie status kodu na 400 a popisu chyby ako "Nezadali ste hodnotu id" v pripade, ze nie je zadane id
                    Context.Response.StatusCode = 400;
                    Context.Response.StatusDescription = "Nezadali ste hodnotu id";
                    Context.Response.Write("Nezadali ste ID");
                    return;
                }

                // Vyhladanie jednej knihy podla zadaneho id
                XmlNodeList singleBookById = doc.SelectNodes("Bookstore/books/book[id=" + id + "]");
                if (singleBookById.Item(0) == null)
                {
                    // Nastavenie status kodu na 404 a popisu chyby ako "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup" v pripade, ze kniha sa neda najst
                    Context.Response.StatusCode = 404;
                    Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
                    Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");
                }
                else
                {
                    // Zapisanie najdenej knihy do suboru s casovym znacenim
                    WriteToTheFileWithTimeStamp(fileOutputSingleSearch, singleBookById);
                    Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                    // Serializacia najdenej knihy ako JSON string
                    Context.Response.Write(JsonConvert.SerializeXmlNode(singleBookById.Item(0), Formatting.Indented));
                }
            }
            catch (Exception ex)
            {
                // Nastavenie status kodu na 500 a popisu chyby ako "Interná chyba servera" v pripade vynimky
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
                // Načítanie XML dokumentu
                XmlDocument doc = LoadDocument(fileBookInfo);
                // Ak sa dokument nedá načítať, nastaví sa stavový kód na 500 a opíše sa chyba
                if (doc == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Dokument nemohol byť načítaný";
                    return;
                }
                // Ak nie je zadaný názov knihy, nastaví sa stavový kód na 400 a vypíše sa chybová hláška
                if (string.IsNullOrEmpty(name))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.StatusDescription = "Nezadali ste hodnotu mena knihy";
                    Context.Response.Write("Nezadali ste ID");
                    return;
                }
                // Prevod názvu knihy na malé písmená
                name = name.ToLower();
                // Vyhľadanie záznamu o knihe s daným názvom
                XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[translate(nazov,'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ','abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ') = \"" + name + "\"]");
                // Ak sa záznam nenašiel, vyhodí sa výnimka a nastaví sa stavový kód na 500
                if (nodeListBook == null || nodeListBook.Count == 0)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Chyba pri spracovávaní požiadavky";
                    throw new Exception("Záznam pre daný názov nebol nájdený");

                }
                // Zapísanie výsledku do súboru s časovým razítkom
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                // Výpis výsledku v podobe JSONu

                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
            }
            //Ak došlo k výnimke, spracuj chybu
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
            //Nacitanie XML dokumentu z cesty ulozenej v premennej fileBookInfo
            XmlDocument doc = LoadDocument(fileBookInfo);
            //Ak sa dokument nenacital, nastav status code na 500 a ukonci funkciu
            if (doc == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Dokument nemohol byt nacitany";
                return;
            }
            //Ak nebol zadany isbn, nastav status code na 400 a ukonci funkciu
            if (string.IsNullOrEmpty(isbn))
            {
                Context.Response.StatusCode = 400;
                Context.Response.StatusDescription = "Nezadali ste hodnotu mena isbn";
                Context.Response.Write("Nezadali ste ID");
                return;
            }
            //Vyber vsetkych knih v dokumente
            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books/book");
            //Pocet knih v dokumente
            int allBookCount = AllBook.Count;

            //Vyber knihy s zadanym ISBN
            XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[isbn=\"" + isbn + "\"]");
            //Ak sa kniha nenasla, nastav status code na 404 a ukonci funkciu
            if (nodeListBook.Item(0) == null)
            {
                Context.Response.StatusCode = 404;
                Context.Response.StatusDescription = "Zadaný záznam sa nenasiel prosím skontrolujte svoj vstup";
                Context.Response.Write("Pre zadanu hodnotu sa nenasiel ziaden zaznam");
            }
            //Ak sa kniha nasla, zapis ju do suboru s casovym znacenim a vrat ju v JSON formate v http odpovedi
            else
            {
                WriteToTheFileWithTimeStamp(fileOutputSingleSearch, nodeListBook);
                //Nastavenie UTF-8 sady pre http response
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeXmlNode(nodeListBook.Item(0), Formatting.Indented));
            }
        }
        [WebMethod] // označenie pre volanie cez webový protokol
        public void GetListAllBooks() // verejná funkcia na získanie zoznamu kníh
        {
            XmlDocument doc = new XmlDocument(); // vytvorenie XML dokumentu
            try
            {
                doc.Load(fileBookInfo); // načítanie súboru s informáciami o knihách
            }
            catch (FileNotFoundException) // v prípade, že súbor nebol nájdený
            {
                Context.Response.StatusCode = 500; // nastavenie chybového kódu na 500
                Context.Response.StatusDescription = "Súbor nebol nájdený"; // popis chyby
                return; // ukončenie funkcie
            }
            catch (XmlException) // v prípade chyby pri načítaní XML dokumentu
            {
                Context.Response.StatusCode = 500; // nastavenie chybového kódu na 500
                Context.Response.StatusDescription = "Chyba pri načítaní XML dokumentu"; // popis chyby
                return; // ukončenie funkcie
            }

            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books"); // získanie zoznamu všetkých kníh
            if (AllBook == null || AllBook.Item(0) == null) // v prípade, že zoznam je prázdny alebo neexistuje
            {
                Context.Response.StatusCode = 500; // nastavenie chybového kódu na 500
                Context.Response.StatusDescription = "Chyba pri spracovávaní dát v dokumente"; // popis chyby
                return; // ukončenie funkcie
            }

            // nastavenie UTF-8 sady pre http response 
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(JsonConvert.SerializeXmlNode(AllBook.Item(0), Formatting.Indented));
            // serializácia XML uzla ako JSON a odoslanie ako http odpoveď s formátovaním
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
                    string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Write("Prosím zadajte všetky vstupné parametre");
                    return;
                }
            }
            else
            {

                if (string.IsNullOrEmpty(selectedAtribute) ||
                       string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
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

        // nemazat zatial najlesia uz ju len doplnit 

        [WebMethod]
        public void GetAggregatedDataSellByDateAndSelectedCategory(string kat, DateTime startDate, DateTime endDate)
        {
            // Load the books data from XML
            XDocument booksData = XDocument.Load(fileBookInfo);
            // Load the transactions data from XML
            XDocument transactionsData = XDocument.Load(fileBookTransactionInfo);
            if (kat != "autori")
            {


                // Join the books data and transactions data on book id to get all information related to each transaction
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")

                                     // Filter the transactions to only those with a date within the specified range and of type "predaj"
                                     where (DateTime)transaction.Element("datum") >= startDate
                                     && (DateTime)transaction.Element("datum") <= endDate
                                     && transaction.Element("typ_transakcie").Value == "predaj"

                                     // Group the transactions by the specified category element value
                                     group new { Book = book, Transaction = transaction } by book.Element(kat).Value into g
                                     select new
                                     {
                                         // Store the category value as Podkategoria
                                         Podkategoria = g.Key,

                                         // Calculate the total quantity of books sold and total revenue for each category
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),

                                         // Group the books by their id to get aggregated data for books with the same id
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,

                                                 // Calculate the total quantity sold and total revenue for each book
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))
                                             }).ToList()
                                     };
                var totalAggregatedData = new
                {
                    TotalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    TotalRevenue = aggregatedData.Sum(x => x.TotalRevenue)
                };
             
                // Combine the aggregated data and total aggregated data into a single object
                var result = new

                {
                    AggregatedData = aggregatedData,
                    TotalAggregatedData = totalAggregatedData
                };

                // Serialize the result to JSON using the Newtonsoft.Json library
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            
        }
            else {
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                     where (DateTime)transaction.Element("datum") >= startDate
                                     && (DateTime)transaction.Element("datum") <= endDate
                                     && transaction.Element("typ_transakcie").Value == "predaj"
                                     group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor1").Value into g
                                     select new
                                     {
                                         // Store the author1 as Podkategoria
                                         Podkategoria = g.Key,
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new {
                                                 Id = x.Key,
                                                 Quantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 Revenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))
                                             })
                                     };
                // Group by author2 - toto sluzi len na analyticke uceli to Totalneho poctu predananzch knih a totalneho prijmu sa to nezaratava ale
                // aby sme mali prehlad o prijmi a predajoch knih aj podla autora dva a mohli to vyuzit pri drill down operacii 

                
                    var aggregatedData2 = from book in booksData.Descendants("book")
                                          join transaction in transactionsData.Descendants("transakcia")
                                          on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                          where (DateTime)transaction.Element("datum") >= startDate
                                          && (DateTime)transaction.Element("datum") <= endDate
                                          && transaction.Element("typ_transakcie").Value == "predaj"
                                          where book.Element("autori").Element("autor2").Value != "-"
                                          group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor2").Value into g
                                          select new
                                          {
                                              // Store the author2 as Podkategoria
                                              Podkategoria = g.Key,
                                              TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                              TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                              Books = g.GroupBy(x => x.Book.Element("id").Value)
                                                  .Select(x => new {
                                                      Id = x.Key,
                                                      Quantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                      Revenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))
                                                  })
                                          };
                
                // Calculate the total quantity and total revenue for all books
                var totalAggregatedData = new
                {
                    TotalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    TotalRevenue = aggregatedData.Sum(x => x.TotalRevenue)
                };
                var combinedData = aggregatedData.Concat(aggregatedData2);
                // Combine the aggregated data and total aggregated data into a single object
                var result = new

                {
                    AggregatedData = combinedData,
                    TotalAggregatedData = totalAggregatedData
                };

                // Serialize the result to JSON using the Newtonsoft.Json library
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(JsonConvert.SerializeObject(result, Formatting.Indented));
            }

           
        }
       
    }
}








