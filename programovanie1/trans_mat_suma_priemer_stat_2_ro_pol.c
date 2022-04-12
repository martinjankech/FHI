#include <stdio.h>
#define MAX 20
void NacitajMaticu(int r, int s, int A[MAX][MAX])
{
	int i, j; 
for (i = 0; i<r; i++)
for (j = 0; j<s; j++)
	scanf("%d", &A[i][j]);

}
void VypisMaticu(int r, int s, int A[MAX][MAX])
{
	int i, j;
	for (i = 0; i<r; i++)
	{
		for (j = 0; j<s; j++)
			printf("%5d",A[i][j]);
		    printf("\n");
	}
}
void TransponovanaMatica(int r, int s, int C[MAX][MAX], int T[MAX][MAX])
{
	int i, j;
	for (i = 0; i < s; i++)
	{
		for (j = 0; j < r; j++)
				T[i][j] = C[j][i];
	}
} 
int SumaPrvkovMatice(int r, int s, int A[MAX][MAX])
{
	int i, j;
	int Suma = 0;
	for (i = 0; i < r; i++)
	{
		for (j = 0; j < s; j++)
		{
			Suma = Suma + A[i][j];
		}


	}
	printf("Suma %d", Suma);
}
double PriemerPrvkovMatice(int r, int s, int A[MAX][MAX])
{
	int i, j, sucet_poli_matice;
	float suma = 0;
	double priemer;
	sucet_poli_matice = r * s;

	for (i = 0; i < r; i++)
    for (j = 0; j < s; j++)
		suma = suma + A[i][j];
		
	priemer = suma / sucet_poli_matice;
	printf("priemer:   %f", priemer);
}

int main()
{
	int riadky, stlpce, A[MAX][MAX],B[MAX][MAX],TA[MAX][MAX],TB[MAX][MAX];
	printf("zadaj stlpec a riadok \t");
	scanf("%d %d", &riadky, &stlpce);
	NacitajMaticu(riadky, stlpce, A);
	NacitajMaticu(riadky, stlpce, B);
	printf("\n");
	VypisMaticu(riadky, stlpce, A);
	printf("\n");
		SumaPrvkovMatice(riadky, stlpce, A);
	printf("\n");
	PriemerPrvkovMatice(riadky, stlpce, A);
	printf("\n");
	VypisMaticu(riadky, stlpce, B);
	printf("\n");
	SumaPrvkovMatice(riadky, stlpce, B);
	printf("\n");
	PriemerPrvkovMatice(riadky, stlpce, B);
	printf("\n");
	TransponovanaMatica(riadky,stlpce, A,TA);
	VypisMaticu(stlpce,riadky, TA);
	printf("\n");
	SumaPrvkovMatice(stlpce,riadky ,TA);
	printf("\n");
	PriemerPrvkovMatice(stlpce,riadky, TA);
	printf("\n");
	TransponovanaMatica(riadky,stlpce,B, TB);// v parametri sa vymenili stpce a riadky pretože ide o transponovanu maticu
	VypisMaticu(stlpce,riadky ,TB);
	printf("\n");
	SumaPrvkovMatice(stlpce,riadky, TB);
	printf("\n");
	PriemerPrvkovMatice(stlpce,riadky, TB);
	return 0;


	




}
