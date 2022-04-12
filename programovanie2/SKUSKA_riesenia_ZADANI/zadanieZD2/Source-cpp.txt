#include <iostream>
#include <fstream>
#include <string>
#include <algorithm>
using namespace std;
struct TStudent {
	string meno;
	string priezvisko;
	int rodnecislo;
	int body;
	string bydlisko;
	int vzdialenost;
};
int rok(int rc) {
	int r = (rc / 10000);
	if (r < 17) return r + 2000;
	else return r + 1900;
}
int den(int rc){
	return rc % 100;
}
int mesiac(int rc) {
	int m = ((rc % 10000) / 100);
	if (m > 12) return m - 50;
	else return m;
}
int PorovnajStudentov(TStudent *s1, TStudent *s2) {
	if (s1->body < s2->body) return 1;
	if (s1->body > s2->body) return 0;
	if (s1->vzdialenost < s2->vzdialenost) return 1;
	if (s1->vzdialenost > s2->vzdialenost) return 0;
	int r1 = rok(s1->rodnecislo);
	int r2 = rok(s2->rodnecislo);
	if (r1 < r2) return 1;
	if (r1 > r2) return 0;
	int m1 = mesiac(s1->rodnecislo);
	int m2 = mesiac(s2->rodnecislo);
	if (m1 < m2) return 1;
	if (m1 > m2) return 0;
	int d1 = den(s1->rodnecislo);
	int d2 = den(s2->rodnecislo);
	if (d1 < d2) return 1;
	if (d1 > d2) return 0;
	return 0;

}
void ShellSort(TStudent **A, int pocet) {
	int m = pocet;
	while (m /= 2)
	for (int d = pocet - m, i = 0; i < d; i++)
	for (int j = i; (j >= 0) && (PorovnajStudentov(A[j], A[j + m])); j -= m)
		swap(A[j], A[j + m]);
}
int JeZena(int c) {
	return (c % 10000 > 5000);
}
void VypisDatum(int c) {
	cout << c % 100 << ". " << (c / 100) % 50 << ". " << rok(c);
}

void vypis_studenta(TStudent *s) {
	cout << s->priezvisko << " " << s->meno << "\t";
	cout << "(" << s->body << ")" << " ";
	cout << s->rodnecislo << ", ";
	if (JeZena(s->rodnecislo))
		cout << "z" << ", ";
	else
		cout << "m" << ", ";
	VypisDatum(s->rodnecislo);
	cout << "\t" << s->bydlisko << "\t";
	cout << "(" << s->vzdialenost << ")" << " ";
	cout << endl;
}
void vypis_studentov(TStudent **st, int poc) {
	for (int i = 0; i < poc; i++)
		vypis_studenta(st[i]);
}
int main() {
	TStudent** studenti = new TStudent*[100];
	string m, p, byd;
	int r, body, vzd;
	int count = 0;
	ifstream in;
	in.open("studenti.txt");
	if (!in)
	{
		cout << "Subor sa nepodarilo otvorit";
		return 0;
	}
	for (; in >> m >> p >> r >> body >> byd >> vzd; count++) {
		studenti[count] = new TStudent();
		studenti[count]->meno = m;
		studenti[count]->priezvisko = p;
		studenti[count]->rodnecislo = r;
		studenti[count]->body = body;
		studenti[count]->bydlisko = byd;
		studenti[count]->vzdialenost = vzd;
	}
	cout << " neusporiadany zoznam " << count << " studentov zo suboru 'studenti.txt'" << endl;
	vypis_studentov(studenti, count);
	ShellSort(studenti, count);
	cout << endl;
	cout << "USPORIADANY zozn. " << count << " studentov zo sub 'studenti.txt' funkciou 'ShellSort'" << endl;
	vypis_studentov(studenti, count);
	cout << endl;
	cout << "----------------------------------------------" << endl;
	cout << "vlozte pohlavie hladanych studentov (zean(z)/muz(m))\t\t\t:";
	char pohlavie;

	cin >> pohlavie;
	if (pohlavie == 'z' || pohlavie == 'm') {
		cout << "vlozte maximalny pocet bodov na ubytovanie hladanych studentov\t\t:";
		int maxb, minv;
		cin >> maxb;
		cout << "vlozte minimalnu vzdialenost bydliska od skoly hladanych studentov\t:";
		cin >> minv;
		cout << endl;
		TStudent** najdeni = new TStudent*[count];
		int pocetNajdenych = 0;
		for (int i = 0; i < count; i++) {
			if ((studenti[i]->body <= maxb) && (studenti[i]->vzdialenost >= minv)) {
				if (pohlavie == 'z' && JeZena(studenti[i]->rodnecislo))
					najdeni[pocetNajdenych++] = studenti[i];
				if (pohlavie == 'm' && !JeZena(studenti[i]->rodnecislo))
					najdeni[pocetNajdenych++] = studenti[i];
			}
		}
		vypis_studentov(najdeni, pocetNajdenych);
		cout << endl << "pocet najdenych studentov s pohlavim '" << pohlavie << "', maximalnymi bodmi za ubytovanie '" << maxb << "'";
		cout << endl << "a s minimalnou vzdialenostou bydliska od skoly '" << minv << "': " << pocetNajdenych << endl;
		delete[] najdeni;
	}
	for (int i = 0; i < count; i++)
		delete studenti[i];
	delete[] studenti;
}