#include<iostream>
using namespace std;

#include "class.h"
#include "string"

Ustav::Ustav(char *naz, char *sid, long ico)
{
	Ustav::SetNazov(naz);
	Ustav::SetSidlo(sid);
	Ustav::SetIco(ico);
}
void Ustav::SetNazov(char *nam)
{
	strcpy_s(Ustav::nazovUstavu, nam);
}
void Ustav::SetSidlo(char *pra)
{
	strcpy_s(Ustav::sidloUstavu, pra);
}
void Ustav::SetIco(long ico)
{
	Ustav::ICO = ico;
}
char *Ustav::GetNazov()
{
	return nazovUstavu;
}
char *Ustav::GetSidlo()
{
	return sidloUstavu;
}
long Ustav::GetIco()
{
	return ICO;
}

Klient::Klient(char *naz, char *sid, long ico, char *men, char *prie, char *rc, char *diag)
{
	Klient::SetNazov(naz);
	Klient::SetSidlo(sid);
	Klient::SetIco(ico);
	Klient::SetMeno(men);
	Klient::SetPriezvisko(prie);
	Klient::SetRodcis(rc);
	Klient::SetDiagnoza(diag);
}
void Klient::SetMeno(char *men)
{
	strcpy_s(Klient::meno, men);
}
void Klient::SetPriezvisko(char *prie)
{
	strcpy_s(Klient::priezvisko, prie);
}
void Klient::SetRodcis(char *rc)
{
	strcpy_s(Klient::rodcis, rc);
}
void Klient::SetDiagnoza(char *diag)
{
	strcpy_s(Klient::diagnoza, diag);
}
char *Klient::GetMeno()
{
	return meno;
}
char *Klient::GetPriezvisko()
{
	return priezvisko;
}
char *Klient::GetRodcis()
{
	return rodcis;
}
char *Klient::GetDiagnoza()
{
	return diagnoza;
}
int Klient::VratRokNarod() {
	int rc = stoi(rodcis) / 10000;
	if (rc<17) return (rc + 2000);
	else return (rc + 1900);

}
int Klient::najdiKlienta(Klient *pole, char *diagn, int index)
{
	if (strcmp((pole + index)->GetDiagnoza(), diagn)) return 0;
	else return 1;
}

ostream& operator<<(ostream &vyst_prud, Klient a)
{
	vyst_prud << endl << "nazov ustavu, v ktorom sa nachadza klient\t: " << a.GetNazov() << endl
		<< "sidlo ustavu, v ktorom sa nachadya klient\t: " << a.GetSidlo() << endl
		<< "meno klienta\t\t: " << a.GetMeno() << endl
		<< "priezvisko klienta\t: " << a.GetPriezvisko() << endl
		<< "rodne cislo klienta\t: " << a.GetRodcis() << endl
		<< "diagnoza klienta\t: " << a.GetDiagnoza() << endl;
	return vyst_prud;
}