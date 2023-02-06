using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;


namespace xml_net_framework_skusam
{
    internal class Program
    {
        static void Main(string[] args)
        { string kategorie = "category";
            DateTime datumZac = DateTime.Parse("2020-01-08").Date;
            DateTime datumKon = DateTime.Parse("2020-01-12").Date;


            Console.WriteLine(GetAggregatedData(kategorie, datumZac, datumKon));
          
            //            XmlDocument doc = new XmlDocument();
            //            doc.Load("D:\\git_repositories\\FHI\\diplomovka\\moja_praca\\rychlokurzCsharpAskusanie\\xml_net_framework_skusam\\xml_net_framework_skusam\\books.xml");
            //            string json = JsonConvert.SerializeXmlNode(doc, Formatting.Indented) ;
            //            var details = JObject.Parse(json);

            //            Console.WriteLine(json);
            //            Console.WriteLine(details);

            //            XmlNodeList nodeList = doc.SelectNodes("bookstore/book");
            //            int countRootNodes = nodeList.Count;
            //            // Console.WriteLine(nodeList.Item(0).ChildNodes.Count);
            //            int j = 1;
            //            foreach (XmlNode node in nodeList)

            //            {
            //                Console.WriteLine("kniha cislo :  " + j);
            //                j++;
            //                int countChild = node.ChildNodes.Count;
            //                for (int i = 0; i < countChild; i++) {

            //                    Console.WriteLine(node.ChildNodes.Item(i).Name + ": " + node.ChildNodes.Item(i).InnerText);


            //                }

            //            }
            string bookTransactionPath = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_transakcie_moje.xml";
            string bookInfoPath = "D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_moje.xml ";
            XElement root = XElement.Load(bookInfoPath);
            XElement rootOrders = XElement.Load(bookTransactionPath);
            //// vsetky data knihy kategoria fantasy
            // IEnumerable<XElement> name =
            //     from el in root.Descendants("book")
            //     where el.Element("kategoria").Value == "Fantasy"
            //     let kategoria = el.Element("kategoria")
            //     let nazov = el.Element("kategoria")
            //     orderby (string)nazov
            //     select el;

            // foreach (XElement el in name)
            //     Console.WriteLine(el.Element("nazov").Value);

            //  vsetky transakcie
            //IEnumerable<XElement> orders =
            //    from elor in rootOrders.Descendants("transakcia")
            //    select elor;

            //foreach (XElement el in orders)
            //    Console.WriteLine(el.Element("id_knihy").Value); ;


            //funkcne
            //var books = root.Descendants("book").Where(m => (string)m.Element("kategoria") == "Fantasy");
            //foreach (XElement el in books)
            //    Console.WriteLine(el.Element("id"));

            //DateTime datumZac = DateTime.Parse("2020-01-08").Date;
            //DateTime datumKon = DateTime.Parse("2020-01-09").Date;
            ////Console.WriteLine(datumKon);
            //// query syntax 
            //var transaktionById =
            //     from el in rootOrders.Descendants("transakcia")
            //     join st in root.Descendants("book")
            //    on (int)el.Element("id_knihy") equals (int)st.Element("id")
            //     // where (int ) el.Element("id_knihy")==1 || (int)el.Element("id_knihy")==2
            //     where DateTime.Parse(el.Element("datum").Value) >= datumZac && DateTime.Parse(el.Element("datum").Value) <= datumKon
            //     select new
            //     {
            //         nazov = st.Element("nazov"),
            //         mnoztvo_sklad = el.Element("aktualne_mnozstvo_na_sklade")
            //     };

            //foreach (var item in transaktionById) { Console.WriteLine(item.nazov+":"+item.mnoztvo_sklad); }
            // skusime method syntax 
            //var actualWarehousebalance =
            //rootOrders.Descendants("transakcia").Join(root.Descendants("book").Where(str=> str.Element("id").Value =="5"), str1 => (int)str1.Element("id_knihy"),
            //str2 => (int)str2.Element("id"), (str1, str2) => new
            //{
            //    id = str2.Element("id"),
            //    nazov = str2.Element("nazov"),
            //    celkovo_cena = str1.Element("celkovo_cena")
            //});

            //foreach (var item in actualWarehousebalance)
            //{ Console.WriteLine(item.id + " " + item.nazov + "" + item.celkovo_cena + " eur "); }

            //DateTime datumZac = DateTime.Parse("2020-01-08").Date;
            //DateTime datumKon = DateTime.Parse("2020-01-12").Date;

            //   var actualWarehousebalance =
            //from el in rootOrders.Descendants("transakcia")
            //where DateTime.Parse(el.Element("datum").Value) >= datumZac && DateTime.Parse(el.Element("datum").Value) <= datumKon
            //group el by (int)el.Element("id_knihy") into pg
            //join st in root.Descendants("book")
            //on (int)pg.FirstOrDefault().Element("id_knihy") equals (int)st.Element("id")

            //select  new { transakcie=pg,
            //nazov=st.Element("nazov").Value};

            //foreach (var item in actualWarehousebalance)
            //{ Console.WriteLine(item.Key); 
            //    foreach ( var transaction in item) 
            //    { Console.Write(transaction.Element("datum")); } }

            //foreach (var data1 in actualWarehousebalance) { 
            //    Console.WriteLine(data1.nazov);
            //    foreach (var transaction1 in data1.transakcie) { Console.WriteLine(transaction1.Element("datum")); } }

            // var groupbytransaction =
            //from el in rootOrders.Descendants("transakcia")
            //group el by (int)el.Element("id_knihy");

            // foreach (var data in groupbytransaction)
            // {
            //     Console.WriteLine(data.Key);
            //     foreach (var transaction in data) { Console.WriteLine(transaction.Element("mnozstvo")); }
            // }


            //tu zacina funkcna metoda na zac a konecne mnoztvo podla datumu treba to spojit este s kategoriov teda dat tu len idcka knih podla vybranej kategorie
            //    var resultStartDate = rootOrders.Descendants("transakcia")
            //       // zac datum ako hranica vyberie aj skorsie pre pripad zeby v dany den neprebehla transakcia pre dane id 
            //       .Where(t => DateTime.Parse(t.Element("datum").Value) <= datumZac)
            //       .GroupBy(t => new { t.Element("id_knihy").Value })
            //       .Select(x => new
            //       {

            //           // zisti poslednu transakciu(max v datumzac ale pripadne aj skorej) na zaklade idcka transakcie-da sa lebo idcka transakcii sa zvysuju podla poradia transakcii a tie vznikaju v case
            //           Firsttransaction = x.OrderByDescending(t => t.Element("id_transakcie").Value).FirstOrDefault()


            //       }).Select(x => new
            //       {
            //           id = x.Firsttransaction.Element("id_knihy"),
            //           datum = x.Firsttransaction.Element("datum"),
            //           mnoztvo = x.Firsttransaction.Element("aktualne_mnozstvo_na_sklade")
            //       });
            //    Console.WriteLine("zaciatocne  pocty");
            //    foreach (var data in resultStartDate)
            //    { Console.WriteLine(data.id + " - " + data.datum + " - " + data.mnoztvo); }

            //    var resultEndDate = rootOrders.Descendants("transakcia")
            //        .Where(t => DateTime.Parse(t.Element("datum").Value) <= datumKon)
            //        .GroupBy(t => new { t.Element("id_knihy").Value })
            //        .Select(x => new
            //        {

            //            // zisti poslednu transakciu na zaklade idcka transakcie-da sa lebo idcka transakcii sa zvysuju podla poradia transakcii a tie vznikaju v case
            //            Lasttransaction = x.OrderByDescending(t => t.Element("id_transakcie").Value).FirstOrDefault()


            //        }).Select(x => new
            //        {
            //            id = x.Lasttransaction.Element("id_knihy"),
            //            datum = x.Lasttransaction.Element("datum"),
            //            mnoztvo = x.Lasttransaction.Element("aktualne_mnozstvo_na_sklade")
            //        });
            //    Console.WriteLine("konecne  pocty");
            //    foreach (var data in resultEndDate)
            //    { Console.WriteLine(data.id + " - " + data.datum + " - " + data.mnoztvo); }



            //    var StartAndEndJoin = resultStartDate.Join(resultEndDate,
            //       startId => startId.id.Value,
            //       endId => endId.id.Value,
            //       (startId, endId) => new
            //       {
            //           id = startId.id,
            //           mnoztvoStart = startId.mnoztvo,
            //           mnoztvoEnd = endId.mnoztvo,
            //       });
            //    foreach (var data in StartAndEndJoin)
            //    { Console.WriteLine(data.id.Value + " - " + data.mnoztvoStart.Value + " - " + data.mnoztvoEnd.Value); }

            //}

            var booksXml = XElement.Load(bookInfoPath);
            var warehouseXml = XElement.Load(bookTransactionPath);
            Console.WriteLine(booksXml.Element("books").Element("book").Element("autori").Element("autor1").Value);



            //var selectedCategory = "Fantasy";
            //DateTime startDate =  DateTime.Parse("2020-01-08").Date;
            //DateTime endDate =  DateTime.Parse("2020-01-09").Date;

            ////DateTime datumKon = DateTime.Parse("2020-01-12").Date

            //var result = from b in booksXml.Descendants("book")
            //             join w1 in warehouseXml.Descendants("transakcia") on (string)b.Element("id") equals (string)w1.Element("id_knihy") into g
            //             from w1 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - startDate).Ticks)).Take(1)
            //             let startAmount = (int)w1.Element("aktualne_mnozstvo_na_sklade")
            //             from w2 in g.OrderBy(x => Math.Abs((DateTime.Parse((string)x.Element("datum")) - endDate).Ticks)).Take(1)
            //             where (string)b.Element("kategoria") == selectedCategory
            //             select new
            //             {
            //                 BookId = (string)b.Element("id"),
            //                 BookName = (string)b.Element("nazov"),
            //                 StartAmount = startAmount,
            //                 EndAmount = (int)w2.Element("aktualne_mnozstvo_na_sklade"),
            //                 StartDate = (DateTime)w1.Element("datum"),
            //                 EndDate = (DateTime)w2.Element("datum")
            //             };

            //foreach (var r in result)
            //{
            //    Console.WriteLine("Book ID: {0}, Book Name: {1}, Start Amount: {2}, End Amount: {3}, Start Date: {4}, End Date: {5}", r.BookId, r.BookName, r.StartAmount, r.EndAmount, r.StartDate, r.EndDate);
            //}
            
           string  GetAggregatedData(string topHierarchy, DateTime startDate, DateTime endDate)
            {
                XElement booksdata = XElement.Load("D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_transakcie_moje.xml");
                XElement transactionsXml = XElement.Load("D:\\git_repozitare\\FHI\\diplomovka\\moja_praca\\webova_sluzba\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\elektronicke_knihkupectvo_webove_sluzby_diplomovka\\xml\\book_moje.xml");
                var books = from book in booksdata.Elements("book")
                            select new
                            {
                                Id = (int)book.Element("id"),
                                Category = (string)book.Element("kategoria"),
                                Language = (string)book.Element("jazyk"),
                                Year = (int)book.Element("rok_vydania"),
                                Binding = (string)book.Element("vazba"),
                                Publisher = (string)book.Element("vydavatelstvo"),
                                SalesPrice = (decimal)book.Element("predajna_cena")
                            };

                var transactions = from transaction in transactionsXml.Elements("transakcia")
                                   where ((DateTime)transaction.Element("datum") >= startDate) && ((DateTime)transaction.Element("datum") <= endDate)
                                   join book in books on (int)transaction.Element("id_knihy") equals book.Id
                                   select new
                                   {
                                       Id = (int)transaction.Element("id_transakcie"),
                                       BookId = (int)transaction.Element("id_knihy"),
                                       Date = (DateTime)transaction.Element("datum"),
                                       Type = (string)transaction.Element("typ_transakcie"),
                                       Quantity = (int)transaction.Element("mnozstvo"),
                                       UnitPrice = (decimal)transaction.Element("cena_za_jednotku"),
                                       TotalPrice = (decimal)transaction.Element("celkovo_cena"),
                                       Category = book.Category,
                                       Language = book.Language,
                                       Year = book.Year,
                                       Binding = book.Binding,
                                       Publisher = book.Publisher,
                                       SalesPrice = book.SalesPrice
                                   };

                XElement result = new XElement("tophierarchy", new XAttribute("element", topHierarchy));
                switch (topHierarchy)
                {
                    case "category":
                        var categoryData = from t in transactions
                                           group t by t.Category into g
                                           select new
                                           {
                                               Category = g.Key,
                                               TotalSellAmount = g.Sum(x => x.Quantity),
                                               TotalRevenue = g.Sum(x => x.TotalPrice)
                                           };
                        foreach (var data in categoryData)
                        {
                            XElement category = new XElement("category", new XAttribute("name", data.Category),
                                new XElement("totalsellamount", data.TotalSellAmount),
                                new XElement("totalrevenue", data.TotalRevenue));
                            var bookData = from t in transactions
                                           where t.Category == data.Category
                                           group t by t.BookId into g
                                           select new
                                           {
                                               BookId = g.Key,
                                               TotalSellAmount = g.Sum(x => x.Quantity),
                                               TotalRevenue = g.Sum(x => x.TotalPrice)
                                           };
                            XElement books1 = new XElement("books");
                            foreach (var book in bookData)
                            {
                                XElement bk = new XElement("book", new XAttribute("id", book.BookId),
                                new XElement("totalsellamount", book.TotalSellAmount),
                                new XElement("totalrevenue", book.TotalRevenue));
                                books1.Add(bk);
                            }
                            category.Add(books);
                            result.Add(category);
                        }
                        break;
                    default: break;
                }
                return result.ToString();
            }
        }
    }
}
        
