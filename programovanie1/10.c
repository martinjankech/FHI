#include <stdio.h>

//definovanie datoveho typu 'struct TZlomok'
struct TZlomok { int citatel, menovatel; };

//funkcia nacita zlomok, cize jeho citatela a menovatela do strukturovej premennej, na ktoru ukazuje //ukazovatel 'pz'
void CitajZlomok(struct TZlomok *pz)
{
	int citatel_local, menovatel_local;
	scanf("%d %d", &citatel_local, &menovatel_local);
	pz->citatel = citatel_local;
	pz->menovatel = menovatel_local;
}

//funkcia zisti a vrati najvacsi spolocny delitel celych cisiel 'a' a 'b'
int NSD(int a, int b)
{
	while (b)
	{
		int c = a % b;
		a = b;
		b = c;
	}
	return a;
}

//funkcia zjedodusi zlomok ulozeny v strukturovej premennej, na ktoru ukazuje ukazovatel 'pz'
void ZjednodusZlomok(struct TZlomok *pz)
{
	int nsd = NSD(pz->citatel, pz->menovatel);
	pz->citatel /= nsd;
	pz->menovatel /= nsd;
}

//funkcia vypise zlomok ulozeny v strukturovej premennej, na ktoru ukazuje ukazovatel 'pz'
void VypisZlomok(struct TZlomok *pz)
{
	int cela_cast, zvysok;
	ZjednodusZlomok(pz);
	cela_cast = pz->citatel / pz->menovatel;
	if (cela_cast)
	{
		printf("%d", cela_cast);
		zvysok = pz->citatel % pz->menovatel;
		if (zvysok)
			printf(" %d/%d ", zvysok, pz->menovatel);
	}
	else
	{
		printf("%d", pz->citatel);
		if (pz->citatel) // ak je 'pz->citatel == 0', napiseme len ju a nie napr. 0/1
			printf("/%d", pz->menovatel);
	}
	printf("\n");
}


/* funkcia naplni strukturovu premennu 'z' hodnotou citatela 'cit' a menovatela 'men',
   potom zlomok zjednodusi pomocou volania funkcie 'SkratZlomok' a nakoniec vrati
   takto upraveny zlomok v strukturovej premennej 'z'  */
struct TZlomok Zlomok(int cit, int men)
{
	struct TZlomok z;
	struct TZlomok *pz;
	pz = &z;
	z.citatel = cit;
	z.menovatel = men;
	ZjednodusZlomok(pz);
	return z;
}

//funkcia vykona sucin zlomkov ulozenych v strukturovych premennych 'z1f' a 'z2f'
//a vrati ho v strukturovej premennej typu 'struct TZlomok'
struct TZlomok SucinZlomkov(struct TZlomok z1f, struct TZlomok z2f)
{
	return Zlomok(z1f.citatel * z2f.citatel, z1f.menovatel * z2f.menovatel);
}

struct TZlomok PodielZlomkov(struct TZlomok z1f, struct TZlomok z2f)
{
	return Zlomok(z1f.citatel * z2f.menovatel, z1f.menovatel * z2f.citatel);
}
//funkcia vykona sucet zlomkov ulozenych v strukturovych premennych 'z1f' a 'z2f'
//a vrati ho v strukturovej premennej typu 'struct TZlomok'
struct TZlomok SucetZlomkov(struct TZlomok z1f, struct TZlomok z2f)
{
	return Zlomok(z1f.citatel * z2f.menovatel + z2f.citatel * z1f.menovatel, z1f.menovatel *
		z2f.menovatel);
}

struct TZlomok RozdielZlomkov(struct TZlomok z1f, struct TZlomok z2f)
{
	return Zlomok(z1f.citatel * z2f.menovatel - z2f.citatel * z1f.menovatel, z1f.menovatel *
		z2f.menovatel);
}



//________________________________________________________________________________
int main()
{
	struct TZlomok z1, z2, sucin_zlomkov, sucet_zlomkov;
	struct TZlomok *pz1, *pz2, *p_sucin_zlomkov, *p_sucet_zlomkov;
	pz1 = &z1;
	pz2 = &z2;
	p_sucin_zlomkov = &sucin_zlomkov;
	p_sucet_zlomkov = &sucet_zlomkov;

	CitajZlomok(pz1);
	VypisZlomok(pz1);
	printf("\n");
	CitajZlomok(pz2);
	VypisZlomok(pz2);

	sucin_zlomkov = SucinZlomkov(z1, z2);
	printf("\nsucin zlomkov: ");
	VypisZlomok(p_sucin_zlomkov);

	sucet_zlomkov = SucetZlomkov(z1, z2);
	printf("sucet zlomkov: ");
	VypisZlomok(p_sucet_zlomkov);

	sucet_zlomkov = RozdielZlomkov(z1, z2);
	printf("rozdiel zlomkov: ");
	VypisZlomok(p_sucet_zlomkov);

	sucin_zlomkov = PodielZlomkov(z1, z2);
	printf("npodiel zlomkov: ");
	VypisZlomok(p_sucin_zlomkov);

	return 0;
}