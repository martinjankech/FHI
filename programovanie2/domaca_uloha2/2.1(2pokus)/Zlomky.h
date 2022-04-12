
#include<iostream>
using namespace std;

//deklaracia neclenskej funkcie
int NSD(int a, int b);

class Zlomky {
private:
	int citatel;
	int menovatel;
	void ZjednodusZlomok(Zlomky &zlomok);
public:
	//explicitny parametricky konstruktor
	Zlomky(int cit = 0, int men = 1)
	{
		citatel = cit;
		if (menovatel == 0)
			menovatel = 1;
		else
			menovatel = men;
	}
	//definicie 3 inline clenskych funkcii (nemaju prology)
	void Citaj_zlomok() { cin >> *this; }
	void Vypis_zlomok() { cout << *this; }
	float Hodnota_zlomku() { return float(citatel) / float(menovatel); }
	void Test_nuly_v_men();

	Zlomky operator+(Zlomky z);
	Zlomky operator-(Zlomky z);//deklaracia clenskej operatorovej funkcie prepisaneho operatora +

	//Operatorove funkcie prepisanych operatorov * a bitoveho posuvu pre triedu 'Zlomky' musia byt 
	//deklarovane so specifikatorm pristupu 'friend', aby mohli prepisane operatory pristupovat 
	//k sukromnym datovym clenom triedy 'Zlomky'.

	//V tejto operatorovej funkcii nemusime pouzit parametre s referenciami, pretoze v tejto funkcii 
	//nemenime instancne data objektov 'zlomok1' a 'zlomok2'
	friend Zlomky operator * (Zlomky zlomok1, Zlomky zlomok2);
	friend Zlomky operator / (Zlomky zlomok1, Zlomky zlomok2);

	//Tieto operatorove neclenske funkcie musia vratit referenciu na prud, ktory dostavaju ako vstupny 
	//parameter, pretoze inak by nefungovalo "retazenie" prepisanych operatorov '<<' a '>>'. 
	friend ostream& operator << (ostream &prud, Zlomky z);
	friend istream& operator >> (istream &prud, Zlomky &z);
};
