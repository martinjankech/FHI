#include<stdio.h>
int main(void)
{
	int a, b, min;
	scanf("%d %d", &a, &b);
	if(a > b)
	{
		min = b;
		printf("%d",min);
	}
	else 
	{
		
		min = a;
		printf("%d",min);
	}
		

	return(0);

}