#include "triedy.h"

int main()
{
	char m[100], priezv[100], rc[100], nazov_prevadzky[100], pracovne_zaradenie[100];
	int pocet, i;
	int long osobne_cislo, mzda ,cislo_prevadzky;

	cout << "kolko rodnych cisiel chcete skumat? ";
	cin >> pocet;
	i = 0;
	while (i < pocet)
	{
		cout << endl;
		if (pocet == 1)
		{
			cout << "vlozte rodne cislo: ";
			i++;
		}
		else
			cout << "vlozte " << ++i << ". rodne cislo: ";
		cin >> rc;
		RodneCislo r(rc); //vytvorenie obj. 'r' tr. 'RodneCislo' pomocou volania parametrickeho konstruktora

		cout << endl;
		cout << r;
		cout << endl << "pohlavie       : ";
		char pom[5];
		(r.VratPohlavie() == 0) ? strcpy(pom, "zena") : strcpy(pom, "muz");
		cout << pom << endl << "-----------------------------" << endl;
	}

	cout << endl << "---------------------------------------------------------" << endl;

	cout << endl << "kolko osob chcete vkladat? ";
	cin >> pocet;

	//vytvorenie dynamickeho pola s poctom 'pocet' objektov typu 'Osoba', 
	//k jeho inicializacii sa pouzije bezparametricky konstruktor triedy 'Osoba', ktory musi existovat
	Osoba *p_osoba = new Osoba[pocet];

	i = 0;
	cout << "---------------------------------------------------------" << endl;
	while (i < pocet)
	{
		int local_i = i;
		if (pocet == 1)
		{
			cout << "vlozte meno osoby       : ";
			cin >> m;
			cout << "vlozte priezvisko osoby : ";
			cin >> priezv;
			cout << "vlozte rodne cislo osoby: ";
			cin >> rc;
		}
		else
		{
			local_i++;
			cout << endl << "vlozte meno " << local_i << ". osoby       : ";
			cin >> m;
			cout << "vlozte priezvisko " << local_i << ". osoby : ";
			cin >> priezv;
			cout << "vlozte rodne cislo " << local_i << ". osoby: ";
			cin >> rc;
		}

		//pomocou adresovej aritmetiky '(p_osoba+i)' sa dostaneme, napr. k prvku pola 'Osoba[pocet]'  
		//s indexom 1, ak je i==1, cize k objektu s indexom 1 ulozenemu v tomto poli
		(p_osoba + i)->ZmenMeno(m);
		(p_osoba + i)->ZmenPriezvisko(priezv);
		(p_osoba + i)->rc.ZmenRC(rc);
		cout << endl << "(data objektu tr. 'Osoba' ulozene v prvku dynamickeho pola 'Osoba[pocet]', ku ktoremu sme pristupili pomocou ukazovatela na tento prvok) " << endl;
		//dereferencia ukazovatela '(p_osoba+i)'
		cout << *(p_osoba + i) << "---------------------------------------------------------" << endl;
		i++;

	}
	cout << endl << "---------------------------------------------------------" << endl;
	cout << endl << "kolko zamestnancov chcete vkladat? ";
	cin >> pocet;
	Zamestnanec *p_zamestnanec = new Zamestnanec[pocet];

	//vytvorenie dynamickeho pola s poctom 'pocet' objektov typu 'Osoba', 
	//k jeho inicializacii sa pouzije bezparametricky konstruktor triedy 'Osoba', ktory musi existovat


	i = 0;
	cout << "---------------------------------------------------------" << endl;
	while (i < pocet)
	{
		int local_i = i;
		if (pocet == 1)
		{
			cout << "vlozte meno zamestnanca       : ";
			cin >> m;
			cout << "vlozte priezvisko zamestnanca : ";
			cin >> priezv;
			cout << "vlozte rodne cislo zamestanca : ";
			cin >> rc;
			cout << "vlozte Osobne cislo Zamestnanca :";
			cin >> osobne_cislo;
			cout << "vlozte pracovne zaradenie Zamestnanca :";
			cin >> pracovne_zaradenie;
			cout << "vlozte nazov prevadzky Zamastnanca : ";
			cin >> nazov_prevadzky;
			cout << "vlozte cislo prevazky Zamestnanca :";
			cin >> cislo_prevadzky;
			cout << "vlozte mzdu Zamestnanca :";
			cin >> mzda;
		}
		else
		{
			local_i++;
			cout << endl << "vlozte meno " << local_i << ". Zamestnanca        : ";
			cin >> m;
			cout << "vlozte priezvisko " << local_i << ". Zamestnanca    : ";
			cin >> priezv;
			cout << "vlozte rodne cislo " << local_i << ". Zamestnanca   : ";
			cin >> rc;
			cout << "vlozte Osobne cislo  " << local_i << ". Zamestnanca  :";
			cin >> osobne_cislo;
			cout << "vlozte pracovne zaradenie " << local_i << ". Zamestnanca  :";
			cin >> pracovne_zaradenie;
			cout << "vlozte nazov prevadzky " << local_i << ". Zamastnanca :";
			cin >> nazov_prevadzky;
			cout << "vlozte cislo prevazky " << local_i << ". Zamestnanca  :";
			cin >> cislo_prevadzky;
			cout << "vlozte mzdu " << local_i << ". Zamestnanca";
			cin >> mzda;
		}

		//pomocou adresovej aritmetiky '(p_osoba+i)' sa dostaneme, napr. k prvku pola 'Osoba[pocet]'  
		//s indexom 1, ak je i==1, cize k objektu s indexom 1 ulozenemu v tomto poli
		(p_zamestnanec + i)->ZmenMeno(m);
		(p_zamestnanec + i)->ZmenPriezvisko(priezv);
		(p_zamestnanec + i)->rc.ZmenRC(rc);
		(p_zamestnanec + i)->ZmenOsobnecislo(osobne_cislo);
		(p_zamestnanec + i)->ZmenPracZaradenie(pracovne_zaradenie);
		(p_zamestnanec + i)->ZmenNazovPrevadzky(nazov_prevadzky);
		(p_zamestnanec + i)->ZmenCis_prevadzky(cislo_prevadzky);
		(p_zamestnanec + i)->ZmenMzdu(mzda);
		 cout << endl << "(data objektu tr. 'Osoba' ulozene v prvku dynamickeho pola 'Osoba[pocet]', ku ktoremu sme pristupili pomocou ukazovatela na tento prvok) " << endl;
		//dereferencia ukazovatela '(p_osoba+i)'
		cout << *(p_zamestnanec + i) << "---------------------------------------------------------" << endl;
		i++;
	}

	//zmazanie 1-rozmerneho dynamickeho pola, na ktore ukazuje ukazovatel 'p_osoba', z pamate
	delete[] p_osoba;
	char men[] = "Martin"; char p[] = "Mato"; char rcs[] = "840609/4578"; char pozicia[] = "lakyrnik"; char pracov[] = "Lakovna";
	cout << endl << "programom vytvoreny a inicializovany objekt pomocou parametrickeho konstruktora triedy 'Zamestnanec': " << endl;
	//vytvorenie objektu 'Mato' triedy 'Zamestnanec' pomocou volania parametrickeho konstruktora
	Zamestnanec Mato(men, p, rcs, 7000256, pozicia, pracov, 25, 1950);
	cout << Mato;

	return 0;
}
