#include "triedy.h"

int main()
{
	char Miesto[40], Druh[40];
	int Cislo, Cena;
	int Pocet;
	int i=0, j=0;

	Zoo objekty[10];
	
	cout << "Vlozte pocet (max. 3):  ";
	cin >> Pocet;

	if ((Pocet > 3) || (Pocet==0))
		cout <<"Zly pocet" << endl;
	
	else
	{	
		while(i < Pocet)
	{
		i++;
		cout << endl << "vlozte miesto zoo"<< i << " . zvierata      : ";
		cin>> Miesto;
		cout << "vlozte druh zvierata "<< i << " . zvierata : ";  
		cin>> Druh;
		cout << "vlozte cislo " << i <<" . zvierata : ";
		cin >> Cislo;
		cout << "vlozte cenu "<< i << " . zvierata : ";
		cin >> Cena;
		
		
		objekty[i].SetMiesto(Miesto);	
		objekty[i].SetDruh(Druh);	
		objekty[i].SetCislo(Cislo);	
		objekty[i].SetCena(Cena);	
		
		}
	cout << endl << endl;

	for (i=1; i<Pocet+1; i++)
	{
		cout << "Miesto : " << objekty[i].GetMiesto() << endl;
		cout << "Druh   : " << objekty[i].GetDruh() << endl;
		cout << "Cislo  : " << objekty[i].GetCislo() << endl;
		cout << "Cena   : " << objekty[i].GetCena() << endl;
		cout << endl << "_______________________________________________________________" <<endl;
	}
	
		}

return 0;
}