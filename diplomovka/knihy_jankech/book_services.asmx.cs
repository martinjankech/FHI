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
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using knihy_jankech;
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

        private String fileBookInfoTest = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\book_moje_test.xml ";
        private String fileBookTransactionInfoTest = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\book_transakcie_moje_test.xml ";

       

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
        //[WebMethod]
        //public void SaveImage()
        //{
        //    var request = HttpContext.Current.Request;
        //    var postedFile = request.Files[0];

        //    var imageData = new ImageData();
        //    imageData.ImageName = Path.GetFileName(postedFile.FileName);
        //    imageData.ImageBytes = new byte[postedFile.ContentLength];
        //    postedFile.InputStream.Read(imageData.ImageBytes, 0, postedFile.ContentLength);

        //    // Save the image to the file system
        //    string filePath = Path.Combine(Server.MapPath("~/imgs"), imageData.ImageName);
        //    System.IO.File.WriteAllBytes(filePath, imageData.ImageBytes);
        //}
        public void ValidateDate(string startDate, string endDate)
        {
            DateTime start, end;
            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out start) ||
                !DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out end))
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Invalid date format provided. Correct format is yyyy-MM-dd");
                return;
            }
        }

            [WebMethod]
        public void AddBook()
        {
            var request = HttpContext.Current.Request;
            var postedFile = request.Files[0];

            var bookData = new BookData();
            
            bookData.Nazov = request["nazov"];
            bookData.Autor1 = request["autor1"];
            bookData.Autor2 = request["autor2"];
            bookData.Kategoria = request["kategoria"];
            bookData.Isbn = request["isbn"];
            bookData.Jazyk = request["jazyk"];
            bookData.Pocet_stran = request["pocet_stran"];
            bookData.Vazba = request["vazba"];
            bookData.Rok_vydania = request["rok_vydania"];
            bookData.Vydavatelstvo = request["vydavatelstvo"];
            bookData.Predajna_cena = Convert.ToDecimal(request["predajna_cena"]);
            bookData.Nakupna_cena = Convert.ToDecimal(request["nakupna_cena"]);   
            bookData.Obsah = request["obsah"];
            bookData.Priemerne_hodnotenie = request["priemerne_hodnotenie"];
            bookData.ImageName = Path.GetFileName(postedFile.FileName);
            bookData.ImageBytes = new byte[postedFile.ContentLength];
            postedFile.InputStream.Read(bookData.ImageBytes, 0, postedFile.ContentLength);

            // Load the existing XML file
            string xmlFilePath = fileBookInfo;
            XElement xmlDoc = XElement.Load(xmlFilePath);
            var lastBookId = xmlDoc.Element("books").Elements("book").Max(x => (int?)x.Element("id")) ?? 0;
            bookData.Id = (lastBookId + 1).ToString();

            // Add the new book element to the XML file

            XElement bookElement = new XElement("book",
                new XElement("id", bookData.Id),
                new XElement("nazov", bookData.Nazov),
                 new XElement("autori",
                    new XElement("autor1", bookData.Autor1),
                    new XElement("autor2", bookData.Autor2)),
                new XElement("kategoria", bookData.Kategoria),
                new XElement("isbn", bookData.Isbn),
                new XElement("jazyk", bookData.Jazyk),
                new XElement("pocet_stran", bookData.Pocet_stran),
                new XElement("vazba", bookData.Vazba),
                new XElement("rok_vydania", bookData.Rok_vydania),
                new XElement("vydavatelstvo", bookData.Vydavatelstvo),
                new XElement("predajna_cena", bookData.Predajna_cena),
                new XElement("nakupna_cena", bookData.Nakupna_cena),
                new XElement("marza", (bookData.Predajna_cena - bookData.Nakupna_cena) / bookData.Predajna_cena * 100),
        new XElement("zisk_kus", bookData.Predajna_cena - bookData.Nakupna_cena),
        new XElement("obsah", bookData.Obsah),
        new XElement("priemerne_hodnotenie", bookData.Priemerne_hodnotenie),
        new XElement("obrazok", "../img/"+bookData.Id+".jpg"));
       
         
            xmlDoc.Element("books").Add(bookElement);
            xmlDoc.Save(xmlFilePath);
            // Save the image to the file system
            string imageFilePath = Path.Combine(Server.MapPath("~/img"), "../img/" + bookData.Id + ".jpg");
            System.IO.File.WriteAllBytes(imageFilePath, bookData.ImageBytes);
        }

        [WebMethod]
        public string UpdateBook()
        {
            try
            {
                var request = HttpContext.Current.Request;

                var bookData = new BookData();

                bookData.Id = request["id"];
                bookData.Nazov = request["nazov"];
                bookData.Autor1 = request["autor1"];
                bookData.Autor2 = request["autor2"];
                bookData.Kategoria = request["kategoria"];
                bookData.Isbn = request["isbn"];
                bookData.Jazyk = request["jazyk"];
                bookData.Pocet_stran = request["pocet_stran"];
                bookData.Vazba = request["vazba"];
                bookData.Rok_vydania = request["rok_vydania"];
                bookData.Vydavatelstvo = request["vydavatelstvo"];
                bookData.Predajna_cena = Convert.ToDecimal(request["predajna_cena"]);
                bookData.Nakupna_cena = Convert.ToDecimal(request["nakupna_cena"]);
                bookData.Obsah = request["obsah"];
                bookData.Priemerne_hodnotenie = request["priemerne_hodnotenie"];

                if (request.Files.Count > 0)
                {
                    var postedFile = request.Files[0];
                    bookData.ImageName = Path.GetFileName(postedFile.FileName);
                    bookData.ImageBytes = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(bookData.ImageBytes, 0, postedFile.ContentLength);
                }

                string xmlFilePath = fileBookInfo;
                XElement xmlDoc = XElement.Load(xmlFilePath);

                var bookElement = xmlDoc.Element("books").Elements("book").FirstOrDefault(x => x.Element("id").Value == bookData.Id);

                if (bookElement == null)
                {
                    return "Error: Book with specified ID was not found.";
                }

                bookElement.SetElementValue("nazov", bookData.Nazov);
                bookElement.Element("autori").SetElementValue("autor1", bookData.Autor1);
                bookElement.Element("autori").SetElementValue("autor2", bookData.Autor2);
                bookElement.SetElementValue("kategoria", bookData.Kategoria);
                bookElement.SetElementValue("isbn", bookData.Isbn);
                bookElement.SetElementValue("jazyk", bookData.Jazyk);
                bookElement.SetElementValue("pocet_stran", bookData.Pocet_stran);
                bookElement.SetElementValue("vazba", bookData.Vazba);
                bookElement.SetElementValue("rok_vydania", bookData.Rok_vydania);
                bookElement.SetElementValue("vydavatelstvo", bookData.Vydavatelstvo);
                bookElement.SetElementValue("predajna_cena", bookData.Predajna_cena.ToString());
                bookElement.SetElementValue("nakupna_cena", bookData.Nakupna_cena.ToString());
                bookElement.SetElementValue("obsah", bookData.Obsah);
                bookElement.SetElementValue("priemerne_hodnotenie", bookData.Priemerne_hodnotenie);

             
        
    if (bookData.ImageBytes != null && bookData.ImageBytes.Length > 0)
                {
                    string imageFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/img"), "../img/" + bookData.Id + ".jpg");
                    System.IO.File.WriteAllBytes(imageFilePath, bookData.ImageBytes);
                }

                xmlDoc.Save(xmlFilePath);

                return "Book was successfully updated.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }

        [WebMethod]
        public string DeleteBook(string id)
        {
            try
            {
                // Load the existing XML file
                string xmlFilePath = fileBookInfo;
                XElement xmlDoc = XElement.Load(xmlFilePath);

       
                            // Find the book with the specified id
                            XElement bookToDelete = xmlDoc.Element("books").Elements("book").FirstOrDefault(x => x.Element("id").Value == id);

                if (bookToDelete != null)
                {
                    // Remove the book element from the XML file
                    bookToDelete.Remove();
                    xmlDoc.Save(xmlFilePath);

                    // Delete the image from the file system
                    string imageFilePath = Path.Combine(Server.MapPath("~/img"), "../img/" + id + ".jpg");
                    System.IO.File.Delete(imageFilePath);

                    return "Success";
                }
                else
                {
                    return "Book not found";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
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
            if (!sortOrder.Equals("ascending") && !sortOrder.Equals("descending"))
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
          
            // Use LINQ to join the information from the two XML files and filter the results based on selected category and selected value
            if (selectedAtribute != "vsetky")
            {
                var result = from b in booksXml.Descendants("book")
                             join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)
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

                if (sortOrder == "ascending")
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
                else if (sortOrder == "descending")
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
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)




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

                if (sortOrder == "ascending")
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
                else if (sortOrder == "descending")
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
           
            // Use LINQ to join the information from the two XML files and filter the results based on selected category and selected value
            if (selectedAtribute != "vsetky")
            {
                var result = from b in booksXml.Descendants("book")
                             join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)
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
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")

                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)
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
        public void GetAggregatedDataSellByDateAndSelectedCategory(string atribute, DateTime startDate, DateTime endDate)
        {
            // Load the books data from XML
            XDocument booksData = XDocument.Load(fileBookInfo);
            // Load the transactions data from XML
            XDocument transactionsData = XDocument.Load(fileBookTransactionInfo);
            if (atribute != "autor" && atribute != "autori")
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
                                     group new { Book = book, Transaction = transaction } by book.Element(atribute).Value into g
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
            else
            {
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
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))
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
                                              .Select(x => new
                                              {

                                                  Id = x.Key,
                                                  Name = x.First().Book.Element("nazov").Value,
                                                  TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                  TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))
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
        [WebMethod]
        public void SortedDrillDownByAtributeDataBetweenTwoDatesSell(string atribute, string startDate, string endDate, string sortingField = "", string sortingOrder = "", string optionalParameter = "")
        {
            // Load the books data from XML
            XDocument booksData = XDocument.Load(fileBookInfo);
            // Load the transactions data from XML
            XDocument transactionsData = XDocument.Load(fileBookTransactionInfo);
            DateTime start, end;
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }

            if (string.IsNullOrEmpty(atribute) || startDate == null || endDate == null || sortingOrder == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Jedna alebo viacero vstupov nebolo vyplnených");
                return;
            }
            if (start > end)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Zaciatočný dátum nesmie byť vačší ako konečný");
                
              
                return;
            }

            if (sortingOrder != "ascending" && sortingOrder != "descending")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Sortovanie može byť iba zostupne alebo vzostupne");
                
            }

            if (sortingField != "hodnota" && sortingField != "nazov")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("zoradovat sa može iba podľa názvu alebo hodnoty");
              
            }
            double days = (end - start).Days + 1;
            if (atribute != "autor" && atribute != "autori")
            {

                // Join the books data and transactions data on book id to get all information related to each transaction
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")

                                     // Filter the transactions to only those with a date within the specified range and of type "predaj"
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "predaj"
                                   


                                     // Group the transactions by the specified category element value
                                     group new { Book = book, Transaction = transaction } by book.Element(atribute).Value into g
                                     select new
                                     {
                                         // Store the category value as Podkategoria
                                         Podkategoria = g.Key,


                                         // Calculate the total quantity of books sold and total revenue for each category
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) /days,

                                         // Group the books by their id to get aggregated data for books with the same id
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,

                                                 // Calculate the total quantity sold and total revenue for each book
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                 AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo")))/days,
                                                 AverageTotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena"))/days


                                             }).ToList()
                                     };
                if (!aggregatedData.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }

                // Sort the aggregated data based on the sortingField and sortingOrder parameters
                if (sortingField == "nazov")
                {
                    aggregatedData = sortingOrder == "ascending"
                    ? aggregatedData.OrderBy(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderBy(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalRevenue = x.TotalRevenue,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalRevenue=x.AverageTotalRevenue,
                            Books = x.Books.OrderBy(y => y.Name).ToList()

                        })
                    : aggregatedData.OrderByDescending(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalRevenue = x.TotalRevenue,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalRevenue = x.AverageTotalRevenue,
                            Books = x.Books.OrderByDescending(y => y.Name).ToList()
                        });
                }
                else if (sortingField == "hodnota" && optionalParameter == "quantity")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalRevenue = x.TotalRevenue,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalRevenue = x.AverageTotalRevenue,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                else if (sortingField == "hodnota" && optionalParameter == "revenue")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalRevenue = x.TotalRevenue,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalRevenue = x.AverageTotalRevenue,
                        Books = x.Books.OrderBy(y => y.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                var maxRevenuePodkategoria = aggregatedData.Max(x => x.TotalRevenue);
                var minRevenuePodkategoria = aggregatedData.Min(x => x.TotalRevenue);
                var maxQuantityPodkategoria = aggregatedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = aggregatedData.Min(x => x.TotalQuantity);

                var maxRevenueBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalRevenue);
                var minRevenueBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalRevenue);
                var maxQuantityBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);

                var namePodkategoriaMaxRevenue = aggregatedData.Where(x => x.TotalRevenue == maxRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMinRevenue = aggregatedData.Where(x => x.TotalRevenue == minRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = aggregatedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = aggregatedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == maxRevenueBook).First().Name;
                var nameBookMinRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == minRevenueBook).First().Name;
                var nameBookMaxQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;

                var totalAggregatedData = new
                {
                    totalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    totalRevenue = aggregatedData.Sum(x => x.TotalRevenue),
                    averageTotalDailyQuantity = aggregatedData.Sum(x=>x.TotalQuantity)/days,
                    averageTotalDailyRevenue = aggregatedData.Sum(x => x.TotalRevenue) / days,
                    namePodkategoriaMaxRevenue=namePodkategoriaMaxRevenue,
                    maxRevenuePodkategoria=maxRevenuePodkategoria,
                    namePodkategoriaMinRevenue=namePodkategoriaMinRevenue,
                    minRevenuePodkategoria=minRevenuePodkategoria,
                    namePodkategoriaMaxQuantity=namePodkategoriaMaxQuantity,
                    maxQuantityPodkategoria=maxQuantityPodkategoria,
                    namePodkategoriaMinQuantity=namePodkategoriaMinQuantity,
                    minQuantityPodkategoria=minQuantityPodkategoria,
                    nameBookMaxRevenue=nameBookMaxRevenue,
                 maxRevenueBook=maxRevenueBook,
                    nameBookMinRevenue = nameBookMinRevenue,
                    minRevenueBook = minRevenueBook,
                    nameBookMaxQuantity = nameBookMaxQuantity,
                    maxQuantityBook = maxQuantityBook,
                    nameBookMinQuantity = nameBookMinQuantity,
                    minQuantityBook = minQuantityBook,




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
            else
            {
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "predaj"
                                     group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor1").Value into g
                                     select new
                                     {
                                         // Store the author1 as Podkategoria
                                         Podkategoria = g.Key,
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                 AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))) / days,
                                                 AverageTotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")) / days
                                             }).ToList()
                                     };
                if (!aggregatedData.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                // Group by author2 - toto sluzi len na analyticke uceli to Totalneho poctu predananzch knih a totalneho prijmu sa to nezaratava ale
                // aby sme mali prehlad o prijmi a predajoch knih aj podla autora dva a mohli to vyuzit pri drill down operacii 


                var aggregatedData2 = from book in booksData.Descendants("book")
                                      join transaction in transactionsData.Descendants("transakcia")
                                      on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                      where (DateTime)transaction.Element("datum") >= start
                                      && (DateTime)transaction.Element("datum") <= end
                                      && transaction.Element("typ_transakcie").Value == "predaj"
                                      where book.Element("autori").Element("autor2").Value != "-"
                                      group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor2").Value into g
                                      select new
                                      {
                                          // Store the author2 as Podkategoria
                                          Podkategoria = g.Key,
                                          TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                          TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                          AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                          AverageTotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,
                                          Books = g.GroupBy(x => x.Book.Element("id").Value)
                                              .Select(x => new
                                              {

                                                  Id = x.Key,
                                                  Name = x.First().Book.Element("nazov").Value,
                                                  TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                  TotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                  AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))) / days,
                                                  AverageTotalRevenue = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")) / days
                                              }).ToList()
                                      };
                if (!aggregatedData2.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                if (sortingField == "nazov")
                {
                    aggregatedData = sortingOrder == "ascending"
                    ? aggregatedData.OrderBy(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderBy(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalRevenue = x.TotalRevenue,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalRevenue = x.AverageTotalRevenue,
                            Books = x.Books.OrderBy(y => y.Name).ToList()

                        })
                    : aggregatedData.OrderByDescending(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalRevenue = x.TotalRevenue,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalRevenue = x.AverageTotalRevenue,
                            Books = x.Books.OrderByDescending(y => y.Name).ToList()
                        });
                    aggregatedData2 = sortingOrder == "ascending"
                   ? aggregatedData.OrderBy(x => x.Podkategoria)
                       .ThenBy(x => x.Books.OrderBy(y => y.Name))
                       .Select(x => new
                       {
                           Podkategoria = x.Podkategoria,
                           TotalQuantity = x.TotalQuantity,
                           TotalRevenue = x.TotalRevenue,
                           AverageTotalQuantity = x.AverageTotalQuantity,
                           AverageTotalRevenue = x.AverageTotalRevenue,
                           Books = x.Books.OrderBy(y => y.Name).ToList()

                       })
                   : aggregatedData2.OrderByDescending(x => x.Podkategoria)
                       .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                       .Select(x => new
                       {
                           Podkategoria = x.Podkategoria,
                           TotalQuantity = x.TotalQuantity,
                           TotalRevenue = x.TotalRevenue,
                           AverageTotalQuantity = x.AverageTotalQuantity,
                           AverageTotalRevenue = x.AverageTotalRevenue,
                           Books = x.Books.OrderByDescending(y => y.Name).ToList()
                       });
                }
                else if (sortingField == "hodnota" && optionalParameter == "quantity")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalRevenue = x.TotalRevenue,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalRevenue = x.AverageTotalRevenue,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                    aggregatedData2 = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalRevenue = x.TotalRevenue,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalRevenue = x.AverageTotalRevenue,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                else if (sortingField == "hodnota" && optionalParameter == "revenue")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalRevenue = x.TotalRevenue,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalRevenue = x.AverageTotalRevenue,
                        Books = x.Books.OrderBy(y => y.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                    aggregatedData2 = aggregatedData.OrderBy(x => x.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                   .ThenBy(x => x.Podkategoria)
                   .Select(x => new
                   {
                       Podkategoria = x.Podkategoria,
                       TotalQuantity = x.TotalQuantity,
                       TotalRevenue = x.TotalRevenue,
                       AverageTotalQuantity = x.AverageTotalQuantity,
                       AverageTotalRevenue = x.AverageTotalRevenue,
                       Books = x.Books.OrderBy(y => y.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
                   .ThenBy(y => y.Name)
                   .ToList()
                   });
                }
                var maxRevenuePodkategoria = aggregatedData.Max(x => x.TotalRevenue);
                var minRevenuePodkategoria = aggregatedData.Min(x => x.TotalRevenue);
                var maxQuantityPodkategoria = aggregatedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = aggregatedData.Min(x => x.TotalQuantity);

                var maxRevenueBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalRevenue);
                var minRevenueBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalRevenue);
                var maxQuantityBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);

                var namePodkategoriaMaxRevenue = aggregatedData.Where(x => x.TotalRevenue == maxRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMinRevenue = aggregatedData.Where(x => x.TotalRevenue == minRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = aggregatedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = aggregatedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == maxRevenueBook).First().Name;
                var nameBookMinRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == minRevenueBook).First().Name;
                var nameBookMaxQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;


                // Calculate the total quantity and total revenue for all books
                var totalAggregatedData = new
                {
                    totalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    totalRevenue = aggregatedData.Sum(x => x.TotalRevenue),
                    averageTotalDailyQuantity = aggregatedData.Sum(x => x.TotalQuantity) / days,
                    averageTotalDailyRevenue = aggregatedData.Sum(x => x.TotalRevenue) / days,
                    namePodkategoriaMaxRevenue = namePodkategoriaMaxRevenue,
                    maxRevenuePodkategoria = maxRevenuePodkategoria,
                    namePodkategoriaMinRevenue = namePodkategoriaMinRevenue,
                    minRevenuePodkategoria = minRevenuePodkategoria,
                    namePodkategoriaMaxQuantity = namePodkategoriaMaxQuantity,
                    maxQuantityPodkategoria = maxQuantityPodkategoria,
                    namePodkategoriaMinQuantity = namePodkategoriaMinQuantity,
                    minQuantityPodkategoria = minQuantityPodkategoria,
                    nameBookMaxRevenue = nameBookMaxRevenue,
                    maxRevenueBook = maxRevenueBook,
                    nameBookMinRevenue = nameBookMinRevenue,
                    minRevenueBook = minRevenueBook,
                    nameBookMaxQuantity = nameBookMaxQuantity,
                    maxQuantityBook = maxQuantityBook,
                    nameBookMinQuantity = nameBookMinQuantity,
                    minQuantityBook = minQuantityBook,
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

        [WebMethod]
        public void SortedDrillDownByAtributeDataBetweenTwoDatesCost(string atribute, string startDate, string endDate, string sortingField = "", string sortingOrder = "", string optionalParameter = "")
        {
            // Load the books data from XML
            XDocument booksData = XDocument.Load(fileBookInfo);
            // Load the transactions data from XML
            XDocument transactionsData = XDocument.Load(fileBookTransactionInfo);
            DateTime start, end;
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }

            if (string.IsNullOrEmpty(atribute) || startDate == null || endDate == null || sortingOrder == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Jedna alebo viacero vstupov nebolo vyplnených");
                return;
            }
            if (start > end)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Zaciatočný dátum nesmie byť vačší ako konečný");


                return;
            }

            if (sortingOrder != "ascending" && sortingOrder != "descending")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Sortovanie može byť iba zostupne alebo vzostupne");

            }

            if (sortingField != "hodnota" && sortingField != "nazov")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("zoradovat sa može iba podľa názvu alebo hodnoty");

            }
            double days = (end - start).Days + 1;
            if (atribute != "autor" && atribute != "autori")
            {

                // Join the books data and transactions data on book id to get all information related to each transaction
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")

                                     // Filter the transactions to only those with a date within the specified range and of type "predaj"
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "nákup"



                                     // Group the transactions by the specified category element value
                                     group new { Book = book, Transaction = transaction } by book.Element(atribute).Value into g
                                     select new
                                     {
                                         // Store the category value as Podkategoria
                                         Podkategoria = g.Key,


                                         // Calculate the total quantity of books sold and total revenue for each category
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,

                                         // Group the books by their id to get aggregated data for books with the same id
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,

                                                 // Calculate the total quantity sold and total revenue for each book
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                 AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))) / days,
                                                 AverageTotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")) / days


                                             }).ToList()
                                     };
                if (!aggregatedData.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }

                // Sort the aggregated data based on the sortingField and sortingOrder parameters
                if (sortingField == "nazov")
                {
                    aggregatedData = sortingOrder == "ascending"
                    ? aggregatedData.OrderBy(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderBy(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalCost = x.TotalCost,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalCost = x.AverageTotalCost,
                            Books = x.Books.OrderBy(y => y.Name).ToList()

                        })
                    : aggregatedData.OrderByDescending(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalCost = x.TotalCost,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalCost = x.AverageTotalCost,
                            Books = x.Books.OrderByDescending(y => y.Name).ToList()
                        });
                }
                else if (sortingField == "hodnota" && optionalParameter == "quantity")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalCost = x.TotalCost,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalCost = x.AverageTotalCost,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                else if (sortingField == "hodnota" && optionalParameter == "cost")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalCost = x.TotalCost,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalCost = x.AverageTotalCost,
                        Books = x.Books.OrderBy(y => y.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                var maxCostPodkategoria = aggregatedData.Max(x => x.TotalCost);
                var minCostPodkategoria = aggregatedData.Min(x => x.TotalCost);
                var maxQuantityPodkategoria = aggregatedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = aggregatedData.Min(x => x.TotalQuantity);

                var maxCostBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalCost);
                var minCostBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalCost);
                var maxQuantityBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);

                var namePodkategoriaMaxCost = aggregatedData.Where(x => x.TotalCost == maxCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMinCost = aggregatedData.Where(x => x.TotalCost == minCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = aggregatedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = aggregatedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxCost = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalCost == maxCostBook).First().Name;
                var nameBookMinCost = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalCost == minCostBook).First().Name;
                var nameBookMaxQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;

                var totalAggregatedData = new
                {
                    totalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    totalCost = aggregatedData.Sum(x => x.TotalCost),
                    averageTotalDailyQuantity = aggregatedData.Sum(x => x.TotalQuantity) / days,
                    averageTotalDailyCost = aggregatedData.Sum(x => x.TotalCost) / days,
                    namePodkategoriaMaxCost = namePodkategoriaMaxCost,
                    maxCostPodkategoria = maxCostPodkategoria,
                    namePodkategoriaMinCost = namePodkategoriaMinCost,
                    minCostPodkategoria = minCostPodkategoria,
                    namePodkategoriaMaxQuantity = namePodkategoriaMaxQuantity,
                    maxQuantityPodkategoria = maxQuantityPodkategoria,
                    namePodkategoriaMinQuantity = namePodkategoriaMinQuantity,
                    minQuantityPodkategoria = minQuantityPodkategoria,
                    nameBookMaxCost = nameBookMaxCost,
                    maxCostBook = maxCostBook,
                    nameBookMinCost = nameBookMinCost,
                    minCostBook = minCostBook,
                    nameBookMaxQuantity = nameBookMaxQuantity,
                    maxQuantityBook = maxQuantityBook,
                    nameBookMinQuantity = nameBookMinQuantity,
                    minQuantityBook = minQuantityBook,




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
            else
            {
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "nákup"
                                     group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor1").Value into g
                                     select new
                                     {
                                         // Store the author1 as Podkategoria
                                         Podkategoria = g.Key,
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,
                                                 TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                 TotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                 AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))) / days,
                                                 AverageTotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")) / days
                                             }).ToList()
                                     };
                if (!aggregatedData.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                // Group by author2 - toto sluzi len na analyticke uceli to Totalneho poctu predananzch knih a totalneho prijmu sa to nezaratava ale
                // aby sme mali prehlad o prijmi a predajoch knih aj podla autora dva a mohli to vyuzit pri drill down operacii 


                var aggregatedData2 = from book in booksData.Descendants("book")
                                      join transaction in transactionsData.Descendants("transakcia")
                                      on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                      where (DateTime)transaction.Element("datum") >= start
                                      && (DateTime)transaction.Element("datum") <= end
                                      && transaction.Element("typ_transakcie").Value == "nákup"
                                      where book.Element("autori").Element("autor2").Value != "-"
                                      group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor2").Value into g
                                      select new
                                      {
                                          // Store the author2 as Podkategoria
                                          Podkategoria = g.Key,
                                          TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                          TotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                          AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                          AverageTotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,
                                          Books = g.GroupBy(x => x.Book.Element("id").Value)
                                              .Select(x => new
                                              {

                                                  Id = x.Key,
                                                  Name = x.First().Book.Element("nazov").Value,
                                                  TotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))),
                                                  TotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")),
                                                  AverageTotalQuantity = x.Sum(y => Math.Abs((int)y.Transaction.Element("mnozstvo"))) / days,
                                                  AverageTotalCost = x.Sum(y => (double)y.Transaction.Element("celkovo_cena")) / days
                                              }).ToList()
                                      };
                if (!aggregatedData2.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                if (sortingField == "nazov")
                {
                    aggregatedData = sortingOrder == "ascending"
                    ? aggregatedData.OrderBy(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderBy(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalCost = x.TotalCost,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalCost = x.AverageTotalCost,
                            Books = x.Books.OrderBy(y => y.Name).ToList()

                        })
                    : aggregatedData.OrderByDescending(x => x.Podkategoria)
                        .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                        .Select(x => new
                        {
                            Podkategoria = x.Podkategoria,
                            TotalQuantity = x.TotalQuantity,
                            TotalCost = x.TotalCost,
                            AverageTotalQuantity = x.AverageTotalQuantity,
                            AverageTotalCost = x.AverageTotalCost,
                            Books = x.Books.OrderByDescending(y => y.Name).ToList()
                        });
                    aggregatedData2 = sortingOrder == "ascending"
                   ? aggregatedData.OrderBy(x => x.Podkategoria)
                       .ThenBy(x => x.Books.OrderBy(y => y.Name))
                       .Select(x => new
                       {
                           Podkategoria = x.Podkategoria,
                           TotalQuantity = x.TotalQuantity,
                           TotalCost = x.TotalCost,
                           AverageTotalQuantity = x.AverageTotalQuantity,
                           AverageTotalCost = x.AverageTotalCost,
                           Books = x.Books.OrderBy(y => y.Name).ToList()

                       })
                   : aggregatedData2.OrderByDescending(x => x.Podkategoria)
                       .ThenBy(x => x.Books.OrderByDescending(y => y.Name))
                       .Select(x => new
                       {
                           Podkategoria = x.Podkategoria,
                           TotalQuantity = x.TotalQuantity,
                           TotalCost = x.TotalCost,
                           AverageTotalQuantity = x.AverageTotalQuantity,
                           AverageTotalCost = x.AverageTotalCost,
                           Books = x.Books.OrderByDescending(y => y.Name).ToList()
                       });
                }
                else if (sortingField == "hodnota" && optionalParameter == "quantity")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalCost = x.TotalCost,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalCost = x.AverageTotalCost,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                    aggregatedData2 = aggregatedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalCost = x.TotalCost,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalCost = x.AverageTotalCost,
                        Books = x.Books.OrderBy(y => y.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                }
                else if (sortingField == "hodnota" && optionalParameter == "revenue")
                {
                    aggregatedData = aggregatedData.OrderBy(x => x.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(x => x.Podkategoria)
                    .Select(x => new
                    {
                        Podkategoria = x.Podkategoria,
                        TotalQuantity = x.TotalQuantity,
                        TotalCost = x.TotalCost,
                        AverageTotalQuantity = x.AverageTotalQuantity,
                        AverageTotalCost = x.AverageTotalCost,
                        Books = x.Books.OrderBy(y => y.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                    .ThenBy(y => y.Name)
                    .ToList()
                    });
                    aggregatedData2 = aggregatedData.OrderBy(x => x.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                   .ThenBy(x => x.Podkategoria)
                   .Select(x => new
                   {
                       Podkategoria = x.Podkategoria,
                       TotalQuantity = x.TotalQuantity,
                       TotalCost = x.TotalCost,
                       AverageTotalQuantity = x.AverageTotalQuantity,
                       AverageTotalCost = x.AverageTotalCost,
                       Books = x.Books.OrderBy(y => y.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
                   .ThenBy(y => y.Name)
                   .ToList()
                   });
                }
                var maxCostPodkategoria = aggregatedData.Max(x => x.TotalCost);
                var minCostPodkategoria = aggregatedData.Min(x => x.TotalCost);
                var maxQuantityPodkategoria = aggregatedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = aggregatedData.Min(x => x.TotalQuantity);

                var maxCostBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalCost);
                var minCostBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalCost);
                var maxQuantityBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);

                var namePodkategoriaMaxCost = aggregatedData.Where(x => x.TotalCost == maxCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMinCost = aggregatedData.Where(x => x.TotalCost == minCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = aggregatedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = aggregatedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxCost = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalCost == maxCostBook).First().Name;
                var nameBookMinCost = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalCost == minCostBook).First().Name;
                var nameBookMaxQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;


                // Calculate the total quantity and total revenue for all books
                var totalAggregatedData = new
                {
                    totalQuantity = aggregatedData.Sum(x => x.TotalQuantity),
                    totalCost = aggregatedData.Sum(x => x.TotalCost),
                    averageTotalDailyQuantity = aggregatedData.Sum(x => x.TotalQuantity) / days,
                    averageTotalDailyCost = aggregatedData.Sum(x => x.TotalCost) / days,
                    namePodkategoriaMaxCost = namePodkategoriaMaxCost,
                    maxCostPodkategoria = maxCostPodkategoria,
                    namePodkategoriaMinCost = namePodkategoriaMinCost,
                    minCostPodkategoria = minCostPodkategoria,
                    namePodkategoriaMaxQuantity = namePodkategoriaMaxQuantity,
                    maxQuantityPodkategoria = maxQuantityPodkategoria,
                    namePodkategoriaMinQuantity = namePodkategoriaMinQuantity,
                    minQuantityPodkategoria = minQuantityPodkategoria,
                    nameBookMaxCost = nameBookMaxCost,
                    maxCostBook = maxCostBook,
                    nameBookMinCost = nameBookMinCost,
                    minCostBook = minCostBook,
                    nameBookMaxQuantity = nameBookMaxQuantity,
                    maxQuantityBook = maxQuantityBook,
                    nameBookMinQuantity = nameBookMinQuantity,
                    minQuantityBook = minQuantityBook,
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









