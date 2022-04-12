#include <iostream>
using namespace std;

class Zoo
{
private:
	char Miesto[40];
	char Druh[40];
	int Cislo;
	int Cena;
public:
	Zoo(){}
	Zoo(char *Miesto, char *Druh, int Cislo, int Cena);
	

	char *GetMiesto();
	char *GetDruh();
	int GetCislo();
	int GetCena();
	 
	void SetMiesto(char *Place);
	void SetDruh(char *DruhZviera);
	void SetCislo(int Number);
	void SetCena(int Price);
};