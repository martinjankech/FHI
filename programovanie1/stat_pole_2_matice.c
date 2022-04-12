#include <stdio.h>
#define MAX 20 
int main()
{
	int riadok, stlpec, i, j, A[MAX][MAX], B[MAX][MAX];
	scanf("%d %d", &riadok, &stlpec);
	for (i = 0; i < riadok; i++)
	{
		for (j = 0; j < stlpec; j++)
			scanf("%d", &A[i][j]);
	}
	for (i = 0; i < riadok; i++)
	{
		for (j = 0; j < stlpec; j++)
			scanf("%d", &B[i][j]);
		printf("\n");
	}
	for (i = 0; i < riadok; i++)
	{
		for (j = 0; j < stlpec; j++)
		{
			printf("%5d", A[i][j]);
		}
			printf("\n");
		

	}
	for (i = 0; i < riadok; i++)
	{
		for (j = 0; j < stlpec; j++)
		{
			printf("%5d", B[i][j]);
		}
			printf("\n");
		

	}

	return 0;
}