#include<iostream>
using namespace std;

#include "Class.h"

FirmaBazar::FirmaBazar(char *nam, char *pra, char *sid, long int ic)
{
	FirmaBazar::ZmenNazov(nam);
	FirmaBazar::SetPrafor(pra);
	FirmaBazar::SetSidlo(sid);
	FirmaBazar::SetIco(ic);
}

Notebook::Notebook(char *nam, char *pra, char *sid, long int ic, char *zntyp, int rok, int cpu, int ram, int hdd, int pcislo, long int pcena)
{
	Notebook::ZmenNazov(nam);
	Notebook::SetPrafor(pra);
	Notebook::SetSidlo(sid);
	Notebook::SetIco(ic);
	Notebook::SetZnackatyp(zntyp);
	Notebook::SetRokvyroby(rok);
	Notebook::SetCpu(cpu);
	Notebook::SetRam(ram);
	Notebook::SetHdd(hdd);
	Notebook::SetPredajcislo(pcislo);
	Notebook::SetPredajcena(pcena);
}

void FirmaBazar::ZmenNazov(char *nam)
{
	strcpy_s(FirmaBazar::nazov_firmy, nam);
}
void FirmaBazar::SetPrafor(char *pra)
{
	strcpy_s(FirmaBazar::pravna_forma, pra);
}
void FirmaBazar::SetSidlo(char *sid)
{
	strcpy_s(FirmaBazar::sidlo_firmy, sid);
}
void FirmaBazar::SetIco(long int ic)
{
	FirmaBazar::ICO = ic;
}
void Notebook::SetZnackatyp(char *zntyp)
{
	strcpy_s(Notebook::znacka_ntbk, zntyp);
}
void Notebook::SetRokvyroby(int rok)
{
	Notebook::rok_vyroby_ntbk = rok;
}
void Notebook::SetCpu(int cpu)
{
	Notebook::pocetjadierCPUntbk = cpu;
}
void Notebook::SetRam(int ram)
{
	Notebook::kapacitaRAMntbk = ram;
}
void Notebook::SetHdd(int hdd)
{
	Notebook::kapacitaHDDntbk = hdd;
}
void Notebook::SetPredajcislo(int pcislo)
{
	Notebook::predaj_cis_ntbk = pcislo;
}
void Notebook::SetPredajcena(long int pcena)
{
	Notebook::cena_ntbk = pcena;
}

char *FirmaBazar::GetFirma()
{
	return nazov_firmy;
}
char *FirmaBazar::GetPrafor()
{
	return pravna_forma;
}
char *FirmaBazar::GetSidlo()
{
	return sidlo_firmy;
}
long int FirmaBazar::GetIco()
{
	return ICO;
}
char *Notebook::GetZnackatyp()
{
	return znacka_ntbk;
}
int Notebook::GetRokvyroby()
{
	return rok_vyroby_ntbk;
}
int Notebook::GetCpu()
{
	return pocetjadierCPUntbk;
}
int Notebook::GetRam()
{
	return kapacitaRAMntbk;
}
int Notebook::GetHdd()
{
	return kapacitaHDDntbk;
}
int Notebook::GetPredajcislo()
{
	return predaj_cis_ntbk;
}
long int Notebook::GetPredajcena()
{
	return cena_ntbk;
}
ostream& operator<<(ostream &vyst_prud, Notebook a)
{
	vyst_prud << "nazov firmy prevadzk. bazar s predavanym notebookom\t: " << a.GetFirma() << endl
		<< "sidlo firmy prevadzk. bazar s predavanym notebookom\t: " << a.GetSidlo() << endl
		<< "znacka a typ notebooku\t: " << a.GetZnackatyp() << endl
		<< "rok vyroby notebooku\t: " << a.GetRokvyroby() << endl
		<< "pocet jadier notebooku\t: " << a.GetCpu() << endl
		<< "kapacita RAM notebooku [GB]\t: " << a.GetRam() << endl
		<< "kapacitapevneho disku notebooku [MB]\t: " << a.GetHdd() << endl
		<< "predajne cislo\t: " << a.GetPredajcislo() << endl
		<< "predajna cena [Eur]\t: " << a.GetPredajcena() << endl;
	return vyst_prud;
}
