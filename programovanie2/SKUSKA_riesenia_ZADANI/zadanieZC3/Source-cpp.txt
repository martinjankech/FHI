#include <iostream>
using namespace std;

#include "Class.h"

int main()
{
	char firma[30], pravna[10], sidlo[30], znacka[30];
	int  cpu, ram, disk, p_cislo, pocet_4rocnych = 0;
	long int ico, rok, i, pocet, cena, celkova_cena = 0;
	double  priemerna_cena = 0, dic;


	cout << "kolko notebookov chcete vkladat do programu?: ";
	cin >> pocet;

	Notebook *Notebooky = new Notebook[pocet];


	for (i = 0; i < pocet; i++)
	{
		cout << "\n---------------------------------------------------------" << endl;
		cout << "vlozte nazov firmy prevadzk. bazar s " << i + 1 << ". predavanym notebookom: ";
		cin >> firma;
		cout << "vlozte pravnu formu firmy prevadzk. bazar s " << i + 1 << ". predavanym notebookom: ";
		cin >> pravna;
		cout << "Vlozte sidlo firmy prevadzk. bazar s " << i + 1 << ". predavanym  notebookom: ";
		cin >> sidlo;
		cout << "Vlozte ICO firmy prevadzk. bazar s " << i + 1 << ". predavanym  notebookom: ";
		cin >> ico;
		cout << "vlozte znacku a typ " << i + 1 << ". predavaneho notebooku: ";
		cin >> znacka;
		cout << "vlozte rok vyroby " << i + 1 << ". predavaneho notebooku: ";
		cin >> rok;
		cout << "vlozte pocet jadier procesora" << i + 1 << ". predavaneho notebooku: ";
		cin >> cpu;
		cout << "vlozte kapacitu RAM v GB " << i + 1 << ". predavaneho notebooku: ";
		cin >> ram;
		cout << "vlozte kapacitu pevneho disku v MB " << i + 1 << ". predavaneho notebooku: ";
		cin >> disk;
		cout << "vlozte predajne cislo: " << i + 1 << ". predavaneho  notebooku:   ";
		cin >> p_cislo;
		cout << "vlozte predajnu cenu: " << i + 1 << ". predavaneho notebooku [EUR]: ";
		cin >> cena;

		(Notebooky + i)->ZmenNazov(firma);
		(Notebooky + i)->SetPrafor(pravna);
		(Notebooky + i)->SetSidlo(sidlo);
		(Notebooky + i)->SetIco(ico);
		(Notebooky + i)->SetZnackatyp(znacka);
		(Notebooky + i)->SetRokvyroby(rok);
		(Notebooky + i)->SetCpu(cpu);
		(Notebooky + i)->SetRam(ram);
		(Notebooky + i)->SetHdd(disk);
		(Notebooky + i)->SetPredajcislo(p_cislo);
		(Notebooky + i)->SetPredajcena(cena);

	}
	cout << "---------------------------------------------------------" << endl;
	Notebook prvy("Happy_Bazar", "s.r.o", "Trnava", 19865789, "ASUSx-FR7", 2012, 2, 2, 750, 5897421, 130);
	Notebook druhy("ABC_Bazar", "s.r.o", "Trencin", 12365701, "HPx-GS9", 2014, 4, 4, 950, 5298421, 150);
	cout << "\nprogramom vytvorene a inicializovane objekty pomocou parametrickeho konstruktora triedy 'Nootebook':\n";
	cout << "---------------------------------------------------------" << endl;
	cout << prvy;
	cout << endl;
	cout << druhy;
	cout << "\nhodnoty instan. premennych objektov triedy 'Notebook' (notebook-ov), vlozene pouzivatelom programu:\n";
	cout << "---------------------------------------------------------" << endl;
	for (i = 0; i < pocet; i++) {
		cout << Notebooky[i];
		cout << "---------------------------------------------------------" << endl;
		celkova_cena += Notebooky[i].GetPredajcena();
	}
	celkova_cena += prvy.GetPredajcena();
	celkova_cena += druhy.GetPredajcena();

	for (i = 0; i < pocet; i++) {
		if ((2018 - Notebooky[i].GetRokvyroby()) <= 4) {
			pocet_4rocnych++;
			priemerna_cena += Notebooky[i].GetPredajcena();
		}
	}

	if ((2018 - prvy.GetRokvyroby()) <= 4) {
		pocet_4rocnych++;
		priemerna_cena += prvy.GetPredajcena();
	}

	if ((2018 - druhy.GetRokvyroby()) <= 4) {
		pocet_4rocnych++;
		priemerna_cena += druhy.GetPredajcena();
	}

	priemerna_cena = priemerna_cena / (double)(pocet_4rocnych);

	cout << "\n\ncelkova predajna cena uvedenych " << (pocet + 2) << "-och notebookov je [EUR]\t:" << celkova_cena;
	cout << "\npriemerna predajna cena uvedenych " << pocet_4rocnych << " 4-rocnych nootebukov [EUR]\t:" << priemerna_cena << endl;
	cin >> i;
	return 0;
}
