
#include<stdio.h>
#include<string.h>
#define MAX 501

int main()
{
	char znak, retazec[MAX];
	int dlzka, pocet = 0, i;
	scanf("%c %500s", &znak, retazec);// %500 maximalne 500 znakov , netreba pouzit ampersound lebo samotny ukayovatel ukazauje na zaciatok pola 
	dlzka = strlen(retazec);// zisti dlzku retazca ,kniznica string.h

	for (i = 0; i < dlzka; i++)
	{
		if (znak == retazec[i])
			pocet++;
	}
	printf("%d\n", pocet);
	return 0;
}

