#include <iostream>
#include <fstream>
#include <string>
using namespace std;

struct TStudent {
	string name;
	string surname;
	string znamka;
	int rodnecislo;
};

int rok(int rc) {
	int r = (rc / 10000);
	if (r < 12) return r + 2000;
	else return r + 1900;
}

int PorovnajStudentov(TStudent *s1, TStudent *s2) {
	if (s1->surname < s2->surname) return 1;
	if (s1->surname > s2->surname) return 0;
	if (s1->name < s2->name) return 1;
	if (s1->name > s2->name) return 0;
	int r1 = rok(s1->rodnecislo);
	int r2 = rok(s2->rodnecislo);
	if (r1 < r2) return 1;
	if (r1 > r2) return 0;
	return 0;
}

void InsertionSort(TStudent **pole, int pocet) {
	int i, j;
	TStudent* tmp;
	for (i = 1; i < pocet; i++) {
		for (j = i, tmp = pole[i]; (j > 0) && PorovnajStudentov(tmp, pole[j - 1]); j--)
			pole[j] = pole[j - 1];
		pole[j] = tmp;
	}
}

int JeZena(int c) {
	return (c % 10000 > 5000);
}

void VypisDatum(int c) {
	cout << c % 100 << ". " << (c / 100) % 50 << ". " << rok(c);
}

void vypis_studenta(TStudent *s) {
	cout << s->surname << " " << s->name << " ";
	cout << "(" << s->znamka << ")" << " ";
	cout << "rod. c. " << (s->rodnecislo >= 100000 ? "" : "0") << s->rodnecislo << " ";
	if (JeZena(s->rodnecislo))
		cout << "zena, narodena" << " ";
	else
		cout << "muz, narodeny" << " ";
	VypisDatum(s->rodnecislo);
	cout << endl;
}

void vypis_studentov(TStudent **st, int poc) {
	for (int i = 0; i < poc; i++)
		vypis_studenta(st[i]);
}

int main() 
{
	TStudent** studenti = new TStudent*[100];
	string n, s, z;
	int r;
	int count = 0;

	ifstream in;
	in.open("studenti.txt");
	if (!in)
	{
		cout << "Subor sa nepodarilo otvorit";
		return 0;
	}
	
	
	for (; in >> n >> s >> z >> r; count++) {
		studenti[count] = new TStudent();
		studenti[count]->name = n;
		studenti[count]->surname = s;
		studenti[count]->znamka = z;
		studenti[count]->rodnecislo = r;
	}
	cout << " neusporiadany zoznam " << count << " studentov zo suboru 'studenti.txt'" << endl;
	vypis_studentov(studenti, count);
	InsertionSort(studenti, count);
	cout << endl;
	cout << "USPORIADANY zozn. " << count << " studentov zo sub 'studenti.txt' funkciou 'InsertionSort'" << endl;
	vypis_studentov(studenti, count);
	cout << endl;
	cout << "----------------------------------------------" << endl;
	cout << "vlozte rok narodenia hladanych studentov:        ";
	int hladani;
	cin >> hladani;
	if (hladani >= 1912 && hladani <= 2011) {
		cout << "vlozte znamku, ktoru maju mat hladani studenti:  ";
		string znamka;
		cin >> znamka;
		cout << endl;
		TStudent** najdeni = new TStudent*[count];
		int pocetNajdenych = 0;
		for (int i = 0; i < count; i++) {
			if (rok(studenti[i]->rodnecislo) == hladani && studenti[i]->znamka == znamka) {
				najdeni[pocetNajdenych++] = studenti[i];
			}
		}
		cout << "pocet a zoznam studentov narodenych v roku " << hladani << " so znamkou " << znamka << ": " << pocetNajdenych << endl;
		vypis_studentov(najdeni, pocetNajdenych);
		delete[] najdeni;
	}
	for (int i = 0; i < count; i++)
		delete studenti[i];
	delete[] studenti;
}