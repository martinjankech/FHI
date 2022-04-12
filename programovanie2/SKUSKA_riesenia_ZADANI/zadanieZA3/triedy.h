#include <iostream>
#include <string>
#include <iomanip>
using namespace std;

class FirmaAutobazar {
private:
	string nazov_firmy;
	string pravna_forma;
	string sidlo_firmy;
	long ICO;
	long long DIC;

public:
	FirmaAutobazar();
	FirmaAutobazar(string Name, string Form, string Place, long ICO1, long long DIC1);	
	string getnazov_firmy() { return nazov_firmy; };
	string getpravna_forma() { return pravna_forma; };
	string getsidlo_firmy() { return sidlo_firmy; };
	long getICO() {return ICO; };
	long long getDIC() { return DIC; };

	void setnazov_firmy(string Name) { nazov_firmy = Name; };
	void setpravna_forma(string Form) { pravna_forma = Form; };
	void setsidlo_firmy(string Place) { sidlo_firmy = Place; };
	void setICO(long ICO1) { ICO = ICO1; };
	void setDIC(long long DIC1) { DIC = DIC1; };


};

class Automobil : public FirmaAutobazar {
private:
	string znacka_typ_auta;
	int rok_vyroby_auta;
	int predaj_cis_auta;
	long cena_auta;

public:
	Automobil();
	Automobil(string Name, string Form, string Place, long ICO1, long long DIC1, string Brand, int Year, int Number, long Price);
	string getznacka_typ_auta() { return znacka_typ_auta; };
	int getrok_vyroby_auta() { return rok_vyroby_auta; };
	int getpredaj_cis_auta() { return predaj_cis_auta; };
	long getcena_auta() { return cena_auta; };

	void setznacka_typ_auta(string Brand) { znacka_typ_auta = Brand; };
	void setrok_vyroby_auta(int Year) { rok_vyroby_auta = Year; };
	void setpredaj_cis_auta(int Number) { predaj_cis_auta = Number; };
	void setcena_auta(long Price) { cena_auta = Price; };
	friend ostream& operator<<(ostream& os, Automobil& t);
};