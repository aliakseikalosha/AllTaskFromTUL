#include "matrix.h"
#include <stdio.h>

int main() {
	int n = 2, m = 2;
	int **a = create(n,m), **b = create(n, m), **temp;
	fill(a, n, m);
	fill(b, n, m);
	printf("matrix A :\n");
	print(a, n, m);
	printf("matrix B :\n");
	print(b, n, m);
	printf("matrix A+B = :\n");
	temp = add(a, b, n, m);
	print(temp, n, m);
	printf("matrix A*B = :\n");
	temp = multiply(a, b, n, m);
	print(temp, n, m);
	freeMemory(a, n);
	freeMemory(b, n);
	freeMemory(temp, n);
	return 0;
}