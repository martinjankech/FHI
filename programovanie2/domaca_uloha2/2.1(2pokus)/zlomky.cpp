#include "Zlomky.h"

//definicia neclenskej funkcie, ktora zisti a vrati najvacsi spolocny delitel celych cisiel 'a' a 'b'
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

//Clenska funkcia zjednodusi zlomok ulozeny v objekte 'zlomok' triedy 'Zlomky'.
//Funkcii musime odovzdat referenciu na objekt, pretoze funkcia moze zmenit instancne premenne //objektu. 
void Zlomky::ZjednodusZlomok(Zlomky &zlomok)
{
	int nsd = NSD(zlomok.citatel, zlomok.menovatel);

	if (nsd == 0)
	{
		cout << "NSD je 0." << endl;
		exit(1); //okamzite skoncenie programu
	}

	zlomok.citatel /= nsd;
	zlomok.menovatel /= nsd;

	//priradenie spravneho znamienka zlomku,
	if (zlomok.citatel < 0 && zlomok.menovatel < 0)
	{
		zlomok.citatel = -zlomok.citatel;
		zlomok.menovatel = -zlomok.menovatel;
	}
	//ak je zlomok zaporny, tak znamienko '-' zapiseme do jeho citatela
	else if (zlomok.citatel < 0 || zlomok.menovatel < 0)
	{
		zlomok.citatel = -abs(zlomok.citatel);
		zlomok.menovatel = abs(zlomok.menovatel);
	}
}

void Zlomky::Test_nuly_v_men()
{
	if (menovatel == 0)
	{
		cout << "Zlomok obsahuje nulu v menovateli." << endl;
		exit(1); //okamzite skoncenie programu
	}
}

//Definicia clenskej operatorovej funkcie prepisaneho binarneho operatora + s jednym parametrom 'z'. 
//Jej druhym parametrom je ukazovatel 'this' na aktualnu instanciu triedy.
//Objekt s ukazovatelom 'this' posiela spravu sam sebe.
Zlomky Zlomky::operator +(Zlomky z)
{
	int cit_vysledok, men_vysledok;
	men_vysledok = menovatel * z.menovatel;
	cit_vysledok = citatel * z.menovatel + z.citatel * menovatel;

	//	     volanie parametrickeho konstruktora, ktory vytvori objekt triedy 'Zlomky'
	return Zlomky(cit_vysledok, men_vysledok);
}

//Definicia operatorovej priatelskej funkcie k triede 'Zlomky', ktora prepisuje operator *.
//Tato operatorova funkcia nie je clenskou funkciou triedy 'Zlomky', je len priatelskou funkciou tejto //triedy.
Zlomky operator * (Zlomky z1, Zlomky z2)
{
	int cit_vysledok, men_vysledok;
	cit_vysledok = z1.citatel * z2.citatel;
	men_vysledok = z1.menovatel * z2.menovatel;

	//	   	volanie parametrickeho konstruktora, ktory vytvori objekt triedy 'Zlomky'
	return Zlomky(cit_vysledok, men_vysledok);
}
Zlomky Zlomky::operator -(Zlomky z)
{
	int cit_vysledok, men_vysledok;
	men_vysledok = menovatel * z.menovatel;
	cit_vysledok = citatel * z.menovatel - z.citatel * menovatel;
	return Zlomky(cit_vysledok, men_vysledok);
}
Zlomky operator / (Zlomky z1, Zlomky z2)
{
	int cit_vysledok, men_vysledok;
	cit_vysledok = z1.citatel * z2.menovatel;
	men_vysledok = z1.menovatel * z2.citatel;
	// volanie prametrickeho konstruktora, ktory vytvori objekt triedy 'Zlomky'
	return Zlomky(cit_vysledok, men_vysledok);
}


//Definicia neclenskej spriatelenej operatorovej funkcie prepisaneho operatora << 
ostream& operator << (ostream &prud, Zlomky z)
{
	int cela_cast, zvysok;
	z.ZjednodusZlomok(z);
	z.Test_nuly_v_men();
	cela_cast = z.citatel / z.menovatel;
	if (cela_cast)
	{
		prud << cela_cast << " ";
		zvysok = z.citatel % z.menovatel;

		if (zvysok)
			prud << zvysok << "/" << z.menovatel;
	}
	else
	{
		prud << z.citatel;
		if (z.citatel) // ak je 'z.citatel == 0', napiseme len ju a nie napr. 0/1
			prud << "/" << z.menovatel;
	}
	return prud;
}

//Definicia neclenskej spriatelenej operatorovej funkcie prepisaneho operatora >>.
//Operatorovej funkcii musime odovzdat referenciu na objekt z, pretoze funkcia meni jeho instancne //premenne
istream& operator >> (istream &prud, Zlomky &z)
{
	cout << "Vlozte citatel  : ";
	prud >> z.citatel;
	cout << "Vlozte menovatel: ";
	prud >> z.menovatel;
	return prud;
}