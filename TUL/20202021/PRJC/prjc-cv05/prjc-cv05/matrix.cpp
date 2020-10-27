#include "matrix.h"
#include <cstddef>
#include <malloc.h>
#include <stdio.h>
#include <stdlib.h>


int** create(int n, int m) {
	int** matrix = (int**)malloc(n * sizeof(int*));
	if (matrix != NULL) {

		for (size_t i = 0; i < n; i++)
		{
			matrix[i] = (int*)malloc(m * sizeof(int));
			if (matrix == NULL) {
				for (size_t j = 0; j < i; j++)
				{
					free(matrix[j]);
				}
				free(matrix);
				return NULL;
			}
		}
		return matrix;
	}
	return NULL;
}

int** add(int** a, int** b, int n, int m) {
	int** result = create(n, m);
	for (size_t x = 0; x < n; x++)
	{
		for (size_t y = 0; y < m; y++)
		{
			result[x][y] = a[x][y] + b[x][y];
		}
	}
	return result;
}

int sumM(int** a, int** b, int n, int m, int i, int j) {

	int s = 0;
	for (size_t k = 0; k < n; k++)
	{
		s += a[i][k] * b[k][j];
	}
	return s;
}

int** multiply(int** a, int** b, int n, int m) {
	if (n != m) {
		return NULL;
	}

	int** result = create(n, m);
	for (size_t i = 0; i < n; i++)
	{
		for (size_t j = 0; j < m; j++)
		{
			result[i][j] = sumM(a, b, n, m, i, j);
		}
	}
	return result;
}

void fill(int** matrix, int n, int m) {
	for (size_t i = 0; i < n; i++)
	{
		for (size_t j = 0; j < m; j++)
		{
			matrix[i][j] = rand() % 10;
		}
	}
}

void print(int** matrix, int n, int m) {
	if (matrix == NULL) {
		printf("NULL\n");
		return;
	}
	for (size_t i = 0; i < n; i++)
	{
		for (size_t j = 0; j < m; j++)
		{
			printf("\t%d", matrix[i][j]);
		}
		printf("\n ");
	}
}


void freeMemory(int** matrix, int n) {
	for (size_t i = 0; i < n; i++)
	{
		free(matrix[i]);
	}
	free(matrix);
}