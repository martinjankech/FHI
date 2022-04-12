#include "Zlomky.h"

int main()
{
	cout << "Program vykona operacie so zlomkami" << endl << endl;

	//pri vytvarani objektov z1, z2 a z3 je volany konstruktor triedy Zlomky bez parametrov, clenom tejto
	//triedy je vsak konstruktor Zlomky(int cit = 0, int men = 1) s implicitnymi parametrami a ich
	//implicitnymi hodnotami, ktory je v takomto pripade zavolany
	Zlomky z1, z2, z3;

	cout << "Vlozte 1. zlomok " << endl;
	z1.Citaj_zlomok();
	//cin >> z1; //FUNKCNA alternativa

	cout << "1.zlomok je     : ";
	z1.Vypis_zlomok();
	cout << endl << endl;
	//cout << "1.zlomok je     : " << z1 << endl << endl; //FUNKCNA alternativa

	cout << "Vlozte 2. zlomok " << endl;
	cin >> z2;
	//z2.Citaj_zlomok(); //FUNKCNA alternativa

	cout << "2.zlomok je     : ";
	z2.Vypis_zlomok();
	cout << endl << endl;
	//cout << "2.zlomok je     : " << z2 << endl << endl; //FUNKCNA alternativa

	cout << "Vysledky: " << endl;
	cout << z1 << " + " << z2 << " = " << z1 + z2 << endl;
	cout << z1 << " - " << z2 << " = " << z1 - z2 << endl;
	cout << z1 << " *" << z2 << " = " << z1 * z2 << endl;
	cout << z1 << " / " << z2 << " = " << z1 / z2 << endl;

	cout << "Ciselne hodnoty 1. a 2. zlomku: " << endl;
	cout << z1 << " = " << z1.Hodnota_zlomku() << endl;
	cout << z2 << " = " << z2.Hodnota_zlomku() << endl;

	return 0;
}
