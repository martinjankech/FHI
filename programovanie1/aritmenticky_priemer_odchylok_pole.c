#include<stdio.h>
#define MAX 100
int main()
{
	int i = 0, j, n,suma=0;
	int pole[MAX];
	double aritmeticky_priemer, odchylka,aritmeticky_priemer_odchylky,sucet_odchyliek=0;
	printf("zadajte pocet cisel ktore chcete nacitat");
		scanf("%d", &n);
		printf("/n teraz zadajte cisla ktore chcete nacitat ");
		for (i = 0; i < n; i++)
		{
			scanf("%d", &pole[i]);
			suma = suma + pole[i];
		}
	aritmeticky_priemer = suma / i;
	for (j = 0; j < i; j++)
	{
		odchylka = aritmeticky_priemer - pole[j];
		if (odchylka < 0)
			odchylka = -odchylka;
		sucet_odchyliek = sucet_odchyliek + odchylka;
	}
	aritmeticky_priemer_odchylky = sucet_odchyliek / i;
	printf("ariteticky priemer odchylky je %.5f", aritmeticky_priemer_odchylky);
	return 0; 

		
	
	
	
	
	

	






}