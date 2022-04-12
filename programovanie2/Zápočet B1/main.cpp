#include "triedy.h"

int main()
{
	char Miesto[50], Znacka[50];
	int Rok, Cislo, Cena;
	int Pocet;
	int i=0, j=0;

	Auto objekty[5];
	
	cout << "Vlozte pocet (max. 3):  ";
	cin >> Pocet;

	if ((Pocet > 3) || (Pocet==0))
		cout <<"Zly pocet" << endl;
	
	else
	{	
		while(i < Pocet)
	{
		i++;
		cout << endl << "vlozte Miesto "<< i << ". auta      : ";
		cin>> Miesto;
		cout << "vlozte Znacku "<< i << ". auta : ";  
		cin>> Znacka;
		cout << "vlozte Rok " << i << ". auta : ";
		cin >> Rok;
		cout << "vlozte Cislo " << i <<". auta : ";
		cin >> Cislo;
		cout << "vlozte Cenu "<< i << ". auta : ";
		cin >> Cena;
		
		
		objekty[i].SetMiesto(Miesto);	
		objekty[i].SetZnacka(Znacka);	
		objekty[i].SetRok(Rok);	
		objekty[i].SetCislo(Cislo);	
		objekty[i].SetCena(Cena);	
		
		}
	cout << endl << endl;

	for (i=1; i<Pocet+1; i++)
	{
		cout << "Miesto : " << objekty[i].GetMiesto() << endl;
		cout << "Znacka : " << objekty[i].GetZnacka() << endl;
		cout << "Rok    : " << objekty[i].GetRok() << endl;
		cout << "Cislo  : " << objekty[i].GetCislo() << endl;
		cout << "Cena   : " << objekty[i].GetCena() << endl;
		cout << endl << "_______________________________________________________________";
	}
	
		}

return 0;
}