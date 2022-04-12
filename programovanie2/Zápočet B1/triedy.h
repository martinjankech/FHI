#include <iostream>
using namespace std;

class Auto
{
private:
	char Miesto[50];
	char Znacka[50];
	int Rok;
	int Cislo;
	int Cena;
public:
	Auto (){}
	Auto (char *Miesto, char *Znacka, int Rok, int Cislo, int Cena);

		char *GetMiesto();
		char *GetZnacka();
		int GetRok();
		int GetCislo();
		int GetCena();

		void SetMiesto (char *Place);
		void SetZnacka (char *Brand);
		void SetRok(int Year);
		void SetCislo(int Number);
		void SetCena(int Price);
};