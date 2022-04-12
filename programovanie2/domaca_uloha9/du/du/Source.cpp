#include "triedy.h"
int main()
{
	int n, data, a, vkladane_c1_do_l2, vkladane_c2_do_l2, vkladane_c3_do_l2, hladane_c_v_l2;
	IntList l, l_ordered, l1, l2;
	cout << "vlozte zakladnu velkost zoznamov, ktore chcete vytvarat: ";
	cin >> n;
	cout << "\nvlozte " << n << " prvkov, ktore budu neusporiadane vlozene do zoznamu Z1: ";
	for (int i = 0; i < n; i++)
	{
		cin >> data; l1.insert_end(data);
	}
	cout << "\nZoznam Z1 = ";
	l1.display();
	cout << "\n_____________________________________________________________\n\nVlozte " << n << " prvkov, ktore budu USPORIADANE vlozene do zoznamu Z2: \n";
	for (int j = 0; j < n; j++)
	{
		cin >> a;
		l2.insert_order(a);
	}
	cout << "\nZoznam Z2 = ";
	l2.display();
	cout << "\nVlozte 3 cisla, ktore budu USPORIADANE vlozene do zoznamu Z2:\n";
	cin >> vkladane_c1_do_l2 >> vkladane_c2_do_l2 >> vkladane_c3_do_l2;
	l2.insert_order(vkladane_c1_do_l2);
	l2.insert_order(vkladane_c2_do_l2);
	l2.insert_order(vkladane_c3_do_l2);
	cout << "\nZoznam Z2 s USPORIADANE vlozenymi cislami " << vkladane_c1_do_l2 << ", " << vkladane_c2_do_l2 << " a " << vkladane_c3_do_l2 << "\nZoznam Z2 = ";
	l2.display();
	cout << "\nVlozte cislo, ktore sa ma hladat v zozname Z2: ";
	cin >> hladane_c_v_l2;
	cout << "Pocet vyskytov cisla " << hladane_c_v_l2 << " v zozname Z2: " << l2.numb_of_occurr(hladane_c_v_l2);
	cout << "\n______________________________________________________________";
	cout << "\n\n\nprvky LEN spojeneho zoznamu Z = Z1 + Z2 prepisanym operatorom '+':\n ";
	l = l1 + l2;
	l.display();
	l = l.join(l, l1);
	cout << "\n\nprvky LEN spojeneho zoznamu Z (= Z + Z1) instancnou funkciou 'join':\n ";
	l.display();
	l_ordered = l_ordered.join_ordered(l, l1);
	cout << "\n\nprvky USPORIADANE spojeneho zoznamu Z_usp (= Z + Z1) instancnou funkciou \n'join_ordered':\n ";
	l_ordered.display();


	cout << "Reverzovany(otoceny) zoznam Z : " << endl;
	l.reverse();
	l.display();

	cout << "Reverzovany (otoceny) zoznam Z_usp:" << endl;
	l_ordered.reverse();
	l_ordered.display();
	return 0;
}
