#include "triedy.h"

Auto::Auto(char *Miesto, char *Znacka, int Rok, int Cislo, int Cena)
{
	strcpy(Auto::Miesto, Miesto);
	strcpy(Auto::Znacka, Znacka);
	Auto::Rok=Rok;
	Auto::Cislo=Cislo;
	Auto::Cena=Cena;
}

void Auto::SetMiesto(char *Place)
{
	strcpy(Miesto,Place);
}
char *Auto::GetMiesto()
{
	return Miesto;
}

void Auto::SetZnacka(char *Brand)
{
	strcpy(Znacka,Brand);
}
char *Auto::GetZnacka()
{
	return Znacka;
}

void Auto::SetRok(int Year)
{
	Rok=Year;
}
int Auto::GetRok()
{
	return Rok;
}

void Auto::SetCislo(int Number)
{
	Cislo=Number;
}
int Auto::GetCislo()
{
	return Cislo;
}

void Auto::SetCena(int Price)
{
	Cena=Price;
}
int Auto::GetCena()
{
	return Cena;
}