#include <stdio.h>
#include <stdlib.h>

/* vytvori 2-rozmerne dynamicke pole rozmerov 'dyn_r x dyn_s', nacita do neho prvky matice
   a vrati ukazovatel nan  */
double **NacitajMaticu_do_dyn_pola(int dyn_r, int dyn_s)
{
	double **dyn_pole; //ukazovatel na ukazovatel na double
	int i, j;

	/* vytvorenie ukazovatelov na riadky 2-rozmerneho dynamickeho pola, cize vytvorenie pola
	   ukazovatelov na double (double *), na ktore ukazuje ukazovatel 'dyn_pole'  */
	dyn_pole = (double **)malloc(dyn_r * sizeof(double *));
	if (dyn_pole == NULL)
	{
		printf("Nedostatok pamate !\n");
		exit(1);
	}


	/* naplnanie jednotlivych ukazovatelov v poli ukazovatelov, na ktore ukazuje ukazovatel
	   'dyn_pole', ukazovatelmi na jednotlive riadky 2-rozmerneho dynamickeho pola,
	   ktore obsahuju prvky datoveho typu 'double'  */
	for (i = 0; i < dyn_r; i++)
	{
		dyn_pole[i] = (double *)malloc(dyn_s * sizeof(double));
		if (dyn_pole[i] == NULL)
		{
			fprintf(stderr, "Nedostatok pamate !\n");
			exit(1);
		}
	}

	/* naplnanie jednotlivych prvkov 2-rozmerneho dynamickeho pola
	   'dyn_pole' hodnotami z konzoly (vlozenymi pouzivatelom)  */
	for (i = 0; i < dyn_r; i++)
		for (j = 0; j < dyn_s; j++)
			scanf("%lf", &dyn_pole[i][j]);

	return dyn_pole;
}

/* vytvori 2-rozmerne dynamicke pole rozmerov 'dyn_r x dyn_s', vyplni ho 0-mi
   a vrati ukazovatel nan  */
double **NacitajNulovuMaticu_do_dyn_pola(int dyn_r, int dyn_s)
{
	double **dyn_pole;
	int i, j;

	dyn_pole = (double **)malloc(dyn_r * sizeof(double *));
	if (dyn_pole == NULL)
	{
		fprintf(stderr, "Nedostatok pamate !\n");
		exit(1);
	}

	for (i = 0; i < dyn_r; i++)
	{
		dyn_pole[i] = (double *)malloc(dyn_s * sizeof(double));
		if (dyn_pole[i] == NULL)
		{
			fprintf(stderr, "Nedostatok pamate !\n");
			exit(1);
		}
	}

	//vyplnenie pola(matice) 0-mi
	for (i = 0; i < dyn_r; i++)
		for (j = 0; j < dyn_s; j++)
			dyn_pole[i][j] = 0;

	return dyn_pole;
}

//vypise na konzolu prvky matice ulozenej v 2-rozmernom dynamickom poli A 
void VypisMaticu_z_dyn_pola(int dyn_r, int dyn_s, double **A)
{
	int i, j;
	for (i = 0; i < dyn_r; i++)
	{
		for (j = 0; j < dyn_s; j++)
			printf("    %.2lf\t", A[i][j]);
		printf("\n");
	}
}

/* zisti pocty prvkov v jednotlivych riadkoch spodneho trojuholnika matice A a ulozi
   ich do prvkov pola 'pocty_prvkovST_f'  */
void ZistiPoctyPrvkovST(int dyn_r, int dyn_s, double **A, int pocty_prvkovST_f[100])
{
	int i, j, z;

	for (z = 0; z < 100; z++)
		pocty_prvkovST_f[z] = 0;

	for (i = 0; i < dyn_r; i++)
		for (j = 0; j < dyn_s; j++)
			if (i > j)
				pocty_prvkovST_f[i]++;
}

/* vytvori 2-rozmerne nepravidelne dynamicke pole, nacita do neho prvky spodneho trojuholnika
   matice A a vrati ukazovatel na toto 2-rozmerne nepravidelne dynamicke pole  */
