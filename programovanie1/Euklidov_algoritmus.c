#include<stdio.h>
int main(void)
{
	int a, b;
	printf("zadajte 2 ciselne hodnoty");
	scanf("%d %d", &a, &b);
	do
	{
		if (a > b)
		{
			a = a - b;
		}

		else {
			b = b - a;
		}
	} while (a == b);
	printf("NSD je %d", a);

	return 0;





}