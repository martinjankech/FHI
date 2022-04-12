#include<stdio.h>
#define MAX 20 
void NacitajMaticu(int r, int s, int A[MAX][MAX])
{
	int i, j;
	for (i = 0; i < r; i++)
		for (j = 0; j < s; j++)
			scanf("%d", &A[i][j]);
}
void VypisMaticu(int r, int s, int A[MAX][MAX])
{
	int i, j;
	for (i = 0; i < r; i++)
	{
		for (j = 0; j < s; j++)

			printf("%5d", A[i][j]);
		printf("\n");
	}
}
double AritmPriemerPrvkovNaDiagonale (int r, int s, int A[MAX][MAX])
{
	int i, j, pocet_prvkov_na_hl_diagonale = 0, suma = 0, aritmeticky_priemer;
	for (i = 0; i < r; i++)
	{
        for (j = 0; j < s; j++)
			if (i == j)
			{
				suma = suma + A[i][j];
				pocet_prvkov_na_hl_diagonale++;

			}
    }
	aritmeticky_priemer = suma / pocet_prvkov_na_hl_diagonale;
	return aritmeticky_priemer;
}
int main()
{
	int riadok, stlpec, A[MAX][MAX], B[MAX][MAX];
	scanf("%d %d", &riadok, &stlpec);
	NacitajMaticu(riadok, stlpec, A);
	NacitajMaticu(riadok, stlpec, B);
	printf("Matica A:\n");
	VypisMaticu(riadok, stlpec, A);
	printf("Matica B:\n");
	VypisMaticu(riadok, stlpec, B);
	printf("\n");
	printf("aritmeticky priemer prvkov na hlavnej diagonale matice A je %f", AritmPriemerPrvkovNaDiagonale(riadok, stlpec, A));
	printf("\n aritmeticky priemer prvkov na hlavnej diagonale matice B je %f", AritmPriemerPrvkovNaDiagonale(riadok, stlpec, B));





}






