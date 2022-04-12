#include "triedy.h"

Zoo::Zoo(char *Miesto, char *Druh, int Cislo, int Cena)
{
		strcpy(Zoo::Miesto , Miesto);
		strcpy(Zoo::Druh, Druh);
		Zoo::Cislo=Cislo;
		Zoo::Cena=Cena;
}

void Zoo::SetMiesto(char *Place)
{
	strcpy(Miesto,Place);
}

char* Zoo::GetMiesto()
{
	char *pom;
	pom=new char[20];
	strcpy(pom, Miesto);
	return pom;
}

void Zoo::SetDruh(char *DruhZviera)
{
	strcpy(Druh, DruhZviera);
}
char* Zoo::GetDruh()
{
	char *pom;
	pom=new char[20];
	strcpy(pom, Druh);
	return pom;
}

void Zoo::SetCislo(int Number)
{
	Cislo=Number;
}
int Zoo::GetCislo()
{
	return Cislo;
}

void Zoo::SetCena(int Price)
{
	Cena=Price;
}
int Zoo::GetCena()
{
	return Cena;
}

