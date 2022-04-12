#include<stdio.h>
int main(int argc, char* argv[])
{
	int a, b, c, max;
	printf("vlozte 3 rozne cele cisla: ");  //vypiseme retazec "vlozte 3 rozne cele cisla: " na konzolu
	scanf("%d %d %d", &a, &b, &c);  //nacitanie troch hodnot zo vstupu do premennych a, b, c
	if (a = b) && (a = c) && (c = b) {
		
		printf("Vlozili ste TRI ROVNAKE cisla, nie je mozne zistit maximalne z 3 cisiel !!!");

	}
		

	if (a > b)  //ak a>b
		max = a;  //vykona sa prikaz max=a;
	else	  //inak
		max = b;  //sa vykona prikaz max=b;
	if (max < c)  //ak max<c
		max = c;  //vykona sa prikaz max=c;

	printf("max. cislo je %d\n", max);  //vypiseme hodnotu premennej ‘max‘ na konzolu

	return 0;
}
