#include <stdio.h>
#define MAX 20
//funkcia nacita cisla zo vstupu (z konzoly) do prvkov matice A reprezentovanej 2-rozmernym polom 
void NacitajMaticu(int r, int s, int A[MAX][MAX])
{
	int i, j; //'i' je riadkovy a 'j' stlpcovy index prvkov matice A ukladanych do pola A[MAX][MAX] 
	for (i = 0; i < r; i++)
		for (j = 0; j < s; j++)
			scanf("%d", &A[i][j]);
}//funkcia vypise prvky matice A (2-rozmerneho pola A[MAX][MAX]) na konzolu
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
//funkcia vytvori z matice C transponovanu maticu a ulozi ju do matice T
void TransponovanaMatica(int r, int s, int C[MAX][MAX], int T[MAX][MAX])
{
	int i, j;
	for (i = 0; i < s; i++)
	{
		for (j = 0; j < r; j++)
			T[i][j] = C[j][i];
	}
}
int main()
{
	//'i' je riadkovy a 'j' stlpcovy index prvkov matice ulozenych v 2-rozmernych poliach A, B, TA, TB
	int riadok, stlpec, i, j, suma_A = 0, suma_B = 0, A[MAX][MAX], B[MAX][MAX], TA[MAX][MAX],
		TB[MAX][MAX];
	scanf("%d %d", &riadok, &stlpec);
	NacitajMaticu(riadok, stlpec, A);
	NacitajMaticu(riadok, stlpec, B);
	printf("A\n");
	VypisMaticu(riadok, stlpec, A);

	for (i = 0; i < riadok; i++)
		for (j = 0; j < stlpec; j++)+
			suma_A += A[i][j];

	printf("suma A: %d\n", suma_A);
	printf("B\n");
	VypisMaticu(riadok, stlpec, B);

	for (i = 0; i < riadok; i++)
		for (j = 0; j < stlpec; j++)
			suma_B += B[i][j];

	printf("suma B: %d\n", suma_B);
	TransponovanaMatica(riadok, stlpec, A, TA);
	TransponovanaMatica(riadok, stlpec, B, TB);
	printf("TA\n");
	VypisMaticu(stlpec, riadok, TA); //pocet riadkov transponovanej matice TA = poctu stlpcov povodnej 
										 //matice A; pocet stlpcov TA = poctu riadkov A 
	printf("TB\n");
	VypisMaticu(stlpec, riadok, TB);

	return 0;
}




