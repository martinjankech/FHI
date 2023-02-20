using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knihy_jankech
{
    public class BookData
    {
        public string Id { get; set; }
        public string Nazov { get; set; }
        public string Autor1 { get; set; }
        public string Autor2 { get; set; }
        public string Kategoria { get; set; }
        public string Isbn { get; set; }
        public string Jazyk { get; set; }
        public string Pocet_stran { get; set; }
        public string Vazba { get; set; }
        public string Rok_vydania { get; set; }
        public string Vydavatelstvo { get; set; }
        public decimal Predajna_cena { get; set; }
        public decimal Nakupna_cena { get; set; }
        public double Marza { get; set; }
        public double Zisk_kus { get; set; }
       public string Obsah { get; set; }
       public string Priemerne_hodnotenie { get; set; }
       public string ImageName { get; set; }
       public byte[] ImageBytes { get; set; }
}
    public class TransactionData
    {
        public string Id_transakcie { get; set; }
        public string Id_knihy { get; set; }
        public string Datum { get; set; }
        public string Typ_transakcie { get; set; }
        public int Mnozstvo { get; set; }
        public double Cena_za_jednotku { get; set; }
        public double Celkovo_cena { get; set; }
        public int Aktualne_mnozstvo_na_sklade { get; set; }
    }
}