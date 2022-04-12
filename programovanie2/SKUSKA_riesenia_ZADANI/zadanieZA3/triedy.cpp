#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

#include "Triedy.h"

FirmaAutobazar::FirmaAutobazar() {
}

FirmaAutobazar::FirmaAutobazar(string Name, string Form, string Place, long ICO1, long long DIC1) {
	nazov_firmy = Name;
	pravna_forma = Form;
	sidlo_firmy = Place;
	ICO = ICO1;
	DIC = DIC1;
}

Automobil::Automobil() :	FirmaAutobazar() {
}

Automobil::Automobil(string Name, string Form, string Place, long ICO1, long long DIC1, string Brand, int Year, int Number, long Price) : 
	FirmaAutobazar(Name, Form, Place, ICO1, DIC1) {
	znacka_typ_auta = Brand;
	rok_vyroby_auta = Year;
	predaj_cis_auta = Number;
	cena_auta = Price;
}

ostream& operator<<(ostream& os, Automobil& t) {
	os << "nazov firmy prevadzk. autobazar s predavanym autom      : " << t.getnazov_firmy() << endl;
	os << "pravna forma fy. prevadzk. autobazar s predavanym autom : " << t.getpravna_forma() << endl;
	os << "sidlo firmy prevadzk. autobazar s predavanym autom      : " << t.getsidlo_firmy() << endl;
	os << "ICO firmy prevadzk. autobazar s predavanym autom        : " << t.getICO() << endl;
	os << "znacka a typ auta                                       : " << t.getznacka_typ_auta() << endl;
	os << "rok vyroby auta                                         : " << t.getrok_vyroby_auta() << endl;
	os << "predajne cislo auta                                     : " << t.getpredaj_cis_auta() << endl;
	os << "predajna cena auta [Eur]                                : " << t.getcena_auta() << endl;
	return os;
}