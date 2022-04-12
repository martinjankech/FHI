#include "triedy.h"
// definicia parametrickeho konstruktora triedy 'Item'
Item::Item(int value, Item *item) : m_value(value)
{
	if (!item)  //ak je hodnota vazobn. clena prvku zoznamu (ukazovatela na nasledujuci prvok) == NULL,
		m_next = NULL;  //tak potom inicializujeme instancnu premennu objektu tohto prvku zoznamu,
	//ukazovatel na nasledujuci prvok 'm_next', na NULL
	else
	{
		m_next = item->m_next;
		item->m_next = this;
	}
}
// definicia clenskej funkcie, ktora vlozi na koniec zoznamu 1 prvok s hodnotou 'value'
void IntList::insert_end(int value)
{
	if (!m_end)    //ak je zoznam prazdny,
		m_end = m_front = new Item(value); //tak novy prvok vkladame do zoznamu ako prvy a zaroven
	//aj ako posledny,
	else
		m_end = new Item(value, m_end);      //inak vlozime novy prvok na koniec zoznamu
	bump_up();
}
// definicia clenskej funkcie, ktora vlozi na zaciatok zoznamu 1 prvok s hodnotou 'value'
void IntList::insert_front(int value)
{
	Item *ptr = new Item(value); //vytvorenie ukazovatela 'ptr' na novy prvok zoznamu s hodnotou
	//datoveho clena 'value', ktory vkladame do tohto zoznamu
	if (!m_front)        //ak je zoznam prazdny,
		m_front = m_end = ptr; //tak novy prvok vkladame ako prvy (a zaroven aj ako posledny) do
	//zoznamu
	else              //inak
	{
		ptr->next(m_front);  //novy prvok vkladame pred prvy prvok zoznamu
		m_front = ptr;
	}
	bump_up();
}
// definicia clenskej funkcie, ktora vlozi 1 prvok s hodnotou 'value' do zoznamu USPORIADANE
void IntList::insert_order(int value)
{
	Item *ptr = new Item(value); //vytvorenie ukazovatela 'ptr' na novy prvok zoznamu s hodnotou
	//datoveho clena 'value', ktory vkladame do tohto zoznamu USPORIADANE
	if (!m_front)        //ak je zoznam prazdny,
		m_front = m_end = ptr; //tak novy prvok vkladame ako prvy do zoznamu,
	else              //inak hladame vhodne miesto pre vlozenie prvku
	{
		Item *pred = NULL, *po = m_front;
		enum { HLADAJ, UKONCI } stav = HLADAJ; // premenna 'stav' je vymenovaneho typu
		while ((stav == HLADAJ) && (po != 0))
			if (po->value() >= value)
				stav = UKONCI;     // Vhodne miesto najdene!
			else  // ak sa vhodne miesto pre vlozenie prvku nenaslo, tak sa presuvame dalej v zozname pri
				// jeho hladani
			{
				pred = po;   // Ukazovatele "pred" a "po"
				po = po->next();    // si zapamataju miesto vlozenia.
			}
		if (pred == NULL)     // Vlozenie noveho prvku na zaciatok zoznamu.
		{
			m_front = ptr;
			ptr->next(po);
		}
		else if (po == NULL)     // Vlozenie noveho prvku na koniec zoznamu.
		{
			m_end->next(ptr);
			ptr->next(NULL);
			m_end = ptr;
		}
		else       // Vlozenie noveho prvku medzi prvky zoznamu,
		{                  // na ktore teraz ukazuju ukazovatele 'pred' a 'po'
			pred->next(ptr);
			ptr->next(po);
		}
	}
	bump_up();
}
// definicia clenskej funkcie, ktora spoji 2 zoznamy 'za' a 'zb' NEusporiadanie
IntList& IntList::join(IntList &za, IntList &zb)
{
	IntList *z = new IntList;   // vytvorenie ukazovatela 'z' na novy objekt triedy 'IntList'. Do tohto noveho
	// objektu sa spajaju 2 zoznamy 'za' a 'zb'
	Item *p1 = za.front(); //vytvorenie pomocneho ukazovatela 'p1' na dat. typ 'Item', do ktoreho si ulozime
	//ukazovatel na 1. prvok zoznamu 'za'
	Item *p2 = zb.front(); //vytvorenie pomocneho ukazovatela 'p2' na dat. typ 'Item', do ktoreho si ulozime
	//ukazovatel na 1. prvok zoznamu 'zb'
	//NEusporiadane vkladanie (kopirovanie) prvkov zoznamu 'za' do noveho zoznamu s ukazovatelom 'z'
	while (p1 != NULL)
	{
		z->insert_end(p1->value());
		p1 = p1->next();
		bump_up();
	}
	//NEusporiadane vkladanie (kopirovanie) prvkov zoznamu 'zb' do noveho zoznamu s ukazovatelom 'z'
	while (p2 != NULL)
	{
		z->insert_end(p2->value());
		p2 = p2->next();
		bump_up();
	}
	return (*z);
}// definicia clenskej funkcie, ktora spoji 2 zoznamy 'za' a 'zb' usporiadanie
IntList& IntList::join_ordered(IntList &za, IntList &zb)
{
	IntList *z = new IntList;;
	Item *p1 = za.front();
	Item *p2 = zb.front();
	// usporiadane vkladanie (kopirovanie) prvkov zoznamu 'za' do noveho zoznamu s ukazovatelom 'z'
	while (p1 != NULL)
	{
		z->insert_order(p1->value());
		p1 = p1->next();
		bump_up();
	}
	// usporiadane vkladanie (kopirovanie) prvkov zoznamu 'zb' do noveho zoznamu s ukazovatelom 'z'
	while (p2 != NULL)
	{
		z->insert_order(p2->value());
		p2 = p2->next();
		bump_up();
	}
	return (*z);
}
// Operatorova funkcia prepisaneho operatora '+' pre triedu 'IntList', ktora vykona neusporiadane
// spojenie dvoch zoznamov do vysledneho zoznamu pomocou operatora '+'
IntList& operator+(IntList &za, IntList &zb)
{
	IntList *z = new IntList;
	Item *p1 = za.front();
	Item *p2 = zb.front();
	//NEusporiadane vkladanie (kopirovanie) prvkov zoznamu 'za' do noveho zoznamu s ukazovatelom 'z'
	while (p1 != NULL)
	{
		z->insert_end(p1->value());
		p1 = p1->next();
	}
	//NEusporiadane vkladanie (kopirovanie) prvkov zoznamu 'zb' do noveho zoznamu s ukazovatelom 'z'
	while (p2 != NULL)
	{
		z->insert_end(p2->value());
		p2 = p2->next();
	}
	return (*z);
}
// definicia clenskej funkcie, ktora zmaze prvy prvok zoznamu
void IntList::remove_front()
{
	if (m_front) // ak zoznam nie je prazdny, tzn. 'm_front != NULL',
	{
		Item *ptr = m_front; // tak vytvorime pomocny ukazovatel 'ptr' na datov. typ 'Item' a naplnime ho
		// ukazovatelom na 1. prvok zoznamu 'm_front'
		m_front = m_front->next(); //ukazovatel na 1. prvok zoznamu 'm_front' prepiseme ukazovatelom na
		//dalsi prvok zoznamu, pretoze 1. prvok zoznamu chceme zmazat
		bump_down(); delete ptr; // mazeme 1. prvok zoznamu, na ktory ukazuje ukazovatel 'ptr'
	}
}
// definicia clenskej funkcie, ktora zmaze vsetky prvky zoznamu
void IntList::remove_all()
{
	while (m_front)
		remove_front();
	m_size = 0;
	m_front = m_end = NULL;
}
// definicia clenskej funkcie, ktora zisti pocet vyskytov prvku s datovou hodnotou 'value' v zozname
int IntList::numb_of_occurr(int value)
{
	int numb_occurr = 0;
	Item *ptr = m_front;
	while (ptr)
	{
		if (ptr->value() == value)
			numb_occurr++;
		ptr = ptr->next();
	}
	return numb_occurr;
}
// definicia clenskej funkcie, ktora zobrazi datove hodnoty prvkov zoznamu na konzolu
void IntList::display()
{
	cout << "  (velkost " << m_size << ") ( ";
	Item *ptr = m_front;
	while (ptr)
	{
		cout << ptr->value() << " ";
		ptr = ptr->next();
	}
	cout << ")\n";
}
void IntList::reverse() {
	
	Item *currently = m_front, *previous = NULL, *next = NULL;

	while (currently != NULL)
	{
		next = currently->next();//dalsi ukazuje nato naco ukazuje m_next prvka na ktory ukazuje aktualny 

		currently->next(previous);//m_next prvku na ktory ukazuje aktualny bude ukazovat na prvok na ktory ukazuje predchadzajuci 

		previous = currently; //bude ukazovat na prvok na ktory ukazuje aktualny

		currently = next; //dalsi bude ukazovat na prvok na ktory ukazuje aktualny 
	}

	m_front = previous;

}
