#pragma once
#include<iostream>
using namespace std;

class Ustav
{
private:
	char nazovUstavu[30];
	char sidloUstavu[30];
	long ICO;
public:
	Ustav() {}
	Ustav(char *naz, char *sid, long ico);

	void SetNazov(char *naz);
	void SetSidlo(char *sid);
	void SetIco(long ico);

	char* GetNazov();
	char* GetSidlo();
	long GetIco();
};
class Klient :public Ustav
{
private:
	char meno[25];
	char priezvisko[25];
	char rodcis[12];
	char diagnoza[20];
public:
	Klient() {}
	Klient(char *naz, char *sid, long ico, char *men, char *prie, char *rc, char *diag);

	void SetMeno(char *men);
	void SetPriezvisko(char *prie);
	void SetRodcis(char *rc);
	void SetDiagnoza(char *diag);

	char* GetMeno();
	char* GetPriezvisko();
	char* GetRodcis();
	char* GetDiagnoza();

	int VratRokNarod();
	int najdiKlienta(Klient *pole, char *diagn, int index);
	friend ostream& operator<<(ostream &vyst_prud, Klient a);
};