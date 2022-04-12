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
	void ScitajMatice(int r, int s, int C[MAX][MAX], int A[MAX][MAX], int B[MAX][MAX])
	{ 
		int i, j;
	for (i = 0; i < r; i++)
		for (j = 0; j < s; j++)
			C[i][j] = A[i][j] + B[i][j];

	}
	void OdcitajMatice(int r, int s, int C[MAX][MAX], int A[MAX][MAX], int B[MAX][MAX])
	{
		int i, j;
		for (i = 0; i < r; i++)
			for (j = 0; j < s; j++)
				C[i][j] = A[i][j] - B[i][j];
	}
	int main()
	{
		int riadok, stlpec, A[MAX][MAX], B[MAX][MAX], C[MAX][MAX], D[MAX][MAX];
		scanf("%d %d", &riadok, &stlpec);
		NacitajMaticu(riadok, stlpec, A);
		NacitajMaticu(riadok, stlpec, B);
		printf("Matica A:\n");
		VypisMaticu(riadok, stlpec, A);
		printf("Matica B:\n");
		VypisMaticu(riadok, stlpec, B);

		ScitajMatice(riadok, stlpec, C, A, B);
		printf("\n\nSucet matic A + B:\n");
		VypisMaticu(riadok, stlpec, C);

		OdcitajMatice(riadok, stlpec, D, A, B);
		printf("\nRozdiel matic A - B:\n");
		VypisMaticu(riadok, stlpec, D);

		return 0;
	}
