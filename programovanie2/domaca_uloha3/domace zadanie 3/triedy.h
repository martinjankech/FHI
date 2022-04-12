#include<iostream>
using namespace std;

/********************* definicie tried programu ***************************/
class RodneCislo
{
private:
	char rodcis[12];
	int pohlavie;		// 0 - zena, 1 - muz
public:
	RodneCislo() {}
	//explicitny parametricky konstruktor
	RodneCislo(char *r);
	char *VratRC();
	int VratDen();
	int VratMesiac();
	int VratRok();
	int VratPohlavie();
	void ZmenRC(char *r);

	//Operatorova funkcia prepisaneho operatora bitoveho posuvu pre triedu 'RodneCislo' musi byt 
	//deklarovana so specifikatorom pristupu 'friend', aby mohol prepisany operator pristupovat 
	//k sukromnym datovym clenom triedy 'RodneCislo'.
	//Tato operatorova funkcia musi vratit referenciu na prud, ktory dostava ako vstupny parameter,
	//pretoze inak by nefungovalo "retazenie" prepisanych operatorov '<<'
	friend ostream& operator<<(ostream &vyst_prud, RodneCislo r);
};

class Osoba
{
private:
	char meno[20], priezvisko[20];
public:
	//Pretoze vo funkcii 'main' vytvarame staticke pole objektov typu 'Osoba',
	//a k jeho inicializacii sa default-ovo pouzije bezparametricky konstruktor triedy 'Osoba', 
	//ktory musi existovat, tak ho tu musime vytvorit
	//a navazne nan aj bezparametricky konstruktor triedy 'RodneCislo', pretoze jej objekt je 
	//zakomponovany do triedy 'Osoba'
	Osoba() {}
	~Osoba() {} //bezparametricky destruktor
  //vytvorenie objektu 'rc' triedy 'RodneCislo' v triede 'Osoba', cim sa stane objekt triedy 'Osoba' 
  //zlozenym objektom, skladajucim sa aj z objektu 'rc' (kompozicia objektov)
	RodneCislo rc;
	//explicitny parametricky konstruktor
	Osoba(char *, char *, char *);
	char *VratMeno();
	char *VratPriezvisko();
	void ZmenMeno(char *);
	void ZmenPriezvisko(char *);

	//Operatorova funkcia prepisaneho operatora bitoveho posuvu pre triedu 'Osoba' musi byt 
	//deklarovana so specifikatorom pristupu 'friend', aby mohol prepisany operator pristupovat
	//k sukromnym datovym clenom triedy 'Osoba'.
	//Tato operatorova funkcia musi vratit referenciu na prud, ktory dostava ako vstupny parameter,
	//pretoze inak by nefungovalo "retazenie" prepisanych operatorov '<<'
	friend ostream& operator<<(ostream &vyst_prud, Osoba o);
};

class Zamestnanec :public Osoba
{
private:
	long int mzda;
	int cis_prevadzky;
public:
	Zamestnanec() {}
	~Zamestnanec() {}
	RodneCislo rc;
//explicitny parametricky konstruktor
	Zamestnanec(char *, char *, char *, long int, int);
	int VratCis_prevadzky();
	long int VratMzdu();
	void ZmenCis_prevadzky(int);
	void ZmenMzdu(long int);

	//Operatorova funkcia prepisaneho operatora bitoveho posuvu pre triedu 'Zamestnanec' musi byt 
	//deklarovana so specifikatorom pristupu 'friend', aby mohol prepisany operator pristupovat 
	//k sukromnym datovym clenom triedy 'Zamestnanec'.
	//Tato operatorova funkcia musi vratit referenciu na prud, ktory dostava ako vstupny parameter,
	//pretoze inak by nefungovalo "retazenie" prepisanych operatorov '<<'
	friend ostream& operator<<(ostream &vyst_prud, Zamestnanec z);
};
