#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

#include "Triedy.h"

int main() {
	cout << "kolko automobilov chcete vkladat do programu? ";
	int pocet;
	cin >> pocet;
	cout << "-----------------------------------------------" << endl;
	Automobil *pole = new Automobil[pocet];	
	Automobil stat_obj = Automobil("Happy_Autobazar", "s.r.o.", "Trnava", 19865789, 2586578521, "Ford_Mondeo", 2010, 589742, 5230);
	string Name;
	string Form;
	string Place;
	long ICO1;
	long long DIC1;
	string Brand;
	int Year;
	int Number; 
	long Price;
	
	for (int i = 0; i < pocet; i++) 
	{		
		cout << "vlozte nazov firmy prevadzk. autobazar s " << i + 1 << ". predavanym autom      : ";
		cin >> Name;
		cout << "vlozte pravnu formu fy. prevadzk. autobazar s " << i + 1 << ". predavanym autom : ";		
		cin >> Form;
		cout << "vlozte sidlo firmy prevadzk. autobazar s " << i + 1 << ". predavanym autom      : ";
		cin >> Place;
		cout << "vlozte ICO firmy prevadzk. autobazar s " << i + 1 << ". predavanym autom        : ";
		cin >> ICO1;
		cout << "vlozte DIC firmy prevadzk. autobazar s " << i + 1 << ". predavanym autom        : ";
		cin >> DIC1;
		cout << "vlozte znacku a typ " << i + 1 << ". auta                                       : ";
		cin >> Brand;
		cout << "vlozte rok vyroby " << i + 1 << ". auta                                         : ";
		cin >> Year;
		cout << "vlozte predajne cislo " << i + 1 << ". auta                                     : "; 
		cin >> Number;
		cout << "vlozte predajnu cenu " << i + 1 << ". auta [Eur]                                : ";
		cin >> Price;
		cout << "----------------------------------------------" << endl;		

		(pole+i)->setnazov_firmy(Name);
		(pole+i)->setpravna_forma(Form);
		(pole+i)->setsidlo_firmy(Place);
		(pole+i)->setICO(ICO1);
		(pole+i)->setDIC(DIC1);
		(pole+i)->setznacka_typ_auta(Brand);
		(pole+i)->setrok_vyroby_auta(Year);
		(pole+i)->setpredaj_cis_auta(Number);
		(pole+i)->setcena_auta(Price);
	}

	cout << "programom vytvoreny a inicializovany objekt pomocou parametrickeho konstruktora triedy 'Automobil'" << endl;
	cout << "----------------------------------------------" << endl;
	cout << stat_obj << endl;
	cout << "hodnoty instan. premennych objektov triedy 'Automobil' (aut). vlozene pouzivatelom programu" << endl;
	for (int i = 0; i < pocet; i++) {
		cout << "----------------------------------------------" << endl;
		cout << *(pole+i);
	}
	cout << "----------------------------------------------" << endl;	
	long long celkova_cena = stat_obj.getcena_auta();
	for (int i = 0; i < pocet; i++)
		celkova_cena += (pole+i)->getcena_auta();
	float priemerna_cena = ((float)celkova_cena) / (pocet + 1);
	cout << "celkova predajna cena uvedenych " << pocet + 1 << "-och aut [Eur]                   : " << celkova_cena << endl;
	cout << "priemerna predajna cena uvedenych " << pocet + 1 << "-och aut [Eur]                 : " << std::setprecision(1) << std::fixed << priemerna_cena << endl;
	
	delete[] pole;

	return 0;
}
