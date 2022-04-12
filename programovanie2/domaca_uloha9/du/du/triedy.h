#include<iostream>
using namespace std;
/********************* definicie tried programu ***************************/
// Trieda reprezentujuca prvok zoznamu
class Item {
public:
	Item(int value, Item *ItemToLinkTo = NULL); // deklaracia parametrickeho konstruktora
	int  value() { return m_value; } //clen. funk., ktora vrati datovu hodn. prvku zoznamu 'm_value'
	void next(Item *link) { m_next = link; } //clenska funkcia, ktora vlozi do vazobneho clena prvku
	//zoznamu 'm_next' ukazovatel 'link'
	Item* next() { return m_next; } //clenska funkcia, ktora vrati hodnotu vazobneho clena prvku zoznamu 'm_next'
private:
	int m_value;   // datova hodnota prvku zoznamu
	Item  *m_next; // vazobny clen prvku zoznamu
};
// Trieda reprezentujuca jednosmerny linearny zoznam
class IntList {
public:
	// definicia konstruktora s inicializacnym zoznamom
	IntList() : m_front(NULL), m_end(NULL), m_size(0) {}
	// definicia explicitneho destruktora
	~IntList() { remove_all(); }
	// clenska funkcia, ktora vrati ukazovatel na 1. prvok zoznamu
	Item* front() { return m_front; }
	// clenska funkcia, ktora
	void reverse();
	void insert_front(int value);  // vlozi na zaciatok zoznamu 1 prvok s hodnotou 'value'
	void insert_end(int value); // vlozi na koniec zoznamu 1 prvok s hodnotou 'value'
	void insert_order(int value); // vlozi 1 prvok s hodnotou 'value' do zoznamu USPORIADANE
	IntList& join(IntList &la, IntList &lb); // spoji 2 zoznamy 'la' a 'lb' NEusporiadanie
	IntList& join_ordered(IntList &la, IntList &lb); // spoji 2 zoznamy 'la' a 'lb' usporiadanie
	void remove_front();    // zmaze prvy prvok zoznamu
	void remove_all();     // zmaze vsetky prvky zoznamu
	int numb_of_occurr(int value); // zisti pocet vyskytov prvku s datovou hodnotou 'value' v zozname
	// Operatorova funkcia prepisaneho operatora '+' pre triedu 'IntList', ktora vykona neusporiadane
	// spojenie dvoch zoznamov do vysledneho zoznamu pomocou operatora '+'
	friend IntList& operator+(IntList&, IntList&);
	void display(); // clenska funkcia, ktora zobrazi datove hodnoty prvkov zoznamu na konzolu
private:
	//privatna clenska funkcia, ktora
	void bump_up() { ++m_size; } //zvacsi o 1 velkost zoznamu ulozenu v clenskej premennej 'm_size'
	void bump_down() { --m_size; } //zmensi o 1 velkost zoznamu ulozenu v clen. premennej 'm_size'
	// privatna clenska premenna -Item *m_front; // ukazovatel na prvy prvok zoznamu
	Item *m_end;   // ukazovatel na posledny prvok zoznamu
	Item *m_front; // doplneny ukazovatel na prvy prvok zoznamu
	int m_size;    // pocet prvkov zoznamu (jeho velkost)
};

