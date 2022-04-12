#include<fstream>
#include<iostream>
using namespace std;

struct datum
{
	int den, mesiac, rok;
};

struct student
{
	char meno[20], priezvisko[20];
	char znamka_MAT_I; // 'A', 'B', 'C', 'D', 'E', 'F'
	datum datum_narodenia;
};

int hladaj(student *S, int dlzka, int(*fun)(student, char*), char *co);
int hladaj_meno(student st, char *hladane);
int hladaj_priezvisko(student st, char *hladane);
int hladaj_znamku(student st, char *hladane);
void VypisStudentovSoZnamkou(student S[100], int pocet, char *zn);



int hladaj(student *S, int dlzka, int(*fun)(student, char*), char *co)
{
	int i = 0;
	while (i < dlzka && !fun(S[i], co))
		i++;
	if (i < dlzka) return i;
	else return -1;
}

void vypis_student(student S, int index)
{
	char mesiace[12][10] = { "januar", "februar", "marec", "april", "maj", "jun", "jul", "august", "september",
											 "oktober", "november", "december" };
	cout << "(" << index << ")";
	cout << S.meno << " " << S.priezvisko << ", nar. ";
	cout << S.datum_narodenia.den << ". ";
	cout << mesiace[S.datum_narodenia.mesiac - 1] << " ";
	cout << S.datum_narodenia.rok << " znamka:" << S.znamka_MAT_I << endl;
}

int hladaj_meno(student st, char *hladane)
{
	if (strcmp(st.meno, hladane) == 0)
		return 1;
	else
		return 0;
}

int hladaj_priezvisko(student st, char *hladane)
{
	return strcmp(st.priezvisko, hladane) == 0 ? 1 : 0;
}

int hladaj_znamku(student st, char *hladane)
{
	return st.znamka_MAT_I == (char)hladane[0] ? 1 : 0;
}

float PriemernaZnamka(student S[100], int pocet)
{ // A:5b, B:4b, C:3b, D:2b, E:1b, F:0b
	int sum = 0;
	for (int i = 0; i < pocet; i++)
	{
		switch (S[i].znamka_MAT_I)
		{
		case 'A': sum += 5; break;
		case 'B': sum += 4; break;
		case 'C': sum += 3; break;
		case 'D': sum += 2; break;
		case 'E': sum += 1; break;
		}
	}
	return (float)sum / pocet;
}

void VypisNadpriemernych(student S[100], int pocet, float pz)
{
	int znamka;
	char mesiace[12][10] = { "januar", "februar", "marec", "april", "maj", "jun", "jul", "august", "september",
											 "oktober", "november", "december" };
	for (int i = 0; i < pocet; i++)
	{
		switch (S[i].znamka_MAT_I)
		{
		case 'A': znamka = 5; break;
		case 'B': znamka = 4; break;
		case 'C': znamka = 3; break;
		case 'D': znamka = 2; break;
		case 'E': znamka = 1; break;
		case 'F': znamka = 0; break;
		}
		if (znamka > pz)
		{
			if (strlen(S[i].meno) + strlen(S[i].priezvisko) > 14)
				cout << S[i].meno << " " << S[i].priezvisko << ",\tnar. ";
			else
				cout << S[i].meno << " " << S[i].priezvisko << ",\t\tnar. ";
			cout << S[i].datum_narodenia.den << ". ";
			cout << mesiace[S[i].datum_narodenia.mesiac - 1] << " ";
			cout << S[i].datum_narodenia.rok << " \t znamka:" << S[i].znamka_MAT_I << endl;
		}
	}
}
void VypisStudentovSoZnamkou(student S[100], int pocet, char *zn)
{
	int i = 0;
	while (i < pocet)
	{
		if (zn[0] == (S[i].znamka_MAT_I))
		{
			vypis_student(S[i], i);
			cout << endl;
		}
		i++;
	}
}

int main()
{
	char znamka[10], meno[20];
	ifstream in;
	int i = 0; float pz;
	in.open("studenti.txt");
	if (!in)
		cout << "Subor sa nepodarilo otvorit";
	else
	{
		student s[100];


		while (!in.eof())
		{
			in >> s[i].meno >> s[i].priezvisko >> s[i].znamka_MAT_I;
			in >> s[i].datum_narodenia.den >> s[i].datum_narodenia.mesiac;
			in >> s[i].datum_narodenia.rok;
			i++;
		}
		pz = PriemernaZnamka(s, i);
		cout << "zoznam studentov s nadpriemernou znamkou z predmetu Matematika I:\n";
		VypisNadpriemernych(s, i, pz);

		cout << "vlozte znamku, ktoru maju mat hladani studenti: ";
		cin >> znamka;
		cout << endl << "zoznam studentov zo znamkov " << znamka << "  z predmetu matematika1 : " << endl;
		VypisStudentovSoZnamkou(s, i, znamka);

		int hladany;
		cout << "\nvlozte znamku, ktoru ma mat hladany student: ";
		cin >> znamka;
		hladany = hladaj(s, i, hladaj_znamku, znamka);

		if (hladany != -1)
		{
			cout << "hladany student sa nasiel: ";
			vypis_student(s[hladany], hladany);
		}
		else
			cout << "Student so znamkou '" << znamka << "' sa NEnasiel.\n";

		cout << "\nvlozte meno, ktore ma mat hladany student: ";
		cin >> meno;
		hladany = hladaj(s, i, hladaj_meno, meno);

		if (hladany != -1)
		{
			cout << "hladany student sa nasiel: ";
			vypis_student(s[hladany], hladany);
		}
		else
			cout << "Student s meno '" << meno << "' sa NEnasiel. ";
	}
	return 0;
}