double **NacitajSpodTrojuholnik_do_dyn_pola(int dyn_r, double **A)
{
	double **spod_truholn_dyn_pole; //ukazovatel na ukazovatel na double
	int i, j, z, pocet_riadkovST = dyn_r - 1;
	j = 1;

	/* vytvorenie ukazovatelov na riadky 2-rozmerneho dynamickeho pola, cize vytvorenie pola
	   ukazovatelov na double (double *), na ktore ukazuje ukazovatel 'spod_truholn_dyn_pole'  */
	spod_truholn_dyn_pole = (double **)malloc(pocet_riadkovST * sizeof(double *));
	if (spod_truholn_dyn_pole == NULL)
	{
		printf("Nedostatok pamate !\n");
		exit(1);
	}

	/* naplnanie jednotlivych ukazovatelov v poli ukazovatelov, na ktore ukazuje ukazovatel
	   'spod_truholn_dyn_pole', ukazovatelmi na jednotlive riadky 2-rozmerneho dynamickeho pola,
	   ktore obsahuju prvky datoveho typu 'double'  */
	for (i = 0; i < pocet_riadkovST; i++)
	{
		spod_truholn_dyn_pole[i] = (double *)malloc(j * sizeof(double));
		if (spod_truholn_dyn_pole[i] == NULL)
		{
			fprintf(stderr, "Nedostatok pamate !\n");
			exit(1);
		}
		j++;
	}

	/* naplnanie jednotlivych prvkov 2-rozmerneho nepravidelneho dynamickeho pola
	   'spod_truholn_dyn_pole' prvkami spodneho trojuholnika matice A  */
	z = 1;
	for (i = 0; i < pocet_riadkovST; i++)
	{
		for (j = 0; j < z; j++)
			spod_truholn_dyn_pole[i][j] = A[i + 1][j];
		z++;
	}

	return spod_truholn_dyn_pole;
}


/* vypise na konzolu prvky spodneho trojuholnika matice A ulozenej v 2-rozmernom
   nepravidelnom dynamickom poli 'spod_truholn_dyn_pole_f'  */
void VypisSpodTrojuholnik_do_dyn_pola(int dyn_r, double **A, double **spod_truholn_dyn_pole_f
	)
{
	int i, j, z, pocet_riadkovST = dyn_r - 1;
	z = 1;
	for (i = 0; i < pocet_riadkovST; i++)
	{
		for (j = 0; j < z; j++)
			printf("    %.2lf\t", spod_truholn_dyn_pole_f[i][j]);
		printf("\n");
		z++;
	}
}

/* funkcia vynasobi matice A * B ulozene v 2-rozmernych dynamickych poliach a ich sucin zapise do
   matice C, ktora je tiez ulozena v 2-rozmernom dynamickom poli; cize funkcia vykona C = A * B  */
int NasobMatice(int dyn_rA, int dyn_sA, double **A, int dyn_rB, int dyn_sB, double **B, double **C)
{
	int i, j, k;

	if (dyn_sA != dyn_rB)
		return 0;

	for (i = 0; i < dyn_rA; i++)
		for (k = 0; k < dyn_sB; k++)
			for (j = 0; j < dyn_sA; j++)
				C[i][k] += A[i][j] * B[j][k];

	return 1;
}
/* funkcia sèíta matice A a B uložené v 2-rozmerných dynamických poliach a ich
   súèet zapíše do matice C, ktorá je tiež uložená v 2-rozmernom dynamickom poli;
   èiže funkcia vykoná C = A + B  */

void ScitajMatice_v_dyn_pol(int dyn_rA, int dyn_sA, double **A, int dyn_rB,
	int dyn_sB, double **B, double **C)

