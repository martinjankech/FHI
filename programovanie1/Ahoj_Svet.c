#include <stdio.h>  //v hlavickovom subore stdio.h su definovane funkcie printf a scanf
int main()
{
	char ckruzku[10];  //premenna, znakove pole, pre ulozenia c. kruzku

	printf("Ahoj, ake je cislo Tvojho kruzku?   ");
	scanf("%s", ckruzku);  //nacitanie cisla kruzku od pouzivatela
	printf("\nKruzok %s je dobry v programovani.\n", ckruzku); //vypisanie odpovede programu

		return 0;
}
