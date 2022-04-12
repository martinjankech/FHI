#include<fstream>
#include<string.h>
#include<iostream>
using namespace std;
//-----------------datove struktury-------------------//
struct datum
{
	int d, m, y;
};
struct TStudent
{
	char meno[32], priezvisko[32];
	
	datum datum_narodenia;
	char znamka_M;
};
struct TPrvok
{
	TStudent student;
	TPrvok *dalsi;
};
struct TZoznam
{
	TPrvok *prvy, *posledny;
};
//-------------------funkcie-----------------------------//
int JePrazdny(TZoznam z)
{
	return z.prvy == NULL;
}
void VlozNaKoniec(TZoznam &z, TStudent sx)
{
	TPrvok *novy = new TPrvok;
	novy->student = sx;
	novy->dalsi = NULL;
	if (z.prvy == NULL) // prazdny zoznam
		z.prvy = z.posledny = novy;
	else
		z.posledny->dalsi = novy;
	z.posledny = novy;
}
void VypisPrvok(TPrvok *px)
{
	cout << px->student.priezvisko << " " << px->student.meno << " \t(" << px->student.znamka_M << ") ";
	cout << px->student.datum_narodenia.d << ". " << px->student.datum_narodenia.m << ". ";
	cout << px->student.datum_narodenia.y << endl;
}
void Vypis(TZoznam z)
{
	if (JePrazdny(z)) return;
	TPrvok *p = z.prvy;
	while (p->dalsi != NULL)
	{
		VypisPrvok(p);
		p = p->dalsi;
	}
	VypisPrvok(p);
}
void ZmazPosledny(TZoznam &z)
{
	if (z.prvy == NULL) // prazdny zoznam
		return;
	TPrvok *p = z.prvy;
	if (p->dalsi == NULL) // je len jeden prvok
	{
		delete p;
		z.prvy = z.posledny = NULL;
			return;
	}
	// najdeme predposledny prvok
	while (p->dalsi->dalsi != NULL)
		p = p->dalsi;
	delete p->dalsi;
	p->dalsi = NULL;
	z.posledny = p;
}
void Zmaz(TZoznam &z)
{
	while (!JePrazdny(z))
		ZmazPosledny(z);
}
//tu moze prijat funkcia 'Nacitaj' aj objekt 'cin',
//pretoze ten je tiez objektom triedy 'istream'
void Nacitaj(TStudent &sx, istream &vstup)
{
	vstup >> sx.meno >> sx.priezvisko >> sx.znamka_M;
	vstup >> sx.datum_narodenia.d >> sx.datum_narodenia.m >> sx.datum_narodenia.y;
}
int main()
{
	TZoznam zoznam;
	zoznam.prvy = zoznam.posledny = NULL;
	TStudent s;
	int n = 0;
	ifstream vstup;
	vstup.open("prvaci.txt");
	if (!vstup)
		cout << "Nepodarilo sa otvorit vstupny subor";
	Nacitaj(s, vstup);
	VlozNaKoniec(zoznam, s);
	while (!vstup.eof())
	{
		Nacitaj(s, vstup );
			VlozNaKoniec(zoznam, s);
		n++;
		
	}
	ZmazPosledny(zoznam);
	vstup.close();
	cout << "--------vypis zoznamu--------------" << endl;
	cout << "Pocet studentov: " << n << endl << endl;
	Vypis(zoznam);
	Zmaz(zoznam);
	return 0;
}