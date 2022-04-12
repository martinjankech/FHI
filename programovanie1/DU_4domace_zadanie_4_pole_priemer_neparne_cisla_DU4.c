
#include<stdio.h>
#define MAX 11//makro

int main(int argc, char* argv[])
{
	int n=0, mod, i, j, pocitadlo_neparnych = 0, suma_neparnych = 0;
	float priemer_neparnych;
	int vstup[MAX];  	 //deklaracia pola pre vstupne hodnoty
	int vystupneparne[MAX]; //deklaracia pola pre vystupne neparne hodnoty
	do
	{
		scanf("%d", &vstup[n]);
	} while (vstup[n++]!= 0);
	n--;
	if (n < MAX)
	{
		for (i = j = 0; i < n; i++)
		{
			mod = vstup[i] % 2; //ak je hodn. prvku "vstup[i]" neparne cislo, potom nie je delitelna 2-kou bezo zvysku
			if (mod != 0)
			{
				vystupneparne[j] = vstup[i];//priradi hodnotu v danom indexe pola vstup do toho indexu pola vystupparne ale iba za podmienky ze mod sa nerovna 0
				suma_neparnych += vystupneparne[j];
				pocitadlo_neparnych++;
				j++;
			}
		}
	}
	else
		return 0;
	priemer_neparnych = suma_neparnych / (double)pocitadlo_neparnych; //konverzia na double je nutna, inak 
											   //by bolo vykonane celocis. delenie
	printf("\nneparne prvky pola 'vstup'           : ");
	for (j = 0; j < pocitadlo_neparnych; j++)  //vypis hodnot pola 'vystupparne' na konzolu
		printf("%d ", vystupneparne[j]);
	printf("\nsuma neparnych prvkov pola 'vstup'   : %d", suma_neparnych);
	printf("\npriemer neparnych prvkov pola 'vstup': %.2f\n", priemer_neparnych);
	return 0;
}









	
	
	
	
	
	
	
	
	
	
	











