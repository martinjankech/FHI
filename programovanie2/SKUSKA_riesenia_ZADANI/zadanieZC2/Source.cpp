#include <fstream>
#include <string.h>
#include <iostream>
#include <algorithm>
using namespace std;

struct TStudent {
	int rodnecislo;
	char meno[16], priezvisko[16], mesto[31];
	int body, vzdialenost;
};

int JeZena(int c);
int Najdi_studenta(TStudent *s, char pohl, int body_ub, char *mesto) {
	char pohlavie, mesto_uppercase[31];
	strcpy_s(mesto_uppercase, mesto);
	mesto_uppercase[0] = toupper(mesto_uppercase[0]); //male zac. pismeno na velke
	//urcenie pohlavia
	if (JeZena(s->rodnecislo))
		pohl = 'z';
	else
		pohl = 'm';
	if (pohl == pohl && (s->body <= body_ub) && !strcmp((s->mesto), mesto_uppercase))
		return 1;
	return 0;

}

//funckie vracajuce rok,mesiac,den z rodnehocisl.
int rok(int rc)
{
	int r = (rc / 10000);
	if (r < 17) return r + 2000;
	else return r + 1900;
}
int mesiac(int rc)
{
	int m = ((rc % 10000) / 100);
	if (m > 12) return m - 50;
	else return m;

}

int den(int rc)
{
	return rc % 100;
}
int PorovnajStudentov(TStudent *s1, TStudent *s2) {
	if (s1->body < s2->body)
		return 1;
	if (s1->body > s2->body)
		return 0;
	if (s1->vzdialenost < s2->vzdialenost)
		return 1;
	if (s1->vzdialenost > s2->vzdialenost)
		return 0;
	//porovnanie veku podla roku,mesiaca a dna
	int r1 = rok(s1->rodnecislo), r2 = rok(s2->rodnecislo);
	if (r1 < r2)
		return 1;
	if (r1 > r2)
		return 0;
	int m1 = mesiac(s1->rodnecislo), m2 = mesiac(s2->rodnecislo);
	if (m1 < m2)
		return 1;
	if (m1 > m2)
		return 0;
	int d1 = den(s1->rodnecislo), d2 = den(s2->rodnecislo);
	if (d1 < d2) return 1;
	if (d1 > d2) return 0;
	return 0;
}
void BubbleSort(TStudent **data, int pocet) //vyuzitim funck.Porovnaj studentov,usporiadame studentv.
{
	int i, j;
	for (i = 1; i < pocet; i++)
	for (j = pocet - 1; j >= i; j--)
	if (PorovnajStudentov(data[j - 1], data[j]))
		swap(data[j], data[j - 1]);
}

void VypisDatum(int c)
{
	cout << c % 100 << ". " << (c / 100) % 50 << ". " << rok(c);
} // vypise mesiac bez + 50tky.

int JeZena(int c)
{
	return (c % 10000 > 5000);//ak plati je to zena,inak muz
}



void vypis_studenta(TStudent *s)
{
	cout << s->priezvisko << " " << s->meno << "\t";
	cout << "(" << s->body << ")" << " "; cout << s->rodnecislo << ", ";

	if (JeZena(s->rodnecislo))
		cout << "z" << ", ";
	else
		cout << "m" << ", ";
	VypisDatum(s->rodnecislo);
	cout << "\t" << s->mesto << "\t";
	cout << "(" << s->vzdialenost << ")" << " ";
	cout << endl;
}


int main()
{
	TStudent **osoby = new TStudent*[100];
	int i = 0, pocet;
	ifstream in;

	//otvorenie sub.
	in.open("studenti.txt");
	if (!in)
	{
		cout << "Subor sa nepodarilo otvorit";
		return 0;
	}
	while (!in.eof())
	{
		osoby[i] = new TStudent;
		in >> osoby[i]->meno >> osoby[i]->priezvisko >> osoby[i]->rodnecislo >> osoby[i]->body;
		in >> osoby[i]->mesto >> osoby[i]->vzdialenost;
		i++;
	}
	pocet = i;
	cout << "  neusporiadany zoznam  " << pocet << " studentov zo suboru 'studenti.txt'" << endl;
	for (i = 0; i < pocet; i++)
		vypis_studenta(osoby[i]);

	cout << "\n  USPORIADANY zozn. " << pocet << " studentov zo sub 'studenti.txt' funkciou 'BubbleSort'" << endl;

	BubbleSort(osoby, pocet);
	for (i = 0; i<pocet; i++)
		vypis_studenta(osoby[i]);
	cout << endl;
	char pohlzad;
	int bodyzad;
	char mestozad[30];
	cout << "---------------------------------------------------------" << endl;
	cout << "vlozte pohlavie hladanych studentov (zean(z)/muz(m))\t\t: ";
	cin >> pohlzad;
	cout << "vlozte maximalny pocet bodov na ubytovanie hladanych studentov\t: ";
	cin >> bodyzad;
	cout << "mesto bydliska hladanych studentov\t\t:";
	cin >> mestozad;
	cout << endl;
	int najdenych = 0;
	for (i = 0; i < pocet; i++)
	{
		if (Najdi_studenta(osoby[i], pohlzad, bodyzad, mestozad) == 1)
		{
			najdenych++;
			vypis_studenta(osoby[i]);
		}
	}
	cout << endl;
	if (najdenych == 0)
		cout << "pozadovany student sa nenasiel." << endl;
	else {
		cout << "pocet najdenych studentov s pohlavim '" << pohlzad << "', maximalnymi bodmi za ubytovanie '" << bodyzad << "'" << endl;
		cout << "a s bydliskom '" << mestozad << "': " << najdenych << endl;
	}
	cin >> i;
	return 0;
}