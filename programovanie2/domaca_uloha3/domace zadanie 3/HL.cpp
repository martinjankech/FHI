#include "triedy.h"

int main()
{
	char m[100], priezv[100], rc[100];
	int pocet, i, cis_prevadz;
	long mz;

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

	//vytvorenie 10-prvkoveho STATICKEHO pola 'objekty_osoby[10]' objektov triedy 'Osoba' 
	Osoba objekty_osoby[10];
	cout << endl << "kolko osob chcete vkladat (mozte vlozit max. 10 osob)? ";
	cin >> pocet;

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

		objekty_osoby[i].ZmenMeno(m);
		objekty_osoby[i].ZmenPriezvisko(priezv);
		objekty_osoby[i].rc.ZmenRC(rc);
		cout << endl << "(data objektu tr. 'Osoba' ulozene v prvku statickeho pola 'objekty_osoby') " <<
			endl;
		cout << objekty_osoby[i] << "---------------------------------------------------------" << endl;
		i++;
	}
	char men[] = "Martin"; char priez[] = "Mato"; char rcs[] = "840609/4578";
	cout << endl << "programom vytvoreny a inicializovany objekt pomocou parametrickeho  konstruktora triedy 'Zamestnanec': " << endl;
		//vytvorenie objektu 'Mato' triedy 'Zamestnanec' pomocou volania parametrickeho konstruktora
	Zamestnanec Mato(men, priez, rcs, 1950, 25);
	cout << Mato;
	 
	Zamestnanec objekty_zam[10];
	cout << "zadajte pocet zamestnancov ktorych chcete nacitat (mozte zadat max 10 zamestnancov)";
	cin >> pocet;
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
			cout << "vlozte mzdu zamestnanca";
			cin >> mz;
			cout << "vlozte cislo prevadzky zamestatnca";
			cin >> cis_prevadz;
		}
		else
		{
			local_i++;
			cout << endl << "vlozte meno " << local_i << ". zamestnanca       : ";
			cin >> m;
			cout << "vlozte priezvisko " << local_i << ". zamestnanca : ";
			cin >> priezv;
			cout << "vlozte rodne cislo " << local_i << ". zamestnanca: ";
			cin >> rc;
			cout << "vlozte mzdu zamestnanca" << local_i << ". zamestnanca: ";
			cin >> mz;
			cout << "vlozte cislo prevadzky zamestatnca" << local_i << ". zamestnanca: ";
			cin >> cis_prevadz;
		}

		objekty_zam[i].ZmenMeno(m);
		objekty_zam[i].ZmenPriezvisko(priezv);
		objekty_zam[i].rc.ZmenRC(rc);
		objekty_zam[i].ZmenMzdu(mz);
		objekty_zam[i].ZmenCis_prevadzky(cis_prevadz);

		cout << endl << "(data objektu tr. 'Zamestnanec' ulozene v prvku statickeho pola 'objekty_osoby') " <<
			endl;
		cout << objekty_zam[i] << "---------------------------------------------------------" << endl;
		i++;
	}

	return 0;
}
