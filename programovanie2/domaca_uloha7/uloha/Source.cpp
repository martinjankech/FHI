#include<fstream>
#include <string.h>
#include<iostream>
using namespace std;


struct TOsoba
{
	long datum;
	char meno[16], priezvisko[16], adresa[31];
};


int PorovnajOsoby(TOsoba *o1, TOsoba *o2)
{
	int porovnanie = strcmp(o1->priezvisko, o2->priezvisko);
	if (porovnanie != 0)
		return porovnanie;
	porovnanie = strcmp(o1->meno, o2->meno);
	if (porovnanie != 0)
		return porovnanie;
	if (o1->datum < o2->datum)
		return -1;
	if (o1->datum > o2->datum)
		return 1;

	return 0;
}

void InsertionSort(TOsoba **pole, int pocet)
{
	for (int i = 0; i < pocet - 1; i++)
	{
		int j = i + 1;
		TOsoba *tmp = pole[j];
		int porovnanie;
		porovnanie = PorovnajOsoby(tmp, pole[j - 1]);
		while (j > 0 && porovnanie > 0)
		{
			pole[j] = pole[j - 1];
			j--;
		}
		pole[j] = tmp;
	}

}

int main()
{
	TOsoba **osoby = new TOsoba*[1000];
	int i = 0, pocet;
	ifstream in;

	in.open("pacienti.txt");
	if (!in)
	{
		cout << "Subor sa nepodarilo otvorit";
		return 0;
	}

	while (!in.eof())
	{
		osoby[i] = new TOsoba;
		in >> osoby[i]->datum >> osoby[i]->meno >> osoby[i]->priezvisko;
		in.getline(osoby[i]->adresa, 30);
		i++;
	}

	pocet = i - 1;
	cout << "v zozname " << (2 <= pocet && pocet <= 4 ? "su " : "je ");
	cout << pocet << " vysetrenia nasledovnych pacientov";
	cout << ":\n";

	InsertionSort(osoby, pocet);

	for (i = pocet - 1; i >= 0; i--)
	{
		cout << osoby[i]->datum << ' ' << osoby[i]->meno << ' '
			<< osoby[i]->priezvisko << osoby[i]->adresa << endl;
		delete osoby[i];
	}
	delete osoby[pocet];
	delete[] osoby;

	return 0;
}