{
	int i, j;
	for (i = 0; i < dyn_rA; i++)
		for (j = 0; j < dyn_sA; j++)
			C[i][j] += A[i][j] + B[i][j];
}
/* funkcia odèíta matice A a B uložené v 2-rozmerných dynamických poliach a ich
rozdiel zapíše do matice C, ktorá je tiež uložená v 2-rozmernom dynamickom poli;
èiže funkcia vykoná C = A - B  */
void OdcitajMatice_v_dyn_pol(int dyn_rA, int dyn_sA, double **A, int dyn_rB,
	int dyn_sB, double **B, double **C)
{
	int i, j;
	for (i = 0; i < dyn_rA; i++)
		for (j = 0; j < dyn_sA; j++)
			C[i][j] = A[i][j] - B[i][j];

}




/* Zmazanie dynamicky alokovaneho 2-rozmerneho pola z pamate */
void Zmaz_dyn_pole(int dyn_rX, double **dyn_pole)
{
	int i;
	for (i = 0; i < dyn_rX; i++)
		free(dyn_pole[i]);
	free(dyn_pole);
}

//________________________________________________________________________________
int main()
{
	double **matice[6]; //vytvorenie pola ukazovatelov na ukazovatel na double
	double **spod_truholn_dyn_pole; //ukazovatel na ukazovatel na double
	int rA, sA, rB, sB, navrat_hodn;

	scanf("%d %d", &rA, &sA); //nacitanie poctu riadkov a stlpcov matice A od pouzivatela
	matice[0] = NacitajMaticu_do_dyn_pola(rA, sA); //vytvorenie matice A a ulozenie ukazovatela na nu 
																			 //do prvku pola ukazovatelov 'matice[0]' 

	scanf("%d %d", &rB, &sB); //nacitanie poctu riadkov a stlpcov matice B od pouzivatela
	matice[1] = NacitajMaticu_do_dyn_pola(rB, sB); //vytvorenie matice B a ulozenie ukazovatela na nu 
																			 //do prvku pola ukazovatelov 'matice[1]'

	//vytvorenie nulovej matice a ulozenie ukazovatela na nu do prvku pola ukazovatelov 'matice[2]'
	matice[2] = NacitajNulovuMaticu_do_dyn_pola(rA, sB);
	matice[3] = NacitajNulovuMaticu_do_dyn_pola(rA, sB);
	printf("\nMatica A:\n");
	VypisMaticu_z_dyn_pola(rA, sA, matice[0]);
	printf("Matica B:\n");
	VypisMaticu_z_dyn_pola(rB, sB, matice[1]);

	
	spod_truholn_dyn_pole = NacitajSpodTrojuholnik_do_dyn_pola(rA, matice[0]);
	printf("\nSpodny trojuholnik A:\n");
	VypisSpodTrojuholnik_do_dyn_pola(rA, matice[0], spod_truholn_dyn_pole);
	spod_truholn_dyn_pole = NacitajSpodTrojuholnik_do_dyn_pola(rB, matice[1]);
	printf("\nSpodny trojuholnik B:\n");
	VypisSpodTrojuholnik_do_dyn_pola(rB, matice[1], spod_truholn_dyn_pole );

	//								   								   A                  	      B              C
	navrat_hodn = NasobMatice(rA, sA, matice[0], rB, sB, matice[1], matice[2]);
	if (navrat_hodn == 1)
	{
		printf("\nA * B:\n");
		VypisMaticu_z_dyn_pola(rA, sB, matice[2]);
	}
	else
		printf("Matice su nezodpovedajuceho typu pre nasobenie !!!\n");


	ScitajMatice_v_dyn_pol(rA, sA, matice[0], rB, sB, matice[1], matice[2]);
	printf("\nA + B:\n");
	VypisMaticu_z_dyn_pola(rA, sB, matice[2]);
	OdcitajMatice_v_dyn_pol(rA, sA, matice[0], rB, sB, matice[1], matice[2]);
	printf("\nA - B:\n");
	VypisMaticu_z_dyn_pola(rA, sB, matice[2]);

	//Zmazanie vsetkych dynamicky alokovanych 2-rozmernych poli z pamate
	Zmaz_dyn_pole(rA, matice[0]);
	Zmaz_dyn_pole(rB, matice[1]);
	Zmaz_dyn_pole(rA, matice[2]);
	Zmaz_dyn_pole(rB - 1, spod_truholn_dyn_pole);

	return 0;
}
