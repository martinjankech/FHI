#include<stdio.h>
//#include<math.h> //ak volame funkciu 'abs'

int NSD_odcitavanim(int a, int b) // definicia funkcie  ak je parametricka tak musime uviest aj datove typy paramentrov 
{
	while (a != b)
	{
		if (a > b)
			a = a - b;
		else
			b = b - a;
	}
	return a;
}

int main()
{
	int c, d;
	scanf("%d %d", &c, &d);

	printf("%d\n", NSD_odcitavanim(c, d)); //NSD_odcitavanim(abs(c), abs(d)) - uprava pre prijimanie aj 
										   //zapornych argumentov;
	return 0;
}
