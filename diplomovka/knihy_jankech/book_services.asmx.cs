using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Formatting = Newtonsoft.Json.Formatting;

namespace knihy_jankech
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class book_services : System.Web.Services.WebService
    {// tieto cesty treba nastaviť pokiaľ využivame absolutne cesty
        //private String fileBookInfo = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\books.xml ";
        //private String fileBookTransactionInfo = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\books_transactions.xml ";
        //private String fileOutputSingleSearch = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\output.xml";
        //private String fileAmountFilterPath = "D:\\git_repozitare\\FHI\\diplomovka\\knihy_jankech\\xml\\outputfiles";
        private String fileBookInfo = HttpContext.Current.Server.MapPath("~/xml/books.xml");
        private String fileBookTransactionInfo = HttpContext.Current.Server.MapPath("~/xml/books_transactions.xml");
        private String fileOutputSingleSearch = HttpContext.Current.Server.MapPath("~/xml/output.xml");
        private String fileAmountFilterPath = HttpContext.Current.Server.MapPath("~/xml/outputfiles");
        // newebové metódy
        // 3 metoty na nacitanie xmldocumentu xdocumentu(pouzivaný pri linq nacita celý dokument ) a xelementu(tiež linq ale konkretny element  )
        public XmlDocument LoadXmlDocument(string filePath)
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
                return null;
            }
        }

        public XElement LoadXElement(string filePath)
        {
            try
            {
                // Načítanie XML elementu zo zadaného súboru
                XElement element = XElement.Load(filePath);
                return element;
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
                // Ak sa vyskytne chyba pri parsovaní XML elementu, nastaví sa status kód na 400 a vráti sa popisná chybová správa
                Context.Response.StatusCode = 400;
                Context.Response.StatusDescription = "Chyba pri parsovaní XML elementu.";
                Context.Response.Write("Chyba pri parsovaní XML elementu: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Ak sa vyskytne akákoľvek iná chyba pri načítaní elementu, nastaví sa status kód na 500 a vráti sa popisná chybová správa
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Chyba pri načítaní elementu.";
                Context.Response.Write("Chyba pri načítaní elementu: " + ex.Message);
                return null;
            }
        }
        public XDocument LoadXDocument(string filePath)
        {
            try
            {
                // Načítanie XML dokumentu zo zadaného súboru
                XDocument document = XDocument.Load(filePath);
                return document;
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
                return null;
            }
        }


        public  string CreateTimestamp()
        {
            // Formátovať aktuálny dátum a čas pomocou vzoru "yyyy-MM-dd HH:mm:ss"
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        // metóda na zápis do jedného súboru (cesta k súboru je parameter) s časovou pečiatkov
        // Táto funkcia zapíše dokument XML do súboru s časovou značkou.Ak súbor už existuje,
        // kód ho načíta a pridá doň nové prvky, inak vytvorí nový dokument XML s koreňovým prvkom.
        // Kód prechádza v cykle každý uzol v údajoch, importuje ho do dokumentu, vytvorí element "output" (výstup), vytvorí element "timestamp" (časová pečiatka) s aktuálnym dátumom a časom,
        // pripojí importovaný uzol a časovú pečiatku k elementu output a  nakoniec pripojí výstupný prvok ku koreňovému prvku.Nakoniec sa dokument uloží do zadanej cesty k súboru.

        // metóda na zápis do súboru s časovou pečiatkov

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
                    doc = LoadXmlDocument(path);
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
                Context.Response.Write("Súbor sa nenašiel: " + ex.Message);
            }
            catch (XmlException ex)
            {
                // Výpis chyby, ak nastala chyba v XML súbore
                Context.Response.Write("Chyba XML: " + ex.Message);
            }
            catch (IOException ex)
            {
                // Výpis chyby, ak nastala chyba pri vstupe/výstupe
                Context.Response.Write("Chyba I/O: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Výpis chyby, ak nemáte oprávnenie k prístupu k súboru
                Context.Response.Write("Nedovolený prístup: " + ex.Message);
            }
        }
        public void SaveWebMethodResult(string xmlResult, string methodName, string[] parameters, string path)
        {
            // Skontroluje, či adresár existuje, ak nie, vytvorí ho
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // Vytvorí názov súboru pre XML súbor pomocou aktuálneho dátumu a času, názvu metódy a parametrov
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + methodName + "_" + string.Join("_", parameters) + ".xml";

            // Uloží XML výsledok do súboru
            string fullPath = Path.Combine(path, fileName);
            System.IO.File.WriteAllText(fullPath, xmlResult);
        }

        [WebMethod (Description = "pridá do xml súboru záznam o novej knihe")]
        public void AddBook()
        {
            try
            {// vytvorenie premennej typu HttpConetxt a ziskanie obsahuje aktualnej http požiadavky
                var request = HttpContext.Current.Request;

                // vytvorí sa nová inštancia triedy BookData
                var bookData = new BookData();
                // Kontrola parametrov z requestu a priradenie hodnôt.
                // Ak niektorý parameter chýba, funkcia vráti chybový kód a príslušnú hlášku
                if (string.IsNullOrEmpty(request["nazov"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter nazov");
                    return;
                }
                else
                {
                    bookData.Nazov = request["nazov"];
                }
                if (string.IsNullOrEmpty(request["autor1"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter autor1");
                    return;
                }
                else
                {
                    bookData.Autor1 = request["autor1"];
                }
                if (string.IsNullOrEmpty(request["autor2"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter autor2");
                    return;
                }
                else
                {
                    bookData.Autor2 = request["autor2"];
                }
                if (string.IsNullOrEmpty(request["kategoria"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter kategoria");
                    return;
                }
                else
                {
                    bookData.Kategoria = request["kategoria"];
                }
                if (string.IsNullOrEmpty(request["isbn"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter isbn");
                    return;
                }
                else
                {
                    bookData.Isbn = request["isbn"];
                }
                if (string.IsNullOrEmpty(request["jazyk"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter jazyk");
                    return;
                }
                else
                {
                    bookData.Jazyk = request["jazyk"];
                }
                if (string.IsNullOrEmpty(request["pocet_stran"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter pocet stran");
                    return;
                }
                else
                {
                    bookData.Pocet_stran = request["pocet_stran"];
                }
                if (string.IsNullOrEmpty(request["vazba"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter vazba");
                    return;
                }
                else
                {
                    bookData.Vazba = request["vazba"];
                }
                if (string.IsNullOrEmpty(request["rok_vydania"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter rok vydania");
                    return;
                }
                else
                {
                    bookData.Rok_vydania = request["rok_vydania"];
                }
                if (string.IsNullOrEmpty(request["vydavatelstvo"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter vydavatelstvo");
                    return;
                }
                else
                {
                    bookData.Vydavatelstvo = request["vydavatelstvo"];
                }
                if (string.IsNullOrEmpty(request["predajna_cena"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter predajna_cena");
                    return;
                }
                else
                {
                    bookData.Predajna_cena = decimal.Parse(request["predajna_cena"]);
                }
                if (string.IsNullOrEmpty(request["nakupna_cena"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter nakupna cena");
                    return;
                }
                else
                {
                    bookData.Nakupna_cena = decimal.Parse(request["nakupna_cena"]);
                }
                if (string.IsNullOrEmpty(request["obsah"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter obsah ");
                    return;
                }
                else
                {
                    bookData.Obsah = request["obsah"];
                }
                if (string.IsNullOrEmpty(request["priemerne_hodnotenie"]))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Zadajte parameter priemerne hodnotenie ");
                    return;
                }
                else
                {
                    bookData.Priemerne_hodnotenie = request["priemerne_hodnotenie"];
                }
                if (request.Files.Count == 0)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Vlozte obrazok knihy pozadovana velkost formatu je 430*600px");
                    return;
                }
                // získanie obrázka so requestu
                var postedFile = request.Files[0];
               // Tento riadok nastaví členskú premennú ImageName objektu bookData na názov súboru nahraného obrázka.
               // Metóda Path.GetFileName získa názov súboru z úplnej cesty k súboru, ktorá je uložená vo vlastnosti FileName objektu postedFile.
                bookData.ImageName = Path.GetFileName(postedFile.FileName);
               // Tento riadok deklaruje v objekte bookData nové pole bajtov s názvom ImageBytes, ktorého dĺžka sa rovná vlastnosti ContentLength objektu postedFile.
               //Toto pole bajtov sa použije na uloženie binárnych údajov nahraného obrázka.
                bookData.ImageBytes = new byte[postedFile.ContentLength];
                //Tento riadok načíta binárne údaje nahraného obrázka z člennskej premennje InputStream objektu postedFile a uloží ich do členneskej premmenej ImageBytes objektu bookData.
                //Metóda Read objektu InputStream prečíta zo streamu zadaný počet bajtov a uloží ich do zadaného poľa bajtov, pričom začína na zadanom offsete (ktorý je v tomto prípade 0).
                //Počet bajtov, ktoré sa majú prečítať, je určený vlastnostou ContentLength objektu postedFile.  
                postedFile.InputStream.Read(bookData.ImageBytes, 0, postedFile.ContentLength);

                // nacitanie xml súboru s knihami a ziskanie hodnoty najvačšieho id 
                string xmlFilePath = fileBookInfo;
                XElement xmlDoc = LoadXElement(xmlFilePath);
                var lastBookId = xmlDoc.Element("books").Elements("book").Max(x => (int?)x.Element("id")) ?? 0;
                bookData.Id = (lastBookId + 1).ToString();

                // pridanie novej knihy do xml suboru 

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
                    new XElement("marza", (bookData.Predajna_cena - bookData.Nakupna_cena) / bookData.Predajna_cena ),
            new XElement("zisk_kus", bookData.Predajna_cena - bookData.Nakupna_cena),
            new XElement("obsah", bookData.Obsah),
            new XElement("priemerne_hodnotenie", bookData.Priemerne_hodnotenie),
            new XElement("obrazok", "../img/" + bookData.Id + ".jpg"));


                xmlDoc.Element("books").Add(bookElement);
                xmlDoc.Save(xmlFilePath);
                // uloženie obrazka do súboru 
                string imageFilePath = Path.Combine(Server.MapPath("~/img"), "../img/" + bookData.Id + ".jpg");
                System.IO.File.WriteAllBytes(imageFilePath, bookData.ImageBytes);
                Context.Response.Write("kniha úspešne pridaná");
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Error: " + ex.Message);
            }
        }

        [WebMethod(Description = "aktualizuje záznam o jednej knihe na základe id")]
        public void UpdateBook()
        {
            try
            {
                var request = HttpContext.Current.Request;
                // vytvorí sa nová inštancia triedy BookData
                var bookData = new BookData();
                // získanie údajov o knihe z premennej request a ich priradenie do členskych premennych book.Data
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
                bookData.Predajna_cena = decimal.Parse(request["predajna_cena"]);
                bookData.Nakupna_cena = decimal.Parse(request["nakupna_cena"]);
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
                XElement xmlDoc = LoadXElement(xmlFilePath);
                // Nájdenie prvku v XML súbore s id knihy rovnakým ako členskej premmenej bookData.Id 
                var bookElement = xmlDoc.Element("books").Elements("book").FirstOrDefault(x => x.Element("id").Value == bookData.Id);

                if (bookElement == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("kniha zo zadanym id sa nenasla");
                }
                // Aktualizovanie hodnôt elementov prvku bookElement s hodnotami z premennej bookData
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
                bookElement.SetElementValue("predajna_cena", bookData.Predajna_cena);
                bookElement.SetElementValue("nakupna_cena", bookData.Nakupna_cena);
                bookElement.SetElementValue("obsah", bookData.Obsah);
                bookElement.SetElementValue("priemerne_hodnotenie", bookData.Priemerne_hodnotenie);
                bookElement.SetElementValue("marza", (bookData.Predajna_cena - bookData.Nakupna_cena) / bookData.Predajna_cena );
                bookElement.SetElementValue("zisk_kus", bookData.Predajna_cena - bookData.Nakupna_cena);


                // Overí, či boli nahrané dáta o obale knihy a sú neprázdne
                if (bookData.ImageBytes != null && bookData.ImageBytes.Length > 0)
                {// Vytvorí cestu k súboru s obrázkom obalu knihy a uloží dáta o obale knihy na túto cestu
                    string imageFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/img"), "../img/" + bookData.Id + ".jpg");
                    System.IO.File.WriteAllBytes(imageFilePath, bookData.ImageBytes);
                }
                // Uloží zmeny v XML súbore s informáciami o knihách
                xmlDoc.Save(xmlFilePath);
                // Vypíše správu o úspešnom vykonaní aktualizácie knihy
                Context.Response.Write("kniha bola uspesne aktualizovana");
            }
            // V prípade výskytu chyby, nastaví HTTP status code na 500 a vypíše chybovú správu
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Error: " + ex.Message);
            }

        }

        [WebMethod(Description = "zmaže knihu podľa zadaného id ")]
        public void DeleteBook(string id)
        {
            try
            {
                // Cesta k súboru s informáciami o knihách
                string xmlFilePath = fileBookInfo;
                // Načíta XML dokument z daného súboru
                XElement xmlDoc = LoadXElement(xmlFilePath);
                // Nájde knihu s daným ID
                XElement bookToDelete = xmlDoc.Element("books").Elements("book").FirstOrDefault(x => x.Element("id").Value == id);
                if (bookToDelete != null)
                {
                    // Odstráni prvok "book" z XML súboru
                    bookToDelete.Remove();
                    xmlDoc.Save(xmlFilePath);

                    // Vymaže obrázok z filesystému
                    string imageFilePath = Path.Combine(Server.MapPath("~/img"), "../img/" + id + ".jpg");
                    System.IO.File.Delete(imageFilePath);
                    // Vypíše správu o úspešnom vymazaní knihy
                    Context.Response.Write("kniha uspesne zmazana");
                }
                else
                {// Ak sa kniha s daným ID nenašla, vráti chybový kód a príslušnú správu
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("kniha so zadanym id nebola najdena");
                }
            }
            catch (Exception ex)
            {// Ak nastala chyba, vypíše chybovú správu
                Context.Response.Write("Error: " + ex.Message);
            }
        }
        [WebMethod(Description = "odošle údaje o jednej knihe na základe id v json formate")]
        public void SinglebookDataById(string id)
        {
            try
            {
                // Nacitanie dokumentu zo suboru
                XmlDocument doc = LoadXmlDocument(fileBookInfo);
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
                    Context.Response.Write("Nezadali ste ID knihy");
                    return;
                }

                // Vyhladanie jednej knihy podla zadaneho id Xpath ukazka kde je vhodny pre svoju jednoduchosť
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
        [WebMethod(Description = "odošle údaje o jednej knihe na základe názvu v json formate")]
        public void SinglebookDataByName(string name)
        {
            try
            {
                // Načítanie XML dokumentu
                XmlDocument doc = LoadXmlDocument(fileBookInfo);
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
                    Context.Response.Write("Nezadali ste nazov knihy");
                    return;
                }
                // Prevod názvu knihy na malé písmená
                name = name.ToLower();
                // Vyhľadanie záznamu o knihe s daným názvom Xpathu ukazka kde je vhodny pre svoju jednoduchosť
                XmlNodeList nodeListBook = doc.SelectNodes("Bookstore/books/book[translate(nazov,'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ','abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ') = \"" + name + "\"]");
                // Ak sa záznam nenašiel, vyhodí sa výnimka a nastaví sa stavový kód na 500
                if (nodeListBook == null || nodeListBook.Count == 0)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Chyba pri spracovávaní požiadavky";
                    throw new Exception("Záznam pre daný názov nebol nájdený");

                }
                // Zapísanie výsledku do súboru s časovou pečiatkou
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

        [WebMethod(Description = "odošle údaje o jednej knihe na základe isbn v json formáte")]
        public void SinglebookDataByIsbn(string isbn)
        {
            //Nacitanie XML dokumentu z cesty ulozenej v premennej fileBookInfo
            XmlDocument doc = LoadXmlDocument(fileBookInfo);
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
                Context.Response.Write("Nezadali ste isbn");
                return;
            }
            //Vyber vsetkych knih v dokumente Xpathu ukazka kde je vhodny pre svoju jednoduchosť
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
        [WebMethod(Description = "získa zoznam všetkých kníh v json formate")]
        public void GetListAllBooks()

        {   // vytvorenie  a nacitanie XML dokumentu
            XmlDocument doc = LoadXmlDocument(fileBookInfo);
            // získanie zoznamu všetkých kníh pomocou Xpathu ukazka kde je vhodny pre svoju jednoduchosť 
            XmlNodeList AllBook = doc.SelectNodes("Bookstore/books");
            // v prípade, že zoznam je prázdny alebo neexistuje
            if (AllBook == null || AllBook.Item(0) == null)
            {// nastavenie chybového kódu na 500
                Context.Response.StatusCode = 500;
                // popis chyby
                Context.Response.StatusDescription = "Chyba pri spracovávaní dát v dokumente"; 
                return; 
            }
            // nastavenie UTF-8 sady pre http response 
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(JsonConvert.SerializeXmlNode(AllBook.Item(0), Formatting.Indented));
            // serializácia XML uzla ako JSON a odoslanie ako http odpoveď s formátovaním
        }
        [WebMethod(Description = "vrati zoznam so vsetkymi transakciami v json formate ")]
        public void GetListAllTransactions()
        {
            XmlDocument doc = LoadXmlDocument(fileBookTransactionInfo);
            // Výber všetkých transakcií zo zoznamu
            XmlNodeList AllTransactions = doc.SelectNodes("knihy_transakcie");
            // Kontrola, či sú k dispozícii nejaké záznamy
            if (AllTransactions == null || AllTransactions.Item(0) == null)
            {// Nastavenie HTTP statusu na 500 a pridanie chybovej správy
                Context.Response.StatusCode = 500;
                Context.Response.StatusDescription = "Chyba spracovania dokumentu";
                return;
            }
            //// Vybranie prvy uzol ktory obsahuje vsetky udaje o transakiách
            XmlNode transactions = AllTransactions.Item(0);
           
            // Získanie xsi atribútu a jeho odstránenie, ak existuje

            XmlAttribute xsiAttribute = transactions.Attributes["xsi:http://www.w3.org/2001/XMLSchema-instance"];
            if (xsiAttribute != null)
                transactions.Attributes.Remove(xsiAttribute);
            // Zapísanie UTF-8 preambuly do odpovede klientovi
            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            // Prevod transakcií na JSON reťazec a odoslanie odpovede klientovi
            Context.Response.Write(JsonConvert.SerializeXmlNode(transactions, Formatting.Indented));
        }
        [WebMethod(Description = "pridá do xml súboru záznam o novej transakcii")]
        public void AddTransaction(string id_knihy, string datum, string typ_transakcie, string mnozstvo, string cena_za_jednotku, string celkovo_cena, string aktualne_mnozstvo_na_sklade)
        {

            try
            {// Ak nie sú všetky vstupné parametre vyplnené, vráti chybu
                if (string.IsNullOrEmpty(id_knihy) ||
            string.IsNullOrEmpty(datum) ||
            string.IsNullOrEmpty(typ_transakcie) ||
            string.IsNullOrEmpty(mnozstvo) ||
            string.IsNullOrEmpty(cena_za_jednotku) ||
            string.IsNullOrEmpty(celkovo_cena) ||
            string.IsNullOrEmpty(aktualne_mnozstvo_na_sklade))
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.StatusDescription = "Nezadali ste vsetky vstupne parametre";
                    return;
                }
                // Vytvorenie noveho objektu triedy TransactionData
                var transactionData = new TransactionData();
                //inicializácia jeho clennskych premennych
                transactionData.Id_knihy = id_knihy;
                transactionData.Datum = datum;
                transactionData.Typ_transakcie = typ_transakcie;
                transactionData.Mnozstvo = Convert.ToInt32(mnozstvo);
                transactionData.Cena_za_jednotku = Convert.ToDouble(cena_za_jednotku);
                transactionData.Celkovo_cena = Convert.ToDouble(celkovo_cena);
                transactionData.Aktualne_mnozstvo_na_sklade = Convert.ToInt32(aktualne_mnozstvo_na_sklade);

                // Načíta existujúci XML súbor
                string xmlFilePath = fileBookTransactionInfo;
                XDocument xmlDoc = LoadXDocument(xmlFilePath);
                // Nájde posledné ID transakcie a priradí ho k novej transakcii
                var lastTransactionId = xmlDoc.Elements("knihy_transakcie").Elements("transakcia").Max(x => (int?)x.Element("id_transakcie")) ?? 0;
                transactionData.Id_transakcie = (lastTransactionId + 1).ToString();

               // Pridá nový element s detskymi elementami novej  transakcie do XML súboru
                XElement transactionElement = new XElement("transakcia",
                    new XElement("id_transakcie", transactionData.Id_transakcie),
                    new XElement("id_knihy", transactionData.Id_knihy),
                    new XElement("datum", transactionData.Datum),
                    new XElement("typ_transakcie", transactionData.Typ_transakcie),
                    new XElement("mnozstvo", transactionData.Mnozstvo),
                    new XElement("cena_za_jednotku", transactionData.Cena_za_jednotku),
                    new XElement("celkovo_cena", transactionData.Celkovo_cena),
                    new XElement("aktualne_mnozstvo_na_sklade", transactionData.Aktualne_mnozstvo_na_sklade));
                // pridanie a ulozenie elementov s novou transaciov do korenoveho elemetu xml dokumentu
                xmlDoc.Element("knihy_transakcie").Add(transactionElement);
                xmlDoc.Save(xmlFilePath);
                // Vráti správu o úspešnom pridaní transakcie
                Context.Response.Write("transakcia uspesne pridana");

            }
            catch (Exception ex)
            {

                Context.Response.StatusCode = 500;
                Context.Response.Write("Error: " + ex.Message);
                return;
            }
        }

        [WebMethod(Description = "aktualizuje údaje o transakcii")]
        public void UpdateTransaction(string id_transakcie, string id_knihy, string datum, string typ_transakcie, string mnozstvo, string cena_za_jednotku, string celkovo_cena, string aktualne_mnozstvo_na_sklade)
        {
            try
            {
                // Načítanie existujúceho XML súboru
                string xmlFilePath = fileBookTransactionInfo;
                XDocument xmlDoc = LoadXDocument(xmlFilePath);

                // Nájdenie prvku transakcie, ktorý sa má aktualizovať podľa jeho id

                var transactionElement = xmlDoc.Element("knihy_transakcie").Elements("transakcia").Where(x => x.Element("id_transakcie").Value == id_transakcie).FirstOrDefault();
                if (transactionElement == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("kniha zo zadanym id sa nenasla");
                }
                else
                {// Aktualizácia hodnôt elementov transakcie
                    transactionElement.SetElementValue("id_knihy", id_knihy);
                    transactionElement.SetElementValue("datum", datum);
                    transactionElement.SetElementValue("typ_transakcie", typ_transakcie);
                    transactionElement.SetElementValue("mnozstvo", mnozstvo);
                    transactionElement.SetElementValue("cena_za_jednotku", cena_za_jednotku);
                    transactionElement.SetElementValue("celkovo_cena", celkovo_cena);
                    transactionElement.SetElementValue("aktualne_mnozstvo_na_sklade", aktualne_mnozstvo_na_sklade);
                }

                xmlDoc.Save(xmlFilePath);// uloženie XML súboru
                Context.Response.Write("Transakcia bola uspesne aktualizovana");

            }

            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Error: " + ex.Message);
            }
        }



        [WebMethod (Description = "zmaže záznam o transakcii") ]
        public void DeleteTransaction(string id_transakcie)
        {
            try
            {
                // Načítanie existujúceho XML súboru
                string xmlFilePath = fileBookTransactionInfo;
                XDocument xmlDoc = LoadXDocument(xmlFilePath);


                // Nájdenie prvku transakcie, ktorý sa má zmazať podľa jeho id
                var transactionElement = xmlDoc.Element("knihy_transakcie").Elements("transakcia").Where(x => x.Element("id_transakcie").Value == id_transakcie).FirstOrDefault();
                if (transactionElement == null)
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("kniha zo zadanym id sa nenasla");
                }

                else
                {// vymazanie a uloženie xml dokumentu už bez knihy
                    transactionElement.Remove();
                    xmlDoc.Save(xmlFilePath);

                }

            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Error: " + ex.Message);

            }
        }
        [WebMethod (Description = "poskytne zoradené údaje o počtoch kníh na sklade na zaklade atributu v zadanom obdobi ")]
        public void SortedBookAmoutsByDateAndAtribute(string selectedAtribute, string selectedValueAtribute, string startDate, string endDate, string sortField, string sortOrder)
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
            // Definujeme premenné start a end typu DateTime
            DateTime start, end;
            // Overíme, či vstupné dátumy sú v správnom formáte. Ak nie, nastavíme chybový kód a vypíšeme chybovú správu.
            // Kľúčové slovo "out" sa v metóde TryParse() používa na odovzdanie premenných "start" a "end" ako argumentov prostredníctvom referencie,
            // to znamená  že  možno upraviť vnútri metódy a ich aktualizované hodnoty možno vrátiť volajúcemu kódu.
            // Ak konverzia nie je úspešná, metóda vráti false a premenné "start" a "end" zostanú neinicializované.
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }
            // Overíme, či začiatočný dátum nie je neskôr ako koncový dátum. Ak áno, nastavíme chybový kód a vypíšeme chybovú správu
            if (start > end)
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Začiatočný dátum nemože byť neskorej ako konečný dátum");
                return;
            }
            // Načítame XML súbory obsahujúce informácie o knihách a transakciách s knihami
            var booksXml = LoadXElement(fileBookInfo);
            var transactionXml = LoadXElement(fileBookTransactionInfo);
          
            // Použijeme LINQ na spojenie informácií z oboch XML súborov a filtrovanie výsledkov na základe vybranej kategórie a hodnoty
            if (selectedAtribute != "vsetky")// Kontrola, či bola vybraná konkrétna kategória a nie hodnota vsetky
            {
                var result = from b in booksXml.Descendants("book") // Vyberie všetky elementy "book" zo súboru booksXml
                     // Spojí elementy zo súboru transactionXml na základe zhody atribútu "id_knihy" s atribútom "id" v súbore booksXml a vytvorí z toho kolekciu
                             join w1 in transactionXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             // Vyberie prvý element z kolekcie, ktorý má najbližší dátum ku dátumu "start"
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             // Uloží aktuálny počet kusov na sklade z vybraného elementu do premennej startAmount
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")
                             // Vyberie prvý element z kolekcie, ktorý má najbližší dátum ku dátumu "end"
                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)
                              // Filter pre vybranú kategóriu a hodnotu
                             where (string)b.Element(selectedAtribute) == selectedValueAtribute ||
                             (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor1") == selectedValueAtribute) ||
                             (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor2") == selectedValueAtribute)
                             select new //Vytvorí nový anonymný typ so zvolenými atribútmi a hodnotami
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
                if (sortOrder == "ascending")// Zoradenie výsledkov podľa vybranej hodnoty
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
                else if (sortOrder == "descending")// Zoradenie výsledkov zostupne podľa vybranej hodnoty
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
                // Vytvoriť objekt s výsledkami a serializovať ho do JSON formátu
                var anoresult = new { result, };
                var json = JsonConvert.SerializeObject(anoresult, Formatting.Indented);
                // Konvertovať JSON reťazec do XML a uložiť ho do premennej doc
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

                // Serializovať XmlDocument do XML reťazca a uložiť ho pomocou metódy SaveWebMethodResult
                string xmlString = doc.OuterXml;
                SaveWebMethodResult(xmlString, "SortedBookAmoutsByDateAndAtribute", parameters, fileAmountFilterPath);
                // Odošli odpoveď v JSON formáte
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }//ak selectedAtribute=="vsetky" tak sa vykona tato vetva, ktora bo skoro identicku logiku. jediny roziel možeme najst v absencii where v  linq dopyte 
            else
            {

                var result = from b in booksXml.Descendants("book")
                             join w1 in transactionXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
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
                var anoresult = new { result, };
                var json = JsonConvert.SerializeObject(anoresult, Formatting.Indented);
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");
                string xmlString = doc.OuterXml;
                SaveWebMethodResult(xmlString, "SortedBookAmoutsByDateAndAtribute", parameters, fileAmountFilterPath);
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }
        }

        [WebMethod (Description = "vráti agregované údaje počtoch knih na sklade podľa atribútu a zvoleného obdobia")]
        public void AgregatedStatiscticsAmount(string selectedAtribute, string selectedValueAtribute, string startDate, string endDate)
        {
            string[] parameters = { selectedAtribute, selectedValueAtribute, startDate, endDate, };
            if (selectedAtribute != "vsetky")
            {
                // Kontrola, či sú všetky parametre zadané
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
            // Kľúčové slovo "out" sa v metóde TryParse() používa na odovzdanie premenných "start" a "end" ako argumentov prostredníctvom referencie,
             // to znamená  že  možno upraviť vnútri metódy a ich aktualizované hodnoty možno vrátiť volajúcemu kódu.
             // Ak konverzia nie je úspešná, metóda vráti false a premenné "start" a "end" zostanú neinicializované.
           
            DateTime start, end;
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }
            // kontrola či je konečný datum vačši ako začiatočny
            if (start > end)
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Začiatočný dátum nemože byť neskorej ako konečný dátum");
                return;
            }
            // Načítame XML súbory obsahujúce informácie o knihách a transakciách
            var booksXml = LoadXElement(fileBookInfo);
            var transactionsXml = LoadXElement(fileBookTransactionInfo);
     
            if (selectedAtribute != "vsetky")
            {// // Výber dát zo súborov booksXml a transactionsXml a zlúčenie pomocou JOIN
                var result = from b in booksXml.Descendants("book")
                             join w1 in transactionsXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
                             // Výber prvku s minimálnou absolútnou hodnotou rozdielu medzi dátumom a počiatkom intervalu
                             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - start).Ticks)).Take(1)
                             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")
                             // Výber prvku s minimálnou absolútnou hodnotou rozdielu medzi dátumom a koncom intervalu
                             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - end).Ticks)).Take(1)
                                 // Výber prvkov, ktoré spĺňajú podmienku vybranej hodnoty atribútu alebo autora
                             where (string)b.Element(selectedAtribute) == selectedValueAtribute ||
                             (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor1") == selectedValueAtribute) ||
                             (selectedAtribute == "autor" && (string)b.Element("autori").Element("autor2") == selectedValueAtribute)
                             // Vytvorenie nového objektu so zvolenými hodnotami
                             select new
                             {
                                 StartAmount = startAmount,
                                 EndAmount = (int)w2.Element("aktualne_mnozstvo_na_sklade"),
                                 StartDate = start.ToString("yyyy-MM-dd"),
                                 EndDate = end.ToString("yyyy-MM-dd")
                             };
                // Výpočet celkového počtu kníh na začiatku a na konci intervalu
                double TotalStartAmount = result.Sum(x => x.StartAmount);
                double TotalEndAmount = result.Sum(x => x.EndAmount);
                // Výpočet maximálneho a minimálneho počtu kníh na začiatku a na konci intervalu
                double MaxStartAmount = result.Max(x => x.StartAmount);
                double MinStartAmont = result.Min(x => x.StartAmount);
                double MaxEndAmount = result.Max(x => x.EndAmount);
                double MinEndAmont = result.Min(x => x.EndAmount);
                // Výpočet priemeru počtu kníh na začiatku a na konci intervalu
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
                // serializacia do json
                var json = JsonConvert.SerializeObject(serializedResult, Formatting.Indented);
                // serializacaia do xml 
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

               
                string xmlString = doc.OuterXml;
                // uloženie do xml suboru 
                SaveWebMethodResult(xmlString, " AgregatedStatiscticsAmount", parameters, fileAmountFilterPath);
          // odoslaie odpovede klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }
            else
            {

                var result = from b in booksXml.Descendants("book")
                             join w1 in transactionsXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
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

                var json = JsonConvert.SerializeObject(serializedResult, Formatting.Indented);
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

             // serializacia xml do retazca
                string xmlString = doc.OuterXml;
                SaveWebMethodResult(xmlString, " AgregatedStatiscticsAmount", parameters, fileAmountFilterPath);
              // odoslanie json odpovede klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }

        }

        [WebMethod(Description = "vytvorí drill down report pre údaje o počtoch a príjmoch z predaja kníh")]
        public void SortedDrillDownByAtributeDataBetweenTwoDatesSell(string atribute, string startDate, string endDate, string sortingField = "", string sortingOrder = "", string optionalParameter = "")
        {// Vytvorenie poľa parametrov

            string[] parameters = { atribute,startDate, endDate, sortingField,sortingOrder,optionalParameter };
            // Načítanie údajov o knihách zo súboru XML
            XDocument booksData = LoadXDocument(fileBookInfo);
            // Načítanie údajov o transakciách zo súboru XML
            XDocument transactionsData = LoadXDocument(fileBookTransactionInfo);
            DateTime start, end;
            // Kontrola či dátumy sú v správnom formáte
            // Kľúčové slovo "out" sa v metóde TryParse() používa na odovzdanie premenných "start" a "end" ako argumentov prostredníctvom referencie,
            // to znamená  že ich  možno upraviť  vnútri metódy a ich aktualizované hodnoty možno vrátiť volajúcemu kódu.
            // Ak konverzia nie je úspešná, metóda vráti false a premenné "start" a "end" zostanú neinicializované.
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }
            // Kontrola, či boli zadané všetky vstupy
            if (string.IsNullOrEmpty(atribute) || startDate == null || endDate == null || sortingOrder == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Jedna alebo viacero vstupov nebolo vyplnených");
                return;
            }
            // Kontrola či dátum 'start' je pred dátumom 'end'
            if (start > end)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Zaciatočný dátum nesmie byť vačší ako konečný");


                return;
            }
            // Kontrola, či bola zadaná správna hodnota 'sortingOrder'
            if (sortingOrder != "ascending" && sortingOrder != "descending")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Sortovanie može byť iba zostupne alebo vzostupne");

            }
            // Kontrola, či bola zadaná správna hodnota 'sortingField'
            if (sortingField != "hodnota" && sortingField != "nazov")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("zoradovat sa može iba podľa názvu alebo hodnoty");

            }
            // Výpočet počtu dní medzi dátumami 'start' a 'end'
            double days = (end - start).Days + 1;
            // Ak nie je zadaný atribút 'autor' alebo 'autori'
            if (atribute != "autor" && atribute != "autori")
            {

                // Spojenie údajov o knihách a transakciách podľa id knihy pre získanie všetkých informácií o každej transakcii
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")

                                     // Filtrujte transakcie tak, aby  sa zobrazovali len tie s dátumom v zadanom rozmedzí a typom "predaj"
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "predaj"
                                     // Zoskupenie transakcií podľa zvoleného prvku kategórie
                                     group new { Book = book, Transaction = transaction } by book.Element(atribute).Value into g
                                     select new
                                     {
                                         // Uloženie nazov podkategorie ako Podkategoria
                                         Podkategoria = g.Key,


                                         // Vypočet celkového množsstva predaných kníh a celkového príjmu pre každú podkategóriu
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalRevenue = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,
                                         // pre každu pokategoriu aj zoskupenie knih patriacich do tejto podkategorie 
                                         // Zoskupenie kníh podľa ich id, aby sme získali agregované údaje pre knihy s rovnakým id
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,

                                                 // Vypočet celkového množstva predaných kníh a celkový príjem pre každú knihu patriacu do potkategorie

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

                // Zoradenie agregovaných údajov podľa parametrov sortingField a sortingOrder
                if (sortingField == "nazov")
                   
                    {// triedenie podla nazvu vzostupne
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
                        // triedenie podla nazvu zostupne
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
                    }// triedenie podla mnozstva a hodnoty vzostupne alebo zostupne
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
                    }// triedenie podla vynosu vzostupne alebo zostupne
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
                // Najvyšší a najnižší celkový výnos a množstvo pre podkategórii  a pre  knihe

                var maxRevenuePodkategoria = aggregatedData.Max(x => x.TotalRevenue);
                var minRevenuePodkategoria = aggregatedData.Min(x => x.TotalRevenue);
                var maxQuantityPodkategoria = aggregatedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = aggregatedData.Min(x => x.TotalQuantity);

                var maxRevenueBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalRevenue);
                var minRevenueBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalRevenue);
                var maxQuantityBook = aggregatedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = aggregatedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);
                // premennej ktoré obsahujú mená maximálne a minimálne hodnoty pre kategórie a knihy
                var namePodkategoriaMaxRevenue = aggregatedData.Where(x => x.TotalRevenue == maxRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMinRevenue = aggregatedData.Where(x => x.TotalRevenue == minRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = aggregatedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = aggregatedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == maxRevenueBook).First().Name;
                var nameBookMinRevenue = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == minRevenueBook).First().Name;
                var nameBookMaxQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = aggregatedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;


                // Definujeme premennú, ktorá obsahuje celkové súhrnné informácie celkove/celkove priemerne min max pre kategoriu a knihu plus nazvy
                var totalAggregatedData = new
                {// tu su ešte dopočitane ešte celkové a celkové priemerne udaje (prve 4)
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

                // kombinueje objekt aggregateddata a totalaggregateddata do jedneho vysledneho objektu 
                var result = new

                {
                    AggregatedData = aggregatedData,
                    TotalAggregatedData = totalAggregatedData
                };

                
               // serializacia objektu na json 
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                // deserializacia jsonu na xml
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

                // serializacia the XmlDocument na retazec
                string xmlString = doc.OuterXml;
                // zápis xml vysledku do diskového súboru
                SaveWebMethodResult(xmlString, "SortedDrillDownByAtributeDataBetweenTwoDatesSell", parameters, fileAmountFilterPath);
                // odoslanie jsonu v utf8 klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }
            else
            {
                var aggregatedDataAuthor1 = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "predaj"
                                     group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor1").Value into g
                                     select new
                                     {
                                         // ulozi author1 ako Podkategoria
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
                if (!aggregatedDataAuthor1.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                // Group by author2 - toto sluzi len na analyticke uceli to Totalneho poctu predananzch knih a totalneho prijmu sa to nezaratava ale
                // aby sme mali prehlad o prijmi a predajoch knih aj podla autora dva a mohli to vyuzit pri drill down operacii 


                var aggregatedDataAuthor2 = from book in booksData.Descendants("book")
                                      join transaction in transactionsData.Descendants("transakcia")
                                      on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                      where (DateTime)transaction.Element("datum") >= start
                                      && (DateTime)transaction.Element("datum") <= end
                                      && transaction.Element("typ_transakcie").Value == "predaj"
                                      where book.Element("autori").Element("autor2").Value != "-"
                                      group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor2").Value into g
                                      select new
                                      {
                                          // ulozi the author2 ako Podkategoria
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
                var combinedData = aggregatedDataAuthor1.Concat(aggregatedDataAuthor2);

                if (sortingField == "nazov")
                {
                    combinedData = sortingOrder == "ascending"
                    ? combinedData.OrderBy(x => x.Podkategoria)
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
                    : combinedData.OrderByDescending(x => x.Podkategoria)
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
                    combinedData = combinedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
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
                {combinedData = combinedData.OrderBy(x => x.TotalRevenue * (sortingOrder == "ascending" ? 1 : -1))
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
                    
                }// Definujeme premenné, ktoré nesú informácie o maximálnych a minimálnych hodnotách pre výpočty v kategóriách a knihách
                var maxRevenuePodkategoria = combinedData.Max(x => x.TotalRevenue);
                var minRevenuePodkategoria = combinedData.Min(x => x.TotalRevenue);
                var maxQuantityPodkategoria = combinedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = combinedData.Min(x => x.TotalQuantity);

                var maxRevenueBook = combinedData.SelectMany(x => x.Books).Max(x => x.TotalRevenue);
                var minRevenueBook = combinedData.SelectMany(x => x.Books).Min(x => x.TotalRevenue);
                var maxQuantityBook = combinedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = combinedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);
               // Nastavujeme názvy pre premenné, ktoré obsahujú maximálne a minimálne hodnoty pre kategórie a knihy
                var namePodkategoriaMaxRevenue = combinedData.Where(x => x.TotalRevenue == maxRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMinRevenue = combinedData.Where(x => x.TotalRevenue == minRevenuePodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = combinedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = combinedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxRevenue = combinedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == maxRevenueBook).First().Name;
                var nameBookMinRevenue = combinedData.SelectMany(x => x.Books).Where(x => x.TotalRevenue == minRevenueBook).First().Name;
                var nameBookMaxQuantity = combinedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = combinedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;


                // Definujeme premennú, ktorá obsahuje celkové súhrnné informácie
                // poznamka pre total/average quantity totalrevenue berieme len udaje od prveho autora aby nam čisla sedeli(aby nebralo prijem za jeden titul pri oboch autoroch)
                // pri ostatných ako max a min sme muselu skombinovat aj prveho aj druheho autora
                var totalAggregatedData = new
                {// tu su ešte dopočitane ešte celkové a celkové priemerne udaje (prve 4)
                    totalQuantity = aggregatedDataAuthor1.Sum(x => x.TotalQuantity),
                    totalRevenue = aggregatedDataAuthor1.Sum(x => x.TotalRevenue),
                    averageTotalDailyQuantity = aggregatedDataAuthor1.Sum(x => x.TotalQuantity) / days,
                    averageTotalDailyRevenue = aggregatedDataAuthor1.Sum(x => x.TotalRevenue) / days,
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
               
                // kombinueje objekt aggregateddata a totalaggregateddata do jedneho vysledneho objektu 
                var result = new

                {
                    AggregatedData = combinedData,
                    TotalAggregatedData = totalAggregatedData
                };

                // serializacia objektu na json 
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                // deserializacia jsonu na xml
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

                // serializacia the XmlDocument na retazec
                string xmlString = doc.OuterXml;
                // zápis xml vysledku do diskového súboru
                SaveWebMethodResult(xmlString, "SortedDrillDownByAtributeDataBetweenTwoDatesSell", parameters, fileAmountFilterPath);
                // odoslanie jsonu v utf8 klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);

            }
        }

        [WebMethod(Description = "vytvorí drill down report pre údaje o počtoch a nákladoch spojených s nákupom kníh od dodávateľa")]
        public void SortedDrillDownByAtributeDataBetweenTwoDatesCost(string atribute, string startDate, string endDate, string sortingField = "", string sortingOrder = "", string optionalParameter = "")
        {// Vytvorenie poľa parametrov
            string[] parameters = { atribute, startDate, endDate, sortingField, sortingOrder, optionalParameter };
            // Načítanie údajov o knihách zo súboru XML
            XDocument booksData = LoadXDocument(fileBookInfo);
            // Načítanie údajov o transakciách zo súboru XML
            XDocument transactionsData = LoadXDocument(fileBookTransactionInfo);
            DateTime start, end;
            // Kontrola či dátumy sú v správnom formáte
            // Kľúčové slovo "out" sa v metóde TryParse() používa na odovzdanie premenných "start" a "end" ako argumentov prostredníctvom referencie,
            // to znamená  že  možno upraviť vnútri metódy a ich aktualizované hodnoty možno vrátiť volajúcemu kódu.
            // Ak konverzia nie je úspešná, metóda vráti false a premenné "start" a "end" zostanú neinicializované.
            if (!DateTime.TryParse(startDate, out start) || !DateTime.TryParse(endDate, out end))
            {
                Context.Response.StatusCode = 400;
                Context.Response.Write("Zadajte validný formát dútumu ( napr. 'yyyy-MM-dd')");
                return;
            }
            // Kontrola, či boli zadané všetky vstupy
            if (string.IsNullOrEmpty(atribute) || startDate == null || endDate == null || sortingOrder == null)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Jedna alebo viacero vstupov nebolo vyplnených");
                return;
            }
            // Kontrola či dátum 'start' je pred dátumom 'end'
            if (start > end)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Zaciatočný dátum nesmie byť vačší ako konečný");


                return;
            }
            // Kontrola, či bola zadaná správna hodnota 'sortingOrder'
            if (sortingOrder != "ascending" && sortingOrder != "descending")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Sortovanie može byť iba zostupne alebo vzostupne");

            }
            // Kontrola, či bola zadaná správna hodnota 'sortingField'
            if (sortingField != "hodnota" && sortingField != "nazov")
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("zoradovat sa može iba podľa názvu alebo hodnoty");

            }
            // Výpočet počtu dní medzi dátumami 'start' a 'end'
            double days = (end - start).Days + 1;
            if (atribute != "autor" && atribute != "autori")
            {

                // Spojenie údajov o knihách a transakciách podľa id knihy pre získanie všetkých informácií o každej transakcii
                var aggregatedData = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")

                                     // Filtrujte transakcie tak, aby zobrazovali len tie s dátumom v zadanom rozmedzí a typom "nakup"
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "nákup"



                                     // Zoskupite transakcie podľa zvoleného prvku kategórie
                                     group new { Book = book, Transaction = transaction } by book.Element(atribute).Value into g
                                     select new
                                     {
                                         // Uložte hodnotu kategórie ako Podkategoria
                                         Podkategoria = g.Key,


                                         /// Vypočítajte celkové množstvo nakupených kníh a celkové náklady pre každú kategóriu
                                         TotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))),
                                         TotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")),
                                         AverageTotalQuantity = g.Sum(x => Math.Abs((int)x.Transaction.Element("mnozstvo"))) / days,
                                         AverageTotalCost = g.Sum(x => (double)x.Transaction.Element("celkovo_cena")) / days,

                                         // Zoskupite knihy podľa ich id, aby ste získali agregované údaje pre knihy s rovnakým id
                                         Books = g.GroupBy(x => x.Book.Element("id").Value)
                                             .Select(x => new
                                             {
                                                 Id = x.Key,
                                                 Name = x.First().Book.Element("nazov").Value,

                                                 // Vypočítajte celkové množstvo nakupených kníh a celkové náklady pre každú knihu
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

                // Zoradite agregované údaje podľa parametrov sortingField a sortingOrder
                if (sortingField == "nazov")
                {// triedenie podla nazvu vzostupne
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
                    // triedenie podla nazvu zostupne
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
                }// triedenie podla mnozstva a hodnoty vzostupne alebo zostupne
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
                }// triedenie podla nakladov vzostupne alebo zostupne
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
                // Najvyšší a najnižší celkový výnos a množstvo v kategórii a v knihe
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
                // Definujeme premennú, ktorá obsahuje celkové súhrnné informácie
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
                // kombinueje objekt aggregateddata a totalaggregateddata do jedneho
                var result = new

                {
                    AggregatedData = aggregatedData,
                    TotalAggregatedData = totalAggregatedData
                };
                // serializacia objektu na json 
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                // deserializacia jsonu na xml
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

                // serializacia the XmlDocument na retazec
                string xmlString = doc.OuterXml;
                // zápis xml vysledku do diskového súboru
                SaveWebMethodResult(xmlString, "SortedDrillDownByAtributeDataBetweenTwoDatesCost", parameters, fileAmountFilterPath);
                // odoslanie jsonu v utf8 klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }
        

        
            else
            {
                var aggregatedDataAuthor1 = from book in booksData.Descendants("book")
                                     join transaction in transactionsData.Descendants("transakcia")
                                     on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                     where (DateTime)transaction.Element("datum") >= start
                                     && (DateTime)transaction.Element("datum") <= end
                                     && transaction.Element("typ_transakcie").Value == "nákup"
                                     group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor1").Value into g
                                     select new
                                     {
                                         // ulož  author1 ako Podkategoria
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
                if (!aggregatedDataAuthor1.Any())
                {
                    Context.Response.StatusCode = 500;
                    Context.Response.Write("Neboli nájdené žiadne záznamy pre zadané kritériá");
                    return;
                }
                // Group by author2 - toto sluzi len na analyticke uceli to Totalneho poctu predananzch knih a totalneho prijmu sa to nezaratava ale
                // aby sme mali prehlad o prijmi a predajoch knih aj podla autora dva a mohli to vyuzit pri drill down operacii 


                var aggregatedDataAuthor2 = from book in booksData.Descendants("book")
                                      join transaction in transactionsData.Descendants("transakcia")
                                      on (int)book.Element("id") equals (int)transaction.Element("id_knihy")
                                      where (DateTime)transaction.Element("datum") >= start
                                      && (DateTime)transaction.Element("datum") <= end
                                      && transaction.Element("typ_transakcie").Value == "nákup"
                                      where book.Element("autori").Element("autor2").Value != "-"
                                      group new { Book = book, Transaction = transaction } by book.Element("autori").Element("autor2").Value into g
                                      select new
                                      {
                                          // ulož  author2 ako Podkategoria
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
                var combinedData = aggregatedDataAuthor1.Concat(aggregatedDataAuthor2);

                if (sortingField == "nazov")
                {
                    combinedData = sortingOrder == "ascending"
                    ? combinedData.OrderBy(x => x.Podkategoria)
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
                    : combinedData.OrderByDescending(x => x.Podkategoria)
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
                    combinedData = combinedData.OrderBy(x => x.TotalQuantity * (sortingOrder == "ascending" ? 1 : -1))
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
                    combinedData = combinedData.OrderBy(x => x.TotalCost * (sortingOrder == "ascending" ? 1 : -1))
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
                // vypocet max min hodnot pre jednotlive podkategorie a knihy plus vyber ich nazvov
                var maxCostPodkategoria = combinedData.Max(x => x.TotalCost);
                var minCostPodkategoria = combinedData.Min(x => x.TotalCost);
                var maxQuantityPodkategoria = combinedData.Max(x => x.TotalQuantity);
                var minQuantityPodkategoria = combinedData.Min(x => x.TotalQuantity);

                var maxCostBook = combinedData.SelectMany(x => x.Books).Max(x => x.TotalCost);
                var minCostBook = combinedData.SelectMany(x => x.Books).Min(x => x.TotalCost);
                var maxQuantityBook = combinedData.SelectMany(x => x.Books).Max(x => x.TotalQuantity);
                var minQuantityBook = combinedData.SelectMany(x => x.Books).Min(x => x.TotalQuantity);

                var namePodkategoriaMaxCost = combinedData.Where(x => x.TotalCost == maxCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMinCost = combinedData.Where(x => x.TotalCost == minCostPodkategoria).First().Podkategoria;
                var namePodkategoriaMaxQuantity = combinedData.Where(x => x.TotalQuantity == maxQuantityPodkategoria).First().Podkategoria;
                var namePodkategoriaMinQuantity = combinedData.Where(x => x.TotalQuantity == minQuantityPodkategoria).First().Podkategoria;
                var nameBookMaxCost = combinedData.SelectMany(x => x.Books).Where(x => x.TotalCost == maxCostBook).First().Name;
                var nameBookMinCost = combinedData.SelectMany(x => x.Books).Where(x => x.TotalCost == minCostBook).First().Name;
                var nameBookMaxQuantity = combinedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == maxQuantityBook).First().Name;
                var nameBookMinQuantity = combinedData.SelectMany(x => x.Books).Where(x => x.TotalQuantity == minQuantityBook).First().Name;


                // Vypocitaj celkove mnozstvé a celkove naklady pre vsetky knihy 
                //opet do celkových/priemernych nakladov a poctov sme zaratali len autora 1 avšak max min sme robili pre oboch autoro pomocou combineddata
                var totalAggregatedData = new
                {
                    totalQuantity = aggregatedDataAuthor1.Sum(x => x.TotalQuantity),
                    totalCost = aggregatedDataAuthor1.Sum(x => x.TotalCost),
                    averageTotalDailyQuantity = aggregatedDataAuthor1.Sum(x => x.TotalQuantity) / days,
                    averageTotalDailyCost = aggregatedDataAuthor1.Sum(x => x.TotalCost) / days,
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
               
                // kombinácia oboch objektov 
                var result = new

                {
                    AggregatedData = combinedData,
                    TotalAggregatedData = totalAggregatedData
                };
                // serializacia objektu na json 
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                // deserializacia jsonu na xml
                XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");
                // serializacia the XmlDocument na retazec
                string xmlString = doc.OuterXml;
                // zápis xml vysledku do diskového súboru
                SaveWebMethodResult(xmlString, "SortedDrillDownByAtributeDataBetweenTwoDatesCost", parameters, fileAmountFilterPath);
  // odoslanie jsonu v utf8 klientovi
                Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
                Context.Response.Write(json);
            }
        }
    
        [WebMethod(Description = "slúži na výpočet celkového príjmu, nákladov, zisku a iných finančných ukazovateľov")]
        public void CalculateFinancialIndicators(int year, int quarter, int month)
        {
            string[] parameters =   { year.ToString(), quarter.ToString(), month.ToString() };
            // Načítanie xml súboru do objektu XDocument
            XDocument xDoc = LoadXDocument(fileBookTransactionInfo);
            // Vytvorenie zoznamu transakcií na základe xml súboru
            var transactions = from transaction in xDoc.Descendants("transakcia")
                               select new
                               {
                                   Date = DateTime.Parse(transaction.Element("datum").Value),
                                   Type = transaction.Element("typ_transakcie").Value,
                                   Amount = (int)transaction.Element("mnozstvo"),
                                   Price = (double)(transaction.Element("cena_za_jednotku"))
                               };
            // Ak nie je zvolený rok, nastaví sa chybový kód 500 a vráti sa chybová správa
            if (year == 0)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("Prosím zvoľte si rok ");
                return;
            }
            // Ak sú zvolené štvrťrok aj mesiac súčasne, nastaví sa chybový kód 500 a vráti sa chybová správa

            if (quarter != 0 && month != 0)
            {
                Context.Response.StatusCode = 500;
                Context.Response.Write("štvrťrok a mesiac nemôžu byť zvolené súčasne ! ");
                return;
            }
            // Ak je zvolený iba štvrťrok, filtrovania sa vykoná pre transakcie z daného štvrťroku
            else if (quarter != 0)
            {
                transactions = transactions.Where(t => t.Date.Year == year && (t.Date.Month - 1) / 3 + 1 == quarter);
            }
            // Ak je zvolený iba mesiac, filtrovania sa vykoná pre transakcie z daného mesiaca
            else if (month != 0)
            {
                transactions = transactions.Where(t => t.Date.Year == year && t.Date.Month == month);
            }
            // Ak nie je zvolený ani štvrťrok, ani mesiac, filtrovania sa vykoná pre transakcie z daného roku
            else
            {
                transactions = transactions.Where(t => t.Date.Year == year);
            }
            // Výpočet celkového príjmu na základe transakcií so záporným množstvom a typom "predaj"
            double totalRevenue = transactions.Where(t => t.Type == "predaj" && (t.Amount < 0))
                                                       .Sum(t => Math.Abs(t.Amount) * t.Price);
            // Výpočet celkových nákladov na základe transakcií s kladným množstvom a typom "nakup"
            double totalCost = transactions.Where(t => t.Type == "nákup" && t.Amount > 0)
                                                    .Sum(t => t.Amount * t.Price);
            // vypočet zisku
            double profit = totalRevenue - totalCost;
            // celkovy počet objednavok a celkovy počet kusov v objednavkach 
            int numSellOrders = transactions.Count(t => t.Type == "predaj" && (t.Amount < 0));
            int numBuyOrders = transactions.Count(t => t.Type == "nákup" && t.Amount > 0);
            int totalQuantityOfBooksNakup = transactions.Where(t => t.Type == "nákup" && t.Amount > 0)
                                                       .Sum(t => t.Amount);

            int totalQuantityOfBooksPredaj = transactions.Where(t => t.Type == "predaj" && (t.Amount < 0))
                                                                   .Sum(t => Math.Abs(t.Amount));
            // vypočet netprofitmargin a ROI
            double netProfitMargin = profit / totalRevenue;
            double returnOnInvestment = profit / totalCost;
 
            // zapis a odoslanie odpovede podobne ako v predošlých metodach 
            var result = new { totalRevenue, totalCost, profit, numSellOrders, numBuyOrders,totalQuantityOfBooksNakup,totalQuantityOfBooksPredaj,  netProfitMargin, returnOnInvestment, };
            
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Root");

            string xmlString = doc.OuterXml;
            SaveWebMethodResult(xmlString, " CalculateRevenueCostProfit", parameters, fileAmountFilterPath);

            Context.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Context.Response.Write(json);
        }

    }
}










