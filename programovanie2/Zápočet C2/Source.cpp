#include<iostream>
#include<time.h>
using namespace std;


int Naraznik(int x,int pole[],int dlzka)
{
	int j = 0;
	pole[dlzka] = x;
	for(int i=0; i<dlzka; i++)
	{
		if(pole[i] == x)
		{
			j = i;
		}
	}
	if(j < dlzka)
	{
	return j;
	}
}

int main()
{
	srand((unsigned)time(NULL));
	int n=20;
	int hladane;
	int index;
	int *pole = new int[n];
	
	cout << "Zadajte hladane cislo: ->  ";
	cin >> hladane;
	
		for(int i=0; i<n; i++)
	{
		pole[i] = rand()%100;
	}

	cout<<"\nZoznam prvkov pola"<< endl << endl;
	
		for(int i=0; i<n; i++)
	{
			cout << "vygenerovane pole[" << i << "]: "<< pole[i] << endl;
	}

	index = Naraznik(hladane,pole,n);
		if (index > 0)
	{
			cout << "\nIndex cisla je: " << index << endl;
	}
		else
			cout << "\nCislo sa v poli nenachadza!" << endl;

}



	