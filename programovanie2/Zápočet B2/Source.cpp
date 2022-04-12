#include<time.h>
#define VYMEN(a, b) { long tmp = a; a = b; b = tmp; }
#include<iostream>
using namespace std;

int Vyhladavanie(int m, int n, long *pole){
	int i, s=0;
	for (i = 0; i < m; i++)
	{
		if (pole[i] == n)
		{
			cout << "\nNasla sa zhoda na indexe : " << i << endl;
			s = 1;
			break;
		}
	}

	if (s == 0)
	{
		cout << "Nenasla sa zhoda" << endl;
		return -1;
	}
	return i;
}


int main()
{
	long n, s = 0, m = 20;
	cout << "vlozte hladane cislo (1-100): ";
	cin >> n;
	long pole[20];
	int i = 0;

	srand((unsigned)time(NULL));

	for (i = 0; i < 20; i++)
		pole[i] = (double)rand() / (RAND_MAX + 1) * 100 + 1;


	for (i = 0; i < 20; i++)
		cout << "vygenerovane cisla [" << i + 1 << "] : " << pole[i] << endl;

	cout << endl;

	int index;
	index = Vyhladavanie(20, n, pole); // pocet prvkov, hladane cislo, tvoje pole
	cout << "i je : " << index;



	return 0;
}
