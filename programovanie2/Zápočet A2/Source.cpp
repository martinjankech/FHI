#include<time.h>
#include<iostream>
using namespace std;

int vyhladavanie (int m,int n,long *cisla)
{
	int zaciatok=1;
	int koniec=20;
	int pivot;
	pivot=(zaciatok+koniec)/2;                       // Find Mid Location of Array

	while(zaciatok<=koniec && cisla[pivot]!=n)      // Compare Item and Value of Mid
	{
		if(cisla[pivot]<n)
			zaciatok=pivot+1;
		else
			koniec=pivot-1;

			pivot=(zaciatok+koniec)/2;
	}

		if(cisla[pivot]==n)
		{
			cout<<"\nNasla sa zhoda na indexe : "<< pivot << endl;
		}
		else
		{
			cout<<"Nenasla sa zhoda" << endl;
		}


return pivot;
}


int main ()
{
	int i, n, m;
	long cisla[101], *hladane;
	int zaciatok, koniec, pivot;

	cout << "Vlozte velkost pola: ";
	cin >> m;

	
	srand((unsigned) time(NULL));   
	
	
	cisla[0]=(long)rand()%80;

	for (i = 1; i <= m; i++)
		cisla[i] = cisla[i-1] + rand()%80;

	for (i = 1; i <= m; i++)
		cout << "vygenerovane cislo ["<< i-1 << "]  :  " << cisla[i-1] << endl;
	
	cout << "vlozte hladane cislo: ";
	cin >> n;

	int index;
	index=vyhladavanie(m, n, cisla);

return 0;
}
