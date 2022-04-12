#pragma once
#include<iostream>
using namespace std;

class FirmaBazar {
private:
	char nazov_firmy[30],pravna_forma[10],sidlo_firmy[30];
	long int ICO;
public:
	FirmaBazar() {};
	FirmaBazar(char *nam, char *pra, char *sid, long int ic);
	

	void ZmenNazov(char *nam);
	void SetPrafor(char *pra);
	void SetSidlo(char *sid);
	void SetIco(long int ic);

	char* GetFirma();
	char* GetPrafor();
	char* GetSidlo();
	long int GetIco();
};

class Notebook :public FirmaBazar{
private:

	char znacka_ntbk[30];
	int rok_vyroby_ntbk;
	int pocetjadierCPUntbk;
	int kapacitaRAMntbk;
	int kapacitaHDDntbk;
	int predaj_cis_ntbk;
	long int cena_ntbk;
public:
	Notebook() {}
	Notebook(char *nam, char *pra, char *sid, long int ic, char *zntyp, int rok, int cpu, int ram, int hdd, int pcislo, long int pcena);

	void SetZnackatyp(char *zntyp);
	void SetRokvyroby(int rok);
	void SetCpu(int cpu);
	void SetRam(int ram);
	void SetHdd(int hdd);
	void SetPredajcislo(int pcislo);
	void SetPredajcena(long int pcena);


	char* GetZnackatyp();
	int GetRokvyroby();
	int GetCpu();
	int GetRam();
	int GetHdd();
	int GetPredajcislo();
	long int GetPredajcena();

	friend ostream& operator<<(ostream &vyst_prud, Notebook a);
};
