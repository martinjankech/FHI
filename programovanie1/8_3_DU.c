#include<stdio.h>
#include <stdlib.h>
#define MAX 50

void nacitajMaticu_zo_suboru(FILE *vstup, int *p_r, int *p_s, int M[][MAX])
{
	int i, j, r_local, s_local;

	fscanf(vstup, "%d %d", &r_local, &s_local);
	*p_r = r_local;
	*p_s = s_local;
	for (i = 0; i < r_local; i++)
		for (j = 0; j < s_local; j++)
			fscanf(vstup, "%d", &M[i][j]);
}

void vypisMaticu_do_suboru(FILE *vystup, int rf, int  sf, int M[][MAX])
{
	int i, j;
	for (i = 0; i < rf; i++)
	{
		for (j = 0; j < sf; j++)
			fprintf(vystup, "%6d", M[i][j]);
		fprintf(vystup, "\n");
	}
	fprintf(vystup, "\n");
}
//funkcia vynasobi matice AF * BF a ich sucin zapise do matice CF, cize vykona CF = AF * BF 
															 //i	      j		             j          k	
void nasobMatice(FILE *vystup, int CF[][MAX], int rf, int  sf, int AF[][MAX], int rf2, int  sf2, int BF[][MAX])
{
	int i, j, k;
	if (rf != sf2 && sf != rf2)
	{
		fprintf(vystup,"%c" " zla matica");
		exit(0);
	}


	for (i = 0; i < rf; i++)
		for (k = 0; k < sf2; k++)
		{
			CF[i][k] = 0;
			for (j = 0; j < sf; j++)
				CF[i][k] += AF[i][j] * BF[j][k];
		}
}

int main(int argc, char* argv[])
{
	int r, s, A[MAX][MAX], r2, s2, B[MAX][MAX], C[MAX][MAX]; //, navrat_hodn;
	int *pr, *ps, *pr2, *ps2;
	FILE *in, *out;
	//inicializacia ukazovatelov; su do nich vlozene adresy premennych 'r', 's', 'r2' a 's2'
	pr = &r;
	ps = &s;
	pr2 = &r2;
	ps2 = &s2;

	in = fopen("cisla.dat", "r");
	if (in == NULL)
	{
		printf("Subor 'cisla.dat' sa neda otvorit !!!");
		return 1;
	}

	out = fopen("vysledok.dat", "w");
	if (!out)
	{
		printf("Subor 'vysledok.dat' sa neda vytvorit !!!");
		return 1;
	}

	nacitajMaticu_zo_suboru(in, pr, ps, A);
	nacitajMaticu_zo_suboru(in, pr2, ps2, B);

	nasobMatice(out,C, r, s, A, r2, s2, B);
	vypisMaticu_do_suboru(out, r, s2, C);


	//zatvorenie datovych prudov, na ktore ukazuju ukazovatele 'in' a 'out' a "splachnutie" ich obsahu do 
	//k nim pripojenym diskovym suborom
	fclose(in);
	fclose(out);
	return 0;
}
