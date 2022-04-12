#include <stdio.h>
#define MAX 100
int main()
{
	int i = 0, j, max, min, element_moL, element_moR;
	int cisla[MAX];
	double priemer, odchylka, min_odchylka, suma = 0;

	printf("zadajte cisla okrem nuly");
	do {
		scanf("%d", &cisla[i]);
	} while (cisla[i++] != 0);
	i--;
	min = max = cisla[0];

	for (j = 0; j < i; j++)
	{
		if (max < cisla[j])

			max = cisla[j];

		if (min > cisla[j])

			min = cisla[j];
		suma = suma + cisla[j];
	}
	priemer = suma / i;
	element_moL = cisla[0];
	min_odchylka = priemer;
	for (j = 0; j < i; j++)
	{
		odchylka = priemer - cisla[j];
		if (odchylka < 0)
			odchylka = -odchylka;
		if (odchylka < min_odchylka)
		{
			min_odchylka = odchylka;
			element_moL = cisla[j];
		}
	}
	element_moR = cisla[i - 1];
	min_odchylka = priemer;
	j = i - 1;
	do
	{
		odchylka = priemer - cisla[j];
		if (odchylka < 0)
			odchylka = -odchylka;
		if (odchylka < min_odchylka)
		{
			min_odchylka = odchylka;
			element_moR = cisla[j];

		}
		j--;

	} while (j >= 0);
	if (element_moL != element_moR)
		printf("priemer:%.5f\nmin:%d\nmax:%d\nelement_mo_L:%d\nelement_mo_P:%d\n", priemer, min,
			max, element_moL, element_moR);
	else if (element_moL == element_moR)
		printf("priemer:%.5f\nmin:%d\nmax:%d\nelement_mo:%d\n", priemer, min, max, element_moL);

	return 0;
}